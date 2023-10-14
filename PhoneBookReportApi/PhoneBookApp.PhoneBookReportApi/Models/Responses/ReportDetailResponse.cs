namespace PhoneBookApp.PhoneBookReportApi.Models.Responses
{
    public class ReportDetailResponse
    {
        public List<ContactInfo> Data { get; set; }
        public object Errors { get; set; }
    }

    public class ContactInfo
    {
        public string Id { get; set; }
        public string ContactId { get; set; }
        public string InfoType { get; set; }
        public string Value { get; set; }
    }
}
