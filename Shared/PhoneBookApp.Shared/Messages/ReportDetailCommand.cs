namespace PhoneBookApp.Shared.Messages
{
    public class ReportDetailCommand
    {
       
        public string ReportId { get; set; }

        public string Location { get; set; }

        public int ContactCount { get; set; }

        public int PhoneNumberCount { get; set; }
    }
}
