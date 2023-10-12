using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PhoneBookApp.Shared.Enums;
//using PhoneBookApp.PhoneBookReportApi.Enums;

namespace PhoneBookApp.PhoneBookReportApi.Dtos
{
    public class ReportDto
    {       
        public string Id { get; set; }       
        public DateTime CreatedDate { get; set; }      
        public DateTime? CompletedDate { get; set; }
        public ReportStatus Status { get; set; }
    }
}
