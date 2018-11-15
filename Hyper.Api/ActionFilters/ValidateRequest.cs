using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Hyper.Api.Responses;
using Hyper.Domain.Messages;

namespace Hyper.Api.ActionFilters
{

    public class ValidateRequestAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                // 400 Bad Request
                foreach (var value in filterContext.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        if (error.Exception != null)
                        {
                            var errorResponse = new ErrorResponse(nameof(Messages.InvalidRequest), 400, Messages.InvalidRequest);
                            filterContext.Result = new ObjectResult(errorResponse) { StatusCode = 400 };
                            return;
                        }
                    }
                }

                // 422 Unprocessable Entity
                var errors = filterContext.ModelState.Where(x => x.Value.Errors.Count > 0).ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

                var validationsResponse = new ValidationResponse(nameof(Messages.ValidationFailed), 422, Messages.ValidationFailed);
                var validationErrorsResponse = new List<ValidationErrorResponse>();
                foreach (var error in errors)
                {
                    foreach (var value in error.Value)
                    {
                        validationErrorsResponse.Add(new ValidationErrorResponse(nameof(value), error.Key, value));
                    }
                }

                filterContext.Result = new ObjectResult(validationsResponse) { StatusCode = 422 };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}