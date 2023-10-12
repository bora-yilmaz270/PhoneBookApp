using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBookApp.PhoneBookReportApi.Services;
using PhoneBookApp.Shared.ControllerBases;

namespace PhoneBookApp.PhoneBookReportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : CustomBaseController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contactInfos = await _reportService.GetAllReportsAsync();
            
            return CreateActionResultInstance(contactInfos);
        }

        [HttpPost]      
        public async Task<IActionResult> Create()
        {
            var response = await _reportService.CreateReportAsync();

            return CreateActionResultInstance(response);
        }
    }
}
