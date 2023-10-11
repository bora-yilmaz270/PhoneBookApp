using AutoMapper;
using MongoDB.Driver;
using PhoneBookApp.PhoneBookApi.Dtos;
using PhoneBookApp.PhoneBookApi.Models;
using PhoneBookApp.PhoneBookApi.Settings;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.PhoneBookApi.Services
{
    public class ContactService: IContactService
    {
        private readonly IMongoCollection<Contact> _contactCollection;
        private readonly IMongoCollection<ContactInfo> _contactInfoCollection;

        private readonly IMapper _mapper;
        public ContactService(IMapper mapper, IDatabaseSettings databaseSettings, IContactInfoService contactInfoService)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var db = client.GetDatabase(databaseSettings.DatabaseName);

            _contactCollection = db.GetCollection<Contact>(databaseSettings.ContactCollectionName);
            _contactInfoCollection = db.GetCollection<ContactInfo>(databaseSettings.ContactInfoCollectionName);

            _mapper = mapper;
           
        }
        public async Task<Response<List<ContactDto>>> GetAllContactsAsync()
        {
            var contacts = await _contactCollection.Find(c => true).ToListAsync();

            return Response<List<ContactDto>>.Success(_mapper.Map<List<ContactDto>>(contacts), 200);      
        }
        public async Task<Response<ContactDto>> GetContactByIdAsync(string id)
        {            
            var contact =await _contactCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return Response<ContactDto>.Success(_mapper.Map<ContactDto>(contact), 200);
        }
        public async Task<Response<ContactDetailDto>> GetContactDetailByIdAsync(string id)
        {
            var contact = await _contactCollection.Find(x => x.Id == id).FirstOrDefaultAsync();            
                 contact.ContactInfos = await _contactInfoCollection.Find(x => x.ContactId == id).ToListAsync();

            return Response<ContactDetailDto>.Success(_mapper.Map<ContactDetailDto>(contact), 200);
        }
        public async Task<Response<ContactCreateDto>> CreateContactAsync(ContactCreateDto contactDto)
        {

            var contact = _mapper.Map<Contact>(contactDto);

            await _contactCollection.InsertOneAsync(contact);

            return Response<ContactCreateDto>.Success(_mapper.Map<ContactCreateDto>(contact), 200);
        }
        public async Task<Response<NoContent>> DeleteContactAsync(string id)
        {

            var result = await _contactCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }           
        }              
              
    }
}
