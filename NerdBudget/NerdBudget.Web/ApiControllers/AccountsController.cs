using System.Web.Http;
using System.Web.Http.Description;
using FluentValidation;
using NerdBudget.Core.Models;
using NerdBudget.Core.Services;
using Newtonsoft.Json;

namespace NerdBudget.Web.ApiControllers
{
    ///	<summary>
    /// Represents a basic controller for Account
    ///	</summary>
    [RoutePrefix("api/accounts"), Authorize]
    public class AccountsController : BaseController
    {
        #region Members

        private IAccountService _service;

        #endregion

        #region Contructors

        public AccountsController(IAccountService service)
        {
            _service = service;
        }

        #endregion

        #region Insert/Update/Delete

        // POST: api/account
        [HttpPost, Route(""), ResponseType(typeof(Account))]
        public IHttpActionResult Post([FromBody]Account account)
        {
            try
            {
                _service.Insert(account);

                JsonSerializerSettings jss = GetPayloadSettings();

                return Json(account, jss);
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        // PUT: api/account/5
        [HttpPut, Route("{id}")]
        public IHttpActionResult Put(string id, [FromBody]Account account)
        {
            Account model = _service.Get(id);

            if (model == null)
            {
                return NotFound();
            }

            model.Name = account.Name;

            try
            {
                _service.Update(model);

                JsonSerializerSettings jss = GetPayloadSettings();

                return Json(model, jss);
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        // DELETE: api/account/5
        [HttpDelete, Route("{id}")]
        public IHttpActionResult Delete(string id)
        {
            Account model = _service.Get(id);

            if (model == null)
            {
                return NotFound();
            }

            try
            {
                _service.Delete(model);

                return Ok();
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        #endregion

        #region Helpers

        private JsonSerializerSettings GetPayloadSettings()
        {
            JsonSerializerSettings jss = PayloadManager.AddPayload<Account>("Id,Name").ToSettings();

            return jss;
        }

        #endregion
    }
}
