using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApp.Shared.Enums
{
    public enum ReportStatus
    {
        [Description("Preparing")]
        Preparing = 0,


        [Description("Completed")]
        Completed = 1
    }
    public static class ReportStatusExtensions
    {
        public static string ToText(this ReportStatus status)
        {
            switch (status)
            {
                case ReportStatus.Preparing:
                    return "Preparing";
                case ReportStatus.Completed:
                    return "Completed";
                default:
                    return status.ToString();
            }
        }
    }
}
