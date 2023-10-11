using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PhoneBookApp.PhoneBookApi.Models
{
    public class ContactInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContactId { get; set; }
        public string InfoType { get; set; }
        public string Value { get; set; }
    }
}
