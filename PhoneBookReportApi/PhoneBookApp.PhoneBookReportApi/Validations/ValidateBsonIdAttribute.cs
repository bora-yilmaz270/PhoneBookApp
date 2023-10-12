using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.PhoneBookReportApi.Validations
{
    public class ValidateBsonIdAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var id = context.ActionArguments["id"] as string;

            if (id == null || !ObjectId.TryParse(id, out _))
            {
                context.Result = new BadRequestObjectResult(Response<NoContent>.Fail("Geçersiz BsonId formatı.", 400));
            }
        }
    }
}
