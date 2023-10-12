namespace PhoneBookApp.PhoneBookReportApi.Settings
{
    public class DatabaseSettings: IDatabaseSettings
    {
        public string ReportCollectionName { get; set; }
        public string ReportDetailCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
