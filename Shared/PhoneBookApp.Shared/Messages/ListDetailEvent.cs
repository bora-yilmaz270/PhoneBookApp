using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Shared.Messages
{
    public class ListDetailEvent
    {
        public List<ReportDetailEvent> ReportDetailEvents { get; set; }
    }
}
