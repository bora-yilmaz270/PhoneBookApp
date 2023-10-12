using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PhoneBookApp.Shared.Enums;
//using PhoneBookApp.PhoneBookReportApi.Enums;

namespace PhoneBookApp.PhoneBookReportApi.Models
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? CompletedDate { get; set; }
        public ReportStatus Status { get; set; }       
    }
}
