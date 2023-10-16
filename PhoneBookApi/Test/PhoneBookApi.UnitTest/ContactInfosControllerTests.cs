using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBookApp.PhoneBookApi.Controllers;
using PhoneBookApp.PhoneBookApi.Dtos;
using PhoneBookApp.PhoneBookApi.Mapping;
using PhoneBookApp.PhoneBookApi.Models;
using PhoneBookApp.PhoneBookApi.Services;
using PhoneBookApp.Shared.Dtos;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PhoneBookApi.UnitTest
{
    public class ContactInfosControllerTests
    {
        private readonly Mock<IContactInfoService> _contactInfoServiceMock;
        private readonly IMapper _mapper;
        private readonly ContactInfosController _ContactInfosControllerController;
        public ContactInfosControllerTests()
        {
            _contactInfoServiceMock = new Mock<IContactInfoService>();

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapping>()).CreateMapper();

            _ContactInfosControllerController = new ContactInfosController(_contactInfoServiceMock.Object);
        }

        [Fact]
        public async Task Get_All_Contact_Infos_OK()
        {
            //Arrange
            _contactInfoServiceMock.Setup(x => x.GetAllContactInfosAsync())
              .Returns(Task.FromResult(GetAllContactInfoByContactId(true)));
            //Act
            var actionResult = await _ContactInfosControllerController.GetAll();
            //Assert
            var objectResult = (ObjectResult)actionResult;
            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.IsType<PhoneBookApp.Shared.Dtos.Response<List<ContactInfoDto>>>(objectResult.Value);
        }
        [Fact]
        public async Task Get_All_Contact_Infos_Not_Ok()
        {
            //Arrange
            _contactInfoServiceMock.Setup(x => x.GetAllContactInfosAsync())
              .Returns(Task.FromResult(GetAllContactInfoByContactId(false)));
            //Act
            var actionResult = await _ContactInfosControllerController.GetAll();
            //Assert
            var objectResult = (ObjectResult)actionResult;
            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
            
        }
        [Fact]
        public async Task Create_Contact_Info_Phone_OK()
        {
            ContactInfoCreateDto contactInfo = new ContactInfoCreateDto
            {
                ContactId = "6528548f431a5559979c38c3",
                InfoType = "Phone",
                Value = "554-381-8430"
            };

            _contactInfoServiceMock.Setup(x => x.CreateContactInfoAsync(It.IsAny<ContactInfoCreateDto>()))
             .Returns(Task.FromResult(CreateContactInfoForPhoneAndContactIdAsyncFake(contactInfo)));

            //Act
            var actionResult = await _ContactInfosControllerController.Create(contactInfo);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (PhoneBookApp.Shared.Dtos.Response<ContactInfoCreateDto>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
        }
        [Fact]
        public async Task Create_Contact_Info_Phone_Not_OK()
        {
            ContactInfoCreateDto contactInfo = new ContactInfoCreateDto
            {
                ContactId = "6528548f431a5559979c38c3",
                InfoType = "Phone",
                Value = "554-3818430"
            };
            _contactInfoServiceMock.Setup(x => x.CreateContactInfoAsync(It.IsAny<ContactInfoCreateDto>()))
            .Returns(Task.FromResult(CreateContactInfoForPhoneAndContactIdAsyncFake(contactInfo)));

            //Act
            var actionResult = await _ContactInfosControllerController.Create(contactInfo);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (PhoneBookApp.Shared.Dtos.Response<ContactInfoCreateDto>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
         
        }
        [Fact]
        public async Task Create_Contact_Info_ContactId_OK()
        {
            ContactInfoCreateDto contactInfo = new ContactInfoCreateDto
            {
                ContactId = "6528548f431a5559979c38c3",
                InfoType = "Phone",
                Value = "554-381-8430"
            };
            _contactInfoServiceMock.Setup(x => x.CreateContactInfoAsync(It.IsAny<ContactInfoCreateDto>()))
             .Returns(Task.FromResult(CreateContactInfoForPhoneAndContactIdAsyncFake(contactInfo)));

            //Act
            var actionResult = await _ContactInfosControllerController.Create(contactInfo);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (PhoneBookApp.Shared.Dtos.Response<ContactInfoCreateDto>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
        }
        [Fact]
        public async Task Create_Contact_Info_ContactId_Not_OK()
        {
            ContactInfoCreateDto contactInfo = new ContactInfoCreateDto
            {
                ContactId = "6528548f431a5559979c555c3",
                InfoType = "Phone",
                Value = "554-381-8430"
            };
            _contactInfoServiceMock.Setup(x => x.CreateContactInfoAsync(It.IsAny<ContactInfoCreateDto>()))
            .Returns(Task.FromResult(CreateContactInfoForPhoneAndContactIdAsyncFake(contactInfo)));

            //Act
            var actionResult = await _ContactInfosControllerController.Create(contactInfo);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (PhoneBookApp.Shared.Dtos.Response<ContactInfoCreateDto>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
         
        }

        [Fact]
        public async Task Delete_Contact_OK()
        {
            //Arrange
            _contactInfoServiceMock.Setup(x => x.DeleteContactInfoAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(FakeDeleteContact("6526f3f12e2a1937aaf17139")));

            //Act
            var actionResult = await _ContactInfosControllerController.Delete("6526f3f12e2a1937aaf17139");

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.NoContent);
        }
        [Fact]
        public async Task Delete_Contact_Not_OK()
        {
            //Arrange
            _contactInfoServiceMock.Setup(x => x.DeleteContactInfoAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(FakeDeleteContact("887f1f77bcf86cd799433344")));

            //Act
            var actionResult = await _ContactInfosControllerController.Delete("887f1f77bcf86cd799433344");

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_ReportBy_Id_OK()
        {
            //Arrange
            _contactInfoServiceMock.Setup(x => x.CreateReportAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(FakeReportIdContact("6526f3f12e2a1937aaf17139")));          

            //Act
            var actionResult = await _ContactInfosControllerController.CreateReportByIdAsync("6526f3f12e2a1937aaf17139");

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        private PhoneBookApp.Shared.Dtos.Response<List<ContactInfoDto>> GetAllContactInfoByContactId(bool IsExsist)
        {
           
          
            List <ContactInfo> contactinfoList = new List<ContactInfo>();
            if (IsExsist)
            {

              var model1=  new ContactInfo
                {
                    Id = "6526f3f12e2a1937aaf17139",
                    ContactId = "6525ca2a043053d3a91eb152",
                    InfoType = "Phone",
                    Value = "554-381-8430"
                };
                var model2=   new ContactInfo
                {
                    Id = "6528526e5f31c70b0c7c3c69",
                    ContactId = "6525ca2a043053d3a91eb152",
                    InfoType = "Phone",
                    Value = "554-381-8430"
                };

                contactinfoList.Add(model1);
                contactinfoList.Add(model2);

                return PhoneBookApp.Shared.Dtos.Response<List<ContactInfoDto>>.Success(_mapper.Map<List<ContactInfoDto>>(contactinfoList), 200);
            }
            return PhoneBookApp.Shared.Dtos.Response<List<ContactInfoDto>>.Fail("Not found", 404);

        }

        private PhoneBookApp.Shared.Dtos.Response<ContactInfoCreateDto> CreateContactInfoForPhoneAndContactIdAsyncFake(ContactInfoCreateDto contactInfoDto)
        {
            string fakecontactId = "6528548f431a5559979c38c3";
            if (fakecontactId== contactInfoDto.ContactId && IsValidPhoneNumber(contactInfoDto.Value))
            {
                var contactInfo = _mapper.Map<ContactInfo>(contactInfoDto);
                return PhoneBookApp.Shared.Dtos.Response<ContactInfoCreateDto>.Success(_mapper.Map<ContactInfoCreateDto>(contactInfo), 200);
            }

            return PhoneBookApp.Shared.Dtos.Response<ContactInfoCreateDto>.Fail( "Geçersiz Value formatı." , 400); 
        }
        private PhoneBookApp.Shared.Dtos.Response<NoContent> FakeDeleteContact(string id)
        {
            string findfakeid = "6526f3f12e2a1937aaf17139";

            if (id == findfakeid)
            {
                return PhoneBookApp.Shared.Dtos.Response<NoContent>.Success(204);
            }
            else
            {
                return PhoneBookApp.Shared.Dtos.Response<NoContent>.Fail("Not found", 404);
            }
        }
        private PhoneBookApp.Shared.Dtos.Response<NoContent> FakeReportIdContact(string id)
        {
            string findfakeid = "6526f3f12e2a1937aaf17139";

            if (id == findfakeid)
            {
                return PhoneBookApp.Shared.Dtos.Response<NoContent>.Success(200);
            }
            else
            {
                return PhoneBookApp.Shared.Dtos.Response<NoContent>.Fail("Not found", 404);
            }
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Telefon numarası formatı kontrolü burada yapılacak
            var regex = new Regex(@"^\d{3}-\d{3}-\d{4}$");
            return regex.IsMatch(phoneNumber);
        }
    }
}
