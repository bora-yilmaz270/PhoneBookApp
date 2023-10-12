using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PhoneBookApp.PhoneBookReportApi.Dtos
{
    public class ReportDetailDto
    {        
        public string Id { get; set; }
      
        public string ReportId { get; set; }
       
        public string Location { get; set; }
     
        public int ContactCount { get; set; }
      
        public int PhoneNumberCount { get; set; }
    }
}
