using PhoneBookApp.Shared.Enums;
using System.Text.Json.Serialization;

namespace PhoneBookApp.PhoneBookReportApi.Dtos
{
    public class ReportDto
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        [JsonIgnore]
        public ReportStatus Status { get; set; }

        public string StatusText => Status.ToText();
    }
   
}
