namespace PhoneBookApp.Shared.Messages
{
    public class ReportDetailEvent
    {
       
        public string ReportId { get; set; }

        public string Location { get; set; }

        public int ContactCount { get; set; }

        public int PhoneNumberCount { get; set; }
    }
}
