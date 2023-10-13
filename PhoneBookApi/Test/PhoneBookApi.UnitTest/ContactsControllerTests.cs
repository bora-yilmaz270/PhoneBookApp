using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBookApp.PhoneBookApi.Controllers;
using PhoneBookApp.PhoneBookApi.Dtos;
using PhoneBookApp.PhoneBookApi.Mapping;
using PhoneBookApp.PhoneBookApi.Models;
using PhoneBookApp.PhoneBookApi.Services;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApi.UnitTest
{
    public class ContactsControllerTests
    {
        private readonly Mock<IContactService> _contactServiceMock;
        private readonly IMapper _mapper;
        private readonly ContactsController _contactController;
        public ContactsControllerTests()
        {
            _contactServiceMock = new Mock<IContactService>();

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapping>()).CreateMapper();

            _contactController = new ContactsController(_contactServiceMock.Object);
        }

        [Fact]
        public async Task Get_All_Contacts_OK()
        {
            //Arrange
            _contactServiceMock.Setup(x => x.GetAllContactsAsync())
              .Returns(Task.FromResult(GetContactsFake()));

            //Act
            var actionResult = await _contactController.GetAll();

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.IsType<PhoneBookApp.Shared.Dtos.Response<List<ContactDto>>>(objectResult.Value);
        }

        [Fact]
        public async Task Get_Contact_By_Id_OK()
        {
            //Arrange
            var fakeContactId = "6525ca2a043053d3a91eb152";

            _contactServiceMock.Setup(x => x.GetContactDetailByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(GetDetailByIdFake(fakeContactId)));
            //Act
            var actionResult = await _contactController.GetById(fakeContactId);
            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (PhoneBookApp.Shared.Dtos.Response<ContactDetailDto>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Id, fakeContactId);
        }

        [Fact]
        public async Task Get_Contact_By_Id_Not_Found()
        {
            //Arrange
            var fakeContactId = "555f1f77bcf86cd79943zzzz";

            _contactServiceMock.Setup(x => x.GetContactDetailByIdAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(GetDetailByIdFake(fakeContactId)));

            //Act
            var actionResult = await _contactController.GetById(fakeContactId);

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_Contact_Created()
        {
            //Arrange
            ContactCreateDto contactDto = new ContactCreateDto
            {
                Name = "Bora",
                LastName = "Yılmaz",
                Company = "LuxCompany"
            };

            _contactServiceMock.Setup(x => x.CreateContactAsync(It.IsAny<ContactCreateDto>()))
              .Returns(Task.FromResult(FakeCreateMethod(contactDto)));

            //Act
            var actionResult = await _contactController.Create(contactDto);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (PhoneBookApp.Shared.Dtos.Response<ContactCreateDto>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.NotNull(response);
            Assert.NotNull(response.Data);

        }
        [Fact]
        public async Task Create_Contact_Created_Not_Valid()
        {
            //Arrange
            ContactCreateDto contactDto = new ContactCreateDto
            {
                Name = "",
                LastName = "Yılmaz",
                Company = "LuxCompany"
            };

            _contactServiceMock.Setup(x => x.CreateContactAsync(It.IsAny<ContactCreateDto>()))
              .Returns(Task.FromResult(FakeCreateMethod(contactDto)));

            //Act
            var actionResult = await _contactController.Create(contactDto);

            //Assert
            var objectResult = (ObjectResult)actionResult;
            var response = (PhoneBookApp.Shared.Dtos.Response<ContactCreateDto>)objectResult.Value;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);


        }

        [Fact]
        public async Task Delete_Contact_OK()
        {
            //Arrange
            _contactServiceMock.Setup(x => x.DeleteContactAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(FakeDeleteContact("887f1f77bcf86cd799439092")));

            //Act
            var actionResult = await _contactController.Delete("887f1f77bcf86cd799439092");

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.NoContent);
        }
        [Fact]
        public async Task Delete_Contact_Not_OK()
        {
            //Arrange
            _contactServiceMock.Setup(x => x.DeleteContactAsync(It.IsAny<string>()))
              .Returns(Task.FromResult(FakeDeleteContact("887f1f77bcf86cd799433344")));

            //Act
            var actionResult = await _contactController.Delete("887f1f77bcf86cd799433344");

            //Assert
            var objectResult = (ObjectResult)actionResult;

            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }

        private PhoneBookApp.Shared.Dtos.Response<List<ContactDto>> GetContactsFake()
        {
            List<ContactDto> contacts = new List<ContactDto>();

            for (int i = 1; i <= 3; i++)
            {
                string name = $"TestName{i}";
                string lastName = $"TestLastName{i}";
                string company = $"TestCompany{i}";

                ContactDto contact = new ContactDto()
                {
                    Id = $"587f1f77bcf86fg7994311{i}",
                    Name = name,
                    LastName = lastName,
                    Company = company
                };
                contacts.Add(contact);
            }

            return PhoneBookApp.Shared.Dtos.Response<List<ContactDto>>.Success(_mapper.Map<List<ContactDto>>(contacts), 200);
        }
        private PhoneBookApp.Shared.Dtos.Response<ContactDetailDto> GetDetailByIdFake(string id)
        {
            var fakeContactId = "6525ca2a043053d3a91eb152";
            Contact contact = new Contact();
            if (fakeContactId == id)
            {

                contact.Id = id;
                contact.Name = "Bora";
                contact.LastName = "Yılmaz";
                contact.Company = "BigCompany";
                contact.ContactInfos = new List<ContactInfo>
           {
                new ContactInfo
                {
                    Id = "6526f3f12e2a1937aaf17139",
                    ContactId = "6525ca2a043053d3a91eb152",
                    InfoType = "Phone",
                    Value = "554-381-8430"
                },
                new ContactInfo
                {
                    Id = "6528526e5f31c70b0c7c3c69",
                    ContactId = "6525ca2a043053d3a91eb152",
                    InfoType = "Phone",
                    Value = "554-381-8430"
                }
            };
                return PhoneBookApp.Shared.Dtos.Response<ContactDetailDto>.Success(_mapper.Map<ContactDetailDto>(contact), 200);
            }
            return PhoneBookApp.Shared.Dtos.Response<ContactDetailDto>.Fail("Not found", 404);

        }
        private PhoneBookApp.Shared.Dtos.Response<ContactCreateDto> FakeCreateMethod(ContactCreateDto contactDto)
        {
            if (string.IsNullOrEmpty(contactDto.Name))
            {
                return PhoneBookApp.Shared.Dtos.Response<ContactCreateDto>.Fail("Name alanı boş olamaz.", 400);
            }
            var model = _mapper.Map<Contact>(contactDto);
            return PhoneBookApp.Shared.Dtos.Response<ContactCreateDto>.Success(_mapper.Map<ContactCreateDto>(model), 200);
        }
        private PhoneBookApp.Shared.Dtos.Response<NoContent> FakeDeleteContact(string id)
        {
            string findfakeid = "887f1f77bcf86cd799439092";

            if (id == findfakeid)
            {
                return PhoneBookApp.Shared.Dtos.Response<NoContent>.Success(204);
            }
            else
            {
                return PhoneBookApp.Shared.Dtos.Response<NoContent>.Fail("Not found", 404);
            }
        }





    }
}
