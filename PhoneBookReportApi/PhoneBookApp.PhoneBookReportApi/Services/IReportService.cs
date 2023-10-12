using PhoneBookApp.PhoneBookReportApi.Dtos;
using PhoneBookApp.PhoneBookReportApi.Models;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.PhoneBookReportApi.Services
{
    public interface IReportService
    {
        Task<Response<ReportCreateDto>> CreateReportAsync();
        Task<Response<List<ReportDto>>> GetAllReportsAsync();
        Task<Response<ReportDto>> GetReportByIdAsync(string id);
        Task<Response<List<ReportDetailDto>>> GetDetailsByReportIdAsync(string reportId);
        Task<Response<List<ReportDetailDto>>> GetAllReportDetailAsync();
        Task CreateReportDetailsAsync(IList<ReportDetail> reportDetails);
        Task ReportCompletedAsync(string id);
    }
}
