using AutoMapper;
using MassTransit;
using MongoDB.Driver;
using PhoneBookApp.PhoneBookApi.Dtos;
using PhoneBookApp.PhoneBookApi.Models;
using PhoneBookApp.PhoneBookApi.Settings;
using PhoneBookApp.Shared.Dtos;
using PhoneBookApp.Shared.Messages;
using Mass = MassTransit;

namespace PhoneBookApp.PhoneBookApi.Services
{
    public class ContactInfoService: IContactInfoService
    {
        private readonly IMongoCollection<ContactInfo> _contactInfoCollection;
        private readonly IMapper _mapper;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public ContactInfoService(IMapper mapper, IDatabaseSettings databaseSettings, ISendEndpointProvider sendEndpointProvider)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var db = client.GetDatabase(databaseSettings.DatabaseName);

            _contactInfoCollection = db.GetCollection<ContactInfo>(databaseSettings.ContactInfoCollectionName);
            _mapper = mapper;  
            _sendEndpointProvider = sendEndpointProvider;
        }     
        public async Task<Shared.Dtos.Response<List<ContactInfoDto>>> GetAllContactInfosAsync()
        {

            var contactInfos = await _contactInfoCollection.Find(c => true).ToListAsync();
            if (contactInfos==null)
            {
                return Shared.Dtos.Response<List<ContactInfoDto>>.Fail("Not found", 404);
            }
            return Shared.Dtos.Response<List<ContactInfoDto>>.Success(_mapper.Map<List<ContactInfoDto>>(contactInfos), 200);


        }
        public async Task<Shared.Dtos.Response<ContactInfoCreateDto>> CreateContactInfoAsync(ContactInfoCreateDto contactInfoDto)
        {         
            var contactInfo = _mapper.Map<ContactInfo>(contactInfoDto);

            await _contactInfoCollection.InsertOneAsync(contactInfo);

            return Shared.Dtos.Response<ContactInfoCreateDto>.Success(_mapper.Map<ContactInfoCreateDto>(contactInfo), 200);

        }
        public async Task<Shared.Dtos.Response<NoContent>> DeleteContactInfoAsync(string id)
        {
            if (id is null)
            {
                return Shared.Dtos.Response<NoContent>.Fail("Not found", 404);

            }

            var result = await _contactInfoCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Shared.Dtos.Response<NoContent>.Success(204);
            }
            else
            {
                return Shared.Dtos.Response<NoContent>.Fail("Not found", 404);
            }
        }
        public  async Task<Shared.Dtos.Response<NoContent>> CreateReportAsync(string Id)
        {
            if (Id is null)
            {
                return Shared.Dtos.Response<NoContent>.Fail("Not found", 404);
            }

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-reportdetail-service"));

            List<ReportDetailCommand> reportDetailEvents = new List<ReportDetailCommand>();

            var contactInfos = await  _contactInfoCollection.Find(c => true).ToListAsync();

            var locations = contactInfos.Where(x => x.InfoType == "Location") ?? null;

            var distinctLocations = locations.Select(x => x.Value).Distinct();

            foreach (var location in distinctLocations)
            {
                ReportDetailCommand reportDetailEvent = new ReportDetailCommand();
                reportDetailEvent.ReportId = Id;

                var contacts = locations
                  .Where(x => x.Value == location)
                  .Select(x => x.ContactId)
                  .Distinct();

                var phoneNumbers = contactInfos
                  .Where(x => x.InfoType == "Phone" && contacts.Contains(x.ContactId))
                  .Select(x => x.Value)
                  .Distinct()
                .Count();

                reportDetailEvent.ContactCount = contacts.Count();
                reportDetailEvent.PhoneNumberCount = phoneNumbers;
                reportDetailEvent.Location = location;
                reportDetailEvents.Add(reportDetailEvent);
            }

            ListDetailCommand listDetailEvent = new ListDetailCommand();

            listDetailEvent.ReportDetailEvents = reportDetailEvents;

            await sendEndpoint.Send<ListDetailCommand>(listDetailEvent);

            return Shared.Dtos.Response<NoContent>.Success(200);

        } 
    }
}
