﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneBookApp.PhoneBookReportApi.Models.Responses;
using PhoneBookApp.PhoneBookReportApi.Services;
using PhoneBookApp.PhoneBookReportApi.Validations;
using PhoneBookApp.Shared.ControllerBases;
using RestSharp;

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

            var client = new RestClient("https://localhost:7066");

            var request = new RestRequest("api/ContactInfos/CreateReportByIdAsync/"+response.Data.Id);

            var contactInfosResponse = client.ExecuteGet(request);            

            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        [ValidateBsonId]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _reportService.GetReportByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpGet]
        [Route("/api/[controller]/GetDetailsByReportIdAsync/{id}")]
        [ValidateBsonId]
        public async Task<IActionResult> GetDetailsByReportIdAsync(string id)
        {
            var response = await _reportService.GetDetailsByReportIdAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpGet]
        [Route("/api/[controller]/GetAllReportDetailAsync")]
        public async Task<IActionResult> GetAllReportDetailAsync()
        {
            var contactInfos = await _reportService.GetAllReportDetailAsync();

            return CreateActionResultInstance(contactInfos);
        }


      

    }
}
