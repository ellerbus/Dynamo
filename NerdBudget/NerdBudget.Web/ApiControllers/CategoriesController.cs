using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using FluentValidation;
using NerdBudget.Core.Models;
using NerdBudget.Core.Services;
using Newtonsoft.Json;

namespace NerdBudget.Web.ApiControllers
{
    ///	<summary>
    /// Represents a basic controller for Category
    ///	</summary>
    [RoutePrefix("api/categories")]
    public class CategoriesController : BaseController
    {
        #region Members

        private IAccountService _accountService;
        private ICategoryService _categoryService;

        #endregion

        #region Contructors

        public CategoriesController(IAccountService accountSvc, ICategoryService categorySvc)
        {
            _accountService = accountSvc;
            _categoryService = categorySvc;
        }

        #endregion

        #region GetAll

        // GET: api/category
        [HttpGet, Route("{accountId}"), ResponseType(typeof(Account))]
        public IHttpActionResult GetAll(string accountId)
        {
            Account account = _accountService.Get(accountId);

            if (account == null)
            {
                return NotFound();
            }

            JsonSerializerSettings jss = GetPayloadSettings();

            var model = new
            {
                account = account,
                categories = account.Categories
            };

            return Json(model, jss);
        }

        #endregion

        #region Detail Display

        // GET: api/category/5
        [HttpGet, Route("{accountId}/{id}"), ResponseType(typeof(Category))]
        public IHttpActionResult Get(string accountId, string id)
        {
            try
            {
                Category category = GetCategory(accountId, id);

                JsonSerializerSettings jss = GetPayloadSettings();

                var model = new
                {
                    account = category.Account,
                    category = category
                };

                return Json(model, jss);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        #endregion

        #region Insert/Update/Delete

        // POST: api/category
        [HttpPost, Route("{accountId}"), ResponseType(typeof(Category))]
        public IHttpActionResult Post(string accountId, [FromBody]Category category)
        {
            Account account = _accountService.Get(accountId);

            if (account == null)
            {
                return NotFound();
            }

            try
            {
                _categoryService.Insert(account, category);

                JsonSerializerSettings jss = GetPayloadSettings();

                return Json(category, jss);
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        // PUT: api/category/5/sequences
        [HttpPut, Route("{accountId}/sequences")]
        public IHttpActionResult PutSequences(string accountId, [FromBody]string[] ids)
        {
            Account account = _accountService.Get(accountId);

            if (account == null)
            {
                return NotFound();
            }

            try
            {
                int seq = 0;

                foreach (string id in ids)
                {
                    Category c = account.Categories[id];

                    c.Sequence = seq;

                    seq += 10;
                }

                _categoryService.Update(account.Categories);

                account.Categories.Resort();

                return Ok();
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        // PUT: api/category/5
        [HttpPut, Route("{accountId}/{id}")]
        public IHttpActionResult Put(string accountId, string id, [FromBody]Category item)
        {
            try
            {
                Category category = GetCategory(accountId, id);

                category.Name = item.Name;

                _categoryService.Update(category);

                JsonSerializerSettings jss = GetPayloadSettings();

                return Json(category, jss);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        // DELETE: api/category/5/5
        [HttpDelete, Route("{accountId}/{id}")]
        public IHttpActionResult Delete(string accountId, string id)
        {
            try
            {
                Category category = GetCategory(accountId, id);

                _categoryService.Delete(category);

                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        #endregion

        #region Helpers

        private Category GetCategory(string accountId, string categoryId)
        {
            Account account = _accountService.Get(accountId);

            if (account == null)
            {
                return null;
            }

            Category category = account.Categories[categoryId];

            return category;
        }

        private JsonSerializerSettings GetPayloadSettings()
        {
            JsonSerializerSettings jss = PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Category>("Id,Name")
                .ToSettings();

            return jss;
        }

        #endregion
    }
}
