using System.Linq;
using System.Web.Http;
using FluentValidation;
using NerdBudget.Core.Models;
using NerdBudget.Core.Services;
using Newtonsoft.Json;

namespace NerdBudget.Web.ApiControllers
{
    ///	<summary>
    /// Represents a basic controller for Map
    ///	</summary>
    [RoutePrefix("api/maps"), Authorize]
    public class MapsController : BaseController
    {
        #region Members

        private IAccountService _service;

        #endregion

        #region Contructors

        public MapsController(IAccountService service)
        {
            _service = service;
        }

        #endregion

        #region Verb Actions

        // PUT: api/map/5
        [HttpPut, Route("{accountId}/{id}")]
        public IHttpActionResult Put(string accountId, string id, [FromBody]Map map)
        {
            Account account = GetAccount(accountId);

            if (account == null)
            {
                return NotFound();
            }

            Budget toBudget = GetBudget(accountId, map.BudgetId);

            if (toBudget == null)
            {
                return NotFound();
            }

            Map model = account.Maps.FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            model.RegexPattern = map.RegexPattern;
            model.BudgetId = toBudget.Id;

            try
            {
                _service.Save(new[] { model });

                return Json(model, GetPayloadSettings());
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        #endregion

        #region Helpers

        private Budget GetBudget(string accountId, string budgetId)
        {
            Account account = GetAccount(accountId);

            if (account == null)
            {
                return null;
            }

            foreach (Category cat in account.Categories)
            {
                foreach (Budget bud in cat.Budgets)
                {
                    if (bud.Id == budgetId)
                    {
                        return bud;
                    }
                }
            }

            return null;
        }

        private Account GetAccount(string accountId)
        {
            return _service.Get(accountId);
        }

        private JsonSerializerSettings GetPayloadSettings()
        {
            return PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddStandardPayload<Map>()
                .ToSettings();
        }

        #endregion
    }
}
