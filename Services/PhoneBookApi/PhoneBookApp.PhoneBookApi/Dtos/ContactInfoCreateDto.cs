using PhoneBookApp.PhoneBookApi.Models;
using PhoneBookApp.PhoneBookApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookApp.PhoneBookApi.Dtos
{
    public class ContactInfoCreateDto
    {       
        public string ContactId { get; set; }      
        public string InfoType { get; set; }      
        public string Value { get; set; }
    }
}
