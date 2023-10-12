using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PhoneBookApp.PhoneBookApi.Dtos;
using PhoneBookApp.Shared.Dtos;
using System.Text.RegularExpressions;

namespace PhoneBookApp.PhoneBookApi.Validations
{
    public class ValidateContactInfoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dto = context.ActionArguments["contactInfoCreateDto"] as ContactInfoCreateDto;

            if (dto == null)
            {
                context.Result = new BadRequestObjectResult(Response<NoContent>.Fail(new List<string> { "Alanlar boş olamaz" },400));
                return;
            }

            if (string.IsNullOrEmpty(dto.ContactId) || !IsValidBsonId(dto.ContactId))
            {
                context.Result = new BadRequestObjectResult(Response<NoContent>.Fail(new List<string> { "ContactId bson tipinde olmalı" }, 400));
                return;
            }

            if (string.IsNullOrEmpty(dto.InfoType) || !IsValidInfoType(dto.InfoType))
            {
                context.Result = new BadRequestObjectResult(Response<NoContent>.Fail(new List<string> { "Geçersiz InfoType değeri." }, 400));
                return;
            }

            if (string.IsNullOrEmpty(dto.Value) || !IsValidValue(dto.InfoType, dto.Value))
            {
                context.Result = new BadRequestObjectResult(Response<NoContent>.Fail(new List<string> { "Geçersiz Value formatı." }, 400));
                return;
            }

            base.OnActionExecuting(context);
        }              
        private bool IsValidInfoType(string infoType)
        {
            return infoType == "Phone" || infoType == "Email" || infoType == "Location";
        }
        private bool IsValidValue(string infoType, string value)
        {
            if (infoType == "Phone")
            {
                return IsValidPhoneNumber(value);
            }
            else if (infoType == "Email")
            {
                return IsValidEmail(value);
            }
            else if (infoType == "Location")
            {
                return value.Length <= 30;
            }

            return false;
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Telefon numarası formatı kontrolü burada yapılacak
            var regex = new Regex(@"^\d{3}-\d{3}-\d{4}$");
            return regex.IsMatch(phoneNumber);
        }
        private bool IsValidBsonId(string contactId)
        {
            try
            {
                MongoDB.Bson.ObjectId.Parse(contactId);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
            return regex.IsMatch(email);
        }

       
    }
}
