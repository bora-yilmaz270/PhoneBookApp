using AutoMapper;
using PhoneBookApp.PhoneBookReportApi.Dtos;
using PhoneBookApp.PhoneBookReportApi.Models;

namespace PhoneBookApp.PhoneBookReportApi.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<Report, ReportCreateDto>().ReverseMap();
            CreateMap<ReportDetail, ReportDetailDto>().ReverseMap();                  

        }
    }
}
