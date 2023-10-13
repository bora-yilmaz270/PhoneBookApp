using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBookApp.PhoneBookReportApi.Controllers;
using PhoneBookApp.PhoneBookReportApi.Dtos;
using PhoneBookApp.PhoneBookReportApi.Mapping;
using PhoneBookApp.PhoneBookReportApi.Models;
using PhoneBookApp.PhoneBookReportApi.Services;
using PhoneBookApp.Shared.Dtos;
using PhoneBookApp.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBookReportApi.UnitTest
{
    public class ReportControllerTests
    {
        private readonly Mock<IReportService> _reportServiceMock;
        private readonly IMapper _mapper;
        private readonly ReportController _reportController;
        public ReportControllerTests()
        {
            _reportServiceMock = new Mock<IReportService>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapping>()).CreateMapper();
            _reportController = new ReportController(_reportServiceMock.Object);
        }

        [Fact]
        public async Task Get_All_Reports_OK()
        {
            //Arrange
            _reportServiceMock.Setup(x => x.GetAllReportsAsync())
              .Returns(Task.FromResult(GetReportsFake()));

            //Act
            var actionResult = await _reportController.GetAll();

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);

            Assert.IsType<Response<List<ReportDto>>>(objectResult.Value);
        }

        [Fact]
        public async Task Create_Report_Created()
        {

            _reportServiceMock.Setup(x => x.CreateReportAsync())
              .Returns(Task.FromResult(CreateReportAsyncFake(true)));

            //Act
            var actionResult = await _reportController.Create();

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (Response<ReportCreateDto>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);

        }

        [Fact]
        public async Task Get_report_by_id_OK()
        {
            //Arrange
            var fakeReportId = "65272acc683adc301880d347";

            _reportServiceMock.Setup(x => x.GetReportByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(GetReportByIdAsync(fakeReportId)));

            //Act
            var actionResult = await _reportController.GetById(fakeReportId);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (Response<ReportDto>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);

        }
      
        [Fact]
        public async Task Get_Details_ByReportId_Async()
        {
            //Arrange
            string fakeReportId = "65282fa8fa4c0fec052fe5f3";

            _reportServiceMock.Setup(x => x.GetDetailsByReportIdAsync(It.IsAny<string>()))
                       .Returns(Task.FromResult(GetDetailsByReportIdAsysnc(fakeReportId)));

            //Act
            var actionResult = await _reportController.GetDetailsByReportIdAsync(fakeReportId);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (Response<List<ReportDetailDto>>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);

        }

        [Fact]
        public async Task Get_All_Report_DetailAsync()
        {           

            _reportServiceMock.Setup(x => x.GetAllReportDetailAsync())
                       .Returns(Task.FromResult(GetAllReportDetailFake()));

            //Act
            var actionResult = await _reportController.GetAllReportDetailAsync();

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (Response<List<ReportDetailDto>>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);

        }

        private Response<List<ReportDto>> GetReportsFake()
        {
            List<Report> responseData = new List<Report>
                    {
                        new Report
                        {
                            Id = "65272acc683adc301880d347",
                            CreatedDate = DateTime.Parse("2023-10-11T23:07:56.146Z"),
                            CompletedDate = null,
                            Status = 0
                        },
                        new Report
                        {
                            Id = "652734af6c44cb4488e34354",
                            CreatedDate = DateTime.Parse("2023-10-11T23:49:53.988Z"),
                            CompletedDate = null,
                             Status = 0
                        }
                    };
            return Response<List<ReportDto>>.Success(_mapper.Map<List<ReportDto>>(responseData), 200);
        }
        private Response<ReportCreateDto> CreateReportAsyncFake(bool ısOk)
        {
            var reportCreateDto = new ReportCreateDto() { CreatedDate = DateTime.UtcNow, Status = ReportStatus.Preparing };

            var report = _mapper.Map<Report>(reportCreateDto);

            return Response<ReportCreateDto>.Success(_mapper.Map<ReportCreateDto>(report), 200);
        }
        private Response<ReportDto> GetReportByIdAsync(string id)
        {
            Report responseData = new Report
            {
                Id = "65272acc683adc301880d347",
                CreatedDate = DateTime.Parse("2023-10-11T23:07:56.146Z"),
                CompletedDate = null,
                Status = 0
            };
            
            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(responseData), 200);
        }
        private Response<List<ReportDetailDto>> GetDetailsByReportIdAsysnc(string reportId)
        {
            List<ReportDetail> reportDetails = new List<ReportDetail>();
            ReportDetail model = new ReportDetail("65282fa8fa4c0fec052fe5f3","kayseri", 2, 1);
            reportDetails.Add(model);
            return Response<List<ReportDetailDto>>.Success(_mapper.Map<List<ReportDetailDto>>(reportDetails), 200);
        }
        private Response<List<ReportDetailDto>> GetAllReportDetailFake()
        {

            List<ReportDetail> reportDetails = new List<ReportDetail>();
            ReportDetail model1 = new ReportDetail("65276bae28395ed7cf044097", "kayseri", 2, 1);
            ReportDetail model2 = new ReportDetail("65276bcb28395ed7cf044099", "kayseri", 2, 1);
            ReportDetail model3 = new ReportDetail("65276c1628395ed7cf04409b", "kayseri", 2, 1);

            reportDetails.Add(model1);
            reportDetails.Add(model2);
            reportDetails.Add(model3);

            return Response<List<ReportDetailDto>>.Success(_mapper.Map<List<ReportDetailDto>>(reportDetails), 200);
        }

    }


}
