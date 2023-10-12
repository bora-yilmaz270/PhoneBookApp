using PhoneBookApp.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Shared.Messages
{
    public class CreateReportEvent
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }       
        public ReportStatus Status { get; set; }
    }
}
