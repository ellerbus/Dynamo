using System.Linq;
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
            JsonSerializerSettings jss = PayloadManager
                .AddPayload<Account>("Id", "Name", "Categories")
                .AddPayload<Category>("Id", "Name", "AccountId")
                .ToSettings();

            Account account = _accountService.Get(accountId);

            if (account == null)
            {
                return NotFound();
            }

            return Json(account, jss);
        }

        #endregion

        #region Detail Display

        // GET: api/category/5
        [HttpGet, Route("{accountId}/{id}"), ResponseType(typeof(Category))]
        public IHttpActionResult Get(string accountId, string id)
        {
            Category category = GetCategory(accountId, id);

            if (category == null)
            {
                return NotFound();
            }

            JsonSerializerSettings jss = PayloadManager
                .AddPayload<Account>("Id", "Name")
                .AddPayload<Category>("Id", "Name", "Account")
                .ToSettings();

            return Json(category, jss);
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

                return Ok(category);
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
                    Category c = account.Categories.First(x => x.Id == id);

                    c.Sequence = seq;

                    seq += 10;
                }

                _categoryService.Update(account.Categories);

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
            Category category = GetCategory(accountId, id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = item.Name;

            try
            {
                _categoryService.Update(category);

                return Ok(category);
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
            Category category = GetCategory(accountId, id);

            if (category == null)
            {
                return NotFound();
            }

            try
            {
                _categoryService.Delete(category);

                return Ok();
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

            Category category = account.Categories.FirstOrDefault(x => x.Id == categoryId);

            return category;
        }

        #endregion
    }
}
