using PhoneBookApp.PhoneBookReportApi.Dtos;
using PhoneBookApp.PhoneBookReportApi.Models;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.PhoneBookReportApi.Services
{
    public interface IReportService
    {
        Task<Response<ReportCreateDto>> CreateReportAsync();
        Task<Response<List<ReportDto>>> GetAllReportsAsync();
        Task<Report> GetReportByIdAsync(string id);       
        Task<IList<ReportDetail>> GetDetailsByReportIdAsync(string reportId);
        Task CreateReportDetailsAsync(IList<ReportDetail> reportDetails);
        Task ReportCompletedAsync(string id);
    }
}
