﻿using PhoneBookApp.PhoneBookApi.Dtos;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.PhoneBookApi.Services
{
    public interface IContactInfoService
    {      
        Task<Response<List<ContactInfoDto>>> GetAllContactInfosAsync();
        Task<Response<ContactInfoCreateDto>> CreateContactInfoAsync(ContactInfoCreateDto contactInfoDto);
        Task<Response<NoContent>> DeleteContactInfoAsync(string id);
        Task<Response<NoContent>> CreateReportAsync(string Id);
    }
}
