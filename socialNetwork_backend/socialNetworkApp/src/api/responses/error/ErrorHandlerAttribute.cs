using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using socialNetworkApp.api.responses;

namespace System.Web.Http.Filters
{
    public class ValidationActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {
                // TODO: проверить работоспособность этой штуки.
                // TODO: Найти способ валидировать ошибки при создании модели
                actionContext.Result = new Resp(422, modelState);
            }
            // .CreateErrorResponse(HttpStatusCode.BadRequest, modelState);
        }
    }
    
    public class NonValidatingValidator : IObjectModelValidator
    {
        public void Validate(ActionContext actionContext, ValidationStateDictionary? validationState, string prefix, object model)
        {
        }
    }
}

// public class ErrorHandlerAttribute
// {
//     
// }