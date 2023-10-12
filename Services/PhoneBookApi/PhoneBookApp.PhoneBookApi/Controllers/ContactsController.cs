using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBookApp.PhoneBookApi.Dtos;
using PhoneBookApp.PhoneBookApi.Services;
using PhoneBookApp.PhoneBookApi.Validations;
using PhoneBookApp.Shared.ControllerBases;

namespace PhoneBookApp.PhoneBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : CustomBaseController
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllContactsAsync();

            return CreateActionResultInstance(contacts);
        }

        [HttpGet("{id}")]
        [ValidateBsonId]
        public async Task<IActionResult> GetById(string id)
        {
            var contacts = await _contactService.GetContactDetailByIdAsync(id);
            
            return CreateActionResultInstance(contacts);
        }

        [HttpPost]
        [ValidateName]
        public async Task<IActionResult> Create(ContactCreateDto contactCreateDto)
        {
            var response = await _contactService.CreateContactAsync(contactCreateDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        [ValidateBsonId]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _contactService.DeleteContactAsync(id);

            return CreateActionResultInstance(response);
        }

    }
}
