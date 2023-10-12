using MassTransit;
using MassTransit.Transports;
using MongoDB.Driver;
using PhoneBookApp.PhoneBookApi.Models;
using PhoneBookApp.PhoneBookApi.Services;
using PhoneBookApp.Shared.Messages;
using System.Linq;
using Mass = MassTransit;
namespace PhoneBookApp.PhoneBookApi.Consumer
{


    public class CreateReportEventConsumer : IConsumer<CreateReportEvent>
    {
        private readonly IContactService _contactService;
        private readonly IContactInfoService _contactInfoService;
        private readonly Mass.IPublishEndpoint _publishEndpoint;

        public CreateReportEventConsumer(IContactService contactService, IContactInfoService contactInfoService, IPublishEndpoint publishEndpoint)
        {
            _contactService = contactService;
            _contactInfoService = contactInfoService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<CreateReportEvent> context)
        {           

            List<ReportDetailEvent> reportDetailEvents = new List<ReportDetailEvent>(); 

            var contactInfos = await _contactInfoService.GetAllContactInfosAsync();            

            var locations = contactInfos.Data.Where(x => x.InfoType == "Location")?? null;         

            var distinctLocations = locations.Select(x => x.Value).Distinct();                      

            foreach (var location in distinctLocations)
            {
                ReportDetailEvent reportDetailEvent = new ReportDetailEvent();
                reportDetailEvent.ReportId =  context.Message.Id;
                
                var contacts = locations
                  .Where(x => x.Value == location)
                  .Select(x => x.ContactId)
                  .Distinct();
                
                var phoneNumbers = contactInfos.Data
                  .Where(x => x.InfoType == "Phone" && contacts.Contains(x.ContactId))
                  .Select(x => x.Value)
                  .Distinct()
                .Count();

                reportDetailEvent.ContactCount = contacts.Count();
                reportDetailEvent.PhoneNumberCount = phoneNumbers;
                reportDetailEvent.Location = location;
                reportDetailEvents.Add(reportDetailEvent);               

            }

            ListDetailEvent listDetailEvent = new ListDetailEvent();

            listDetailEvent.ReportDetailEvents= reportDetailEvents;

            await _publishEndpoint.Publish<ListDetailEvent>(listDetailEvent);
        }
    }
}
