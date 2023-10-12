using PhoneBookApp.PhoneBookApi.Dtos;
using PhoneBookApp.PhoneBookApi.Models;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.PhoneBookApi.Services
{
    public interface IContactService
    {
        Task<Response<List<ContactDto>>> GetAllContactsAsync();    
        Task<Response<ContactCreateDto>> CreateContactAsync(ContactCreateDto contactDto);
        Task<Response<NoContent>> DeleteContactAsync(string id);
        Task<Response<ContactDetailDto>> GetContactDetailByIdAsync(string id);

    }
}
