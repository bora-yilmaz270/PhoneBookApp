using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PhoneBookApp.PhoneBookApi.Dtos;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.PhoneBookApi.Validations
{
    public class ValidateNameAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var contactDto = context.ActionArguments["contactCreateDto"] as ContactCreateDto;

            if (string.IsNullOrEmpty(contactDto.Name))
            {
                context.Result = new BadRequestObjectResult(Response<NoContent>.Fail("Name alanı boş olamaz.", 400));
            }
        }
    }
}
