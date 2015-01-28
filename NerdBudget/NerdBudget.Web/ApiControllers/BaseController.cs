using System.Collections.Generic;
using System.Web.Http;
using FluentValidation.Results;

namespace NerdBudget.Web.ApiControllers
{
    public class BaseController : ApiController
    {
        #region Helpers

        protected IHttpActionResult BadRequest(IEnumerable<ValidationFailure> errors)
        {
            foreach (ValidationFailure vf in errors)
            {
                ModelState.AddModelError(vf.PropertyName, vf.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        #endregion
    }
}