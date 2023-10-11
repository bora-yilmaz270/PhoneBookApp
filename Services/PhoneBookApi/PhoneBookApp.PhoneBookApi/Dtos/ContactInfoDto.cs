using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PhoneBookApp.PhoneBookApi.Models;

namespace PhoneBookApp.PhoneBookApi.Dtos
{
    public class ContactInfoDto
    {        
              
        public string ContactId { get; set; }
        public string InfoType { get; set; }
        public string Value { get; set; }
    }
}
