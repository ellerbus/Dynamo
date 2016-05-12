using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using FluentValidation;
using FluentValidation.Results;

namespace ShortFuze.WebApp
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string err = actionExecutedContext.Exception.Message;

            if (actionExecutedContext.Exception is KeyNotFoundException)
            {
                HttpResponseMessage msg = actionExecutedContext.Request
                    .CreateErrorResponse(HttpStatusCode.NotFound, err);

                throw new HttpResponseException(msg);
            }

            if (actionExecutedContext.Exception is ValidationException)
            {
                ValidationException ex = actionExecutedContext.Exception as ValidationException;

                ModelStateDictionary modelState = actionExecutedContext.ActionContext.ModelState;

                modelState.Clear();

                foreach (ValidationFailure vf in ex.Errors)
                {
                    modelState.AddModelError(vf.PropertyName, vf.ErrorMessage);
                }

                HttpResponseMessage msg = actionExecutedContext.Request
                    .CreateErrorResponse(HttpStatusCode.Conflict, modelState);

                throw new HttpResponseException(msg);
            }

            if (actionExecutedContext.Exception is SecurityException)
            {
                HttpResponseMessage msg = actionExecutedContext.Request
                    .CreateErrorResponse(HttpStatusCode.Forbidden, err);

                throw new HttpResponseException(msg);
            }
        }
    }
}