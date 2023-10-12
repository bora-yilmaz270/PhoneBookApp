using MassTransit;
using MassTransit.Transports;
using MongoDB.Bson;
using PhoneBookApp.PhoneBookReportApi.Dtos;
using PhoneBookApp.PhoneBookReportApi.Models;
using PhoneBookApp.PhoneBookReportApi.Services;
using PhoneBookApp.Shared.Messages;
using System.Linq;
using System.Text.Json;

namespace PhoneBookApp.PhoneBookReportApi.Consumers
{
    public class ReportDetailEventConsumer : IConsumer<ListDetailEvent> //değişti
    {
        private readonly IReportService _reportService;

        public ReportDetailEventConsumer(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task Consume(ConsumeContext<ListDetailEvent> context) //değişti
        {
            var details = context.Message.ReportDetailEvents
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
