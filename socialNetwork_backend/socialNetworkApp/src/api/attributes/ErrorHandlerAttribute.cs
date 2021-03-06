using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using socialNetworkApp.api.enums;
using socialNetworkApp.api.responses;
using socialNetworkApp.api.responses.error;

namespace System.Web.Http.Filters
{
    public class ValidationActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid && modelState.ErrorCount > 0)
            {
                Type validationErrorAnswerType = (actionContext.ActionDescriptor.FilterDescriptors
                    .FirstOrDefault(x => x.Filter.GetType() == typeof(MyProducesResponseTypeAttribute))
                    ?.Filter as MyProducesResponseTypeAttribute).Type;

                var validateErrorIntoResp =
                    Activator.CreateInstance(validationErrorAnswerType.GenericTypeArguments[2]) as ValidateError;
                validateErrorIntoResp.Name = "Validation error";
                validateErrorIntoResp.Summary = "Validation error";
                validateErrorIntoResp.Description = "Ошибка в валидации данных";
                validateErrorIntoResp.Type = ErrorType.validation;
                validateErrorIntoResp.Errors = new Dictionary<string, ValidateOneField>();
                foreach (var keyValuePair in modelState)
                {
                    var errorList = new List<OneFieldErrorValidate>() { };
                    foreach (var valueError in keyValuePair.Value.Errors)
                    {
                        errorList.Add(new OneFieldErrorValidate
                        {
                            Exception = valueError.Exception?.ToString(),
                            ErrorMessage = valueError.ErrorMessage
                        });
                    }

                    var ValObj = new ValidateOneField
                    {
                        FieldName = keyValuePair.Key,
                        Errors = errorList,
                        ValidationState = keyValuePair.Value.ValidationState
                    };
                    validateErrorIntoResp.Errors[keyValuePair.Key] = ValObj;
                }

                var resp = Activator.CreateInstance(validationErrorAnswerType, validateErrorIntoResp);

                actionContext.Result = new Resp(422, resp);
            }
        }
    }

    public class NonValidatingValidator : IObjectModelValidator
    {
        public void Validate(ActionContext actionContext, ValidationStateDictionary? validationState, string prefix,
            object model)
        {
        }
    }
}