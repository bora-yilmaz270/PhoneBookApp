using MassTransit;
using Newtonsoft.Json;
using PhoneBookApp.PhoneBookReportApi.Models;
using PhoneBookApp.PhoneBookReportApi.Models.Responses;
using PhoneBookApp.PhoneBookReportApi.Services;
using PhoneBookApp.Shared.Messages;
using RestSharp;
using System.Text.Json.Nodes;

namespace PhoneBookApp.PhoneBookReportApi.Consumers
{
    public class CreateReportEventConsumer : IConsumer<CreateReportEvent>
    {
        private readonly IReportService _reportService;

        public CreateReportEventConsumer(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task Consume(ConsumeContext<CreateReportEvent> context)
        {

            var client = new RestClient("https://localhost:7066");

            var request = new RestRequest("api/ContactInfos");

            var response = client.ExecuteGet(request);

            var contactInfos = JsonConvert.DeserializeObject<ReportDetailResponse>(response.Content);

            var locations = contactInfos.Data.Where(x => x.InfoType == "Location") ?? null;

            var distinctLocations = locations.Select(x => x.Value).Distinct();

            List<ReportDetail> reportDetails = new List<ReportDetail>();

            foreach (var location in distinctLocations)
            {
                var contacts = locations
                  .Where(x => x.Value == location)
                  .Select(x => x.ContactId)
                  .Distinct();

                var phoneNumbers = contactInfos.Data
                  .Where(x => x.InfoType == "Phone" && contacts.Contains(x.ContactId))
                  .Select(x => x.Value)
                  .Distinct()
                .Count();

                ReportDetail reportDetail = new ReportDetail(context.Message.Id, location, contacts.Count(), phoneNumbers);
                reportDetails.Add(reportDetail);

            }

            var details = reportDetails
       .Select(x => new ReportDetail(x.ReportId, x.Location, x.ContactCount, x.PhoneNumberCount))
       .ToList();

            if (details.Count > 0)
            {
                await _reportService.CreateReportDetailsAsync(details);
                await _reportService.ReportCompletedAsync(details.FirstOrDefault()?.ReportId);
            }

        }
    }
}
