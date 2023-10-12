namespace PhoneBookApp.PhoneBookReportApi.Dtos
{
    public class ReportDetailCreateDto
    {

        public ReportDetailCreateDto(string reportId, string location, int contactCount, int phoneNumberCount)
        {
            ReportId = reportId;
            Location = location;
            ContactCount = contactCount;
            PhoneNumberCount = phoneNumberCount;
        }

        public string ReportId { get; set; }

        public string Location { get; set; }

        public int ContactCount { get; set; }

        public int PhoneNumberCount { get; set; }
    }
}
