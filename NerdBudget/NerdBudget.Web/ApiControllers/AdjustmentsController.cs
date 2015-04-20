using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Augment;
using FluentValidation;
using NerdBudget.Core;
using NerdBudget.Core.Models;
using NerdBudget.Core.Services;
using Newtonsoft.Json;

namespace NerdBudget.Web.ApiControllers
{
    ///	<summary>
    /// Represents a basic controller for Adjustment
    ///	</summary>
    [RoutePrefix("api/adjustments")]
    public class AdjustmentsController : BaseController
    {
        #region Members

        private IAccountService _accountService;
        private IAdjustmentService _adjustmentService;

        #endregion

        #region Contructors

        public AdjustmentsController(IAccountService accountSvc, IAdjustmentService adjustmentSvc)
        {
            _accountService = accountSvc;
            _adjustmentService = adjustmentSvc;
        }

        #endregion

        #region Misc Actions

        // GET: api/ledger/5
        [HttpGet, Route("{accountId}/{budgetId}/{date}/weekly"), ResponseType(typeof(Adjustment))]
        public IHttpActionResult GetAdjustments(string accountId, string budgetId, DateTime date)
        {
            Account account = GetAccount(accountId);

            if (account == null)
            {
                return NotFound();
            }

            Range<DateTime> range = date.ToWeeklyBudgetRange();

            IEnumerable<Adjustment> adjustments = account.Categories
                .SelectMany(x => x.Budgets).SelectMany(x => x.Adjustments)
                .Where(x => x.BudgetId == budgetId && (x.Date == null || range.Contains(x.Date.Value)))
                .OrderBy(x => x.Date).ThenBy(x => x.Name);

            JsonSerializerSettings jss = GetPayloadSettings();

            return Json(adjustments, jss);
        }

        #endregion

        #region Insert/Update/Delete

        // POST: api/adjustment
        [HttpPost, Route("{accountId}")]
        public IHttpActionResult Post(string accountId, [FromBody]Adjustment adjustment)
        {
            try
            {
                Budget budget = GetBudget(accountId, adjustment.BudgetId);

                if (budget == null)
                {
                    return NotFound();
                }

                _adjustmentService.Insert(budget, adjustment);

                JsonSerializerSettings jss = GetPayloadSettings();

                return Json(adjustment, jss);
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        // PUT: api/adjustment/5
        [HttpPut, Route("{accountId}/{id}")]
        public IHttpActionResult Put(string accountId, string id, [FromBody]Adjustment adjustment)
        {
            Adjustment model = GetAdjustment(accountId, id);

            if (model == null)
            {
                return NotFound();
            }

            model.BudgetId = adjustment.BudgetId;
            model.Name = adjustment.Name;
            model.Date = adjustment.Date;
            model.Amount = adjustment.Amount;

            try
            {
                _adjustmentService.Update(adjustment);

                JsonSerializerSettings jss = GetPayloadSettings();

                return Json(adjustment, jss);
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        // DELETE: api/adjustment/5
        [HttpDelete, Route("{accountId}/{id}")]
        public IHttpActionResult Delete(string accountId, string id)
        {
            Adjustment adjustment = GetAdjustment(accountId, id);

            if (adjustment == null)
            {
                return NotFound();
            }

            try
            {
                _adjustmentService.Delete(adjustment);

                return Ok();
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        #endregion

        #region Helpers

        private Account GetAccount(string accountId)
        {
            return _accountService.Get(accountId);
        }

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

        private Adjustment GetAdjustment(string accountId, string id)
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
                    foreach (Adjustment adj in bud.Adjustments)
                    {
                        if (adj.Id == id)
                        {
                            return adj;
                        }
                    }
                }
            }

            return null;
        }

        private JsonSerializerSettings GetPayloadSettings()
        {
            return PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Budget>("Id,FullName")
                .AddBasicPayload<Adjustment>()
                .ToSettings();
        }

        #endregion
    }
}
