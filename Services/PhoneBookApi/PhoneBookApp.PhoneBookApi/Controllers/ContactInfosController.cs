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
    public class ContactInfosController : CustomBaseController
    {
        private readonly IContactInfoService _contactInfoService;

        public ContactInfosController(IContactInfoService contactInfoService)
        {
            _contactInfoService = contactInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contactInfos = await _contactInfoService.GetAllContactInfosAsync();

            return CreateActionResultInstance(contactInfos);
        }

        [HttpPost]
        [ValidateContactInfo]
        public async Task<IActionResult> Create([FromBody] ContactInfoCreateDto contactInfoCreateDto)
        {


            var response = await _contactInfoService.CreateContactInfoAsync(contactInfoCreateDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        [ValidateBsonId]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _contactInfoService.DeleteContactInfoAsync(id);

            return CreateActionResultInstance(response);
        }


    }
}
