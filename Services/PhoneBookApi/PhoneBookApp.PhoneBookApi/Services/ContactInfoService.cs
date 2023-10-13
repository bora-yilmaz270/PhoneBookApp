using AutoMapper;
using MongoDB.Driver;
using PhoneBookApp.PhoneBookApi.Dtos;
using PhoneBookApp.PhoneBookApi.Models;
using PhoneBookApp.PhoneBookApi.Settings;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.PhoneBookApi.Services
{
    public class ContactInfoService: IContactInfoService
    {
        private readonly IMongoCollection<ContactInfo> _contactInfoCollection;
        private readonly IMapper _mapper;
        public ContactInfoService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var db = client.GetDatabase(databaseSettings.DatabaseName);

            _contactInfoCollection = db.GetCollection<ContactInfo>(databaseSettings.ContactInfoCollectionName);

            _mapper = mapper;
        }     
        public async Task<Response<List<ContactInfoDto>>> GetAllContactInfosAsync()
        {

            var contactInfos = await _contactInfoCollection.Find(c => true).ToListAsync();
            if (contactInfos==null)
            {
                return Response<List<ContactInfoDto>>.Fail("Not found", 404);
            }
            return Response<List<ContactInfoDto>>.Success(_mapper.Map<List<ContactInfoDto>>(contactInfos), 200);


        }
        public async Task<Response<ContactInfoCreateDto>> CreateContactInfoAsync(ContactInfoCreateDto contactInfoDto)
        {         
            var contactInfo = _mapper.Map<ContactInfo>(contactInfoDto);

            await _contactInfoCollection.InsertOneAsync(contactInfo);

            return Response<ContactInfoCreateDto>.Success(_mapper.Map<ContactInfoCreateDto>(contactInfo), 200);

        }
        public async Task<Response<NoContent>> DeleteContactInfoAsync(string id)
        {

            var result = await _contactInfoCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Not found", 404);
            }
        }
    }
}
