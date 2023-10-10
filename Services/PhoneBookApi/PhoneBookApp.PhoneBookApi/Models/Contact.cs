using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace PhoneBookApp.PhoneBookApi.Models
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}
