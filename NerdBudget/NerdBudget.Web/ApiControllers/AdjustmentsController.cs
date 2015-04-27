using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Augment;
using FluentValidation;
using FluentValidation.Results;
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

            Budget budget = account.Categories
                .SelectMany(x => x.Budgets).Where(x => x.Id == budgetId)
                .FirstOrDefault();

            IList<Adjustment> adjustments = budget.Adjustments
                .Where(x => x.Date == null || range.Contains(x.Date.Value))
                .OrderBy(x => x.Date).ThenBy(x => x.Name)
                .ToList();

            if (budget.IsValidFor(range))
            {
                Adjustment bgt = new Adjustment()
                {
                    Date = date,
                    Name = "BUDGET: " + budget.Name,
                    Amount = budget.Amount
                };

                adjustments.Insert(0, bgt);
            }

            JsonSerializerSettings jss = GetPayloadSettings();

            return Json(adjustments, jss);
        }

        #endregion

        #region Insert/Update/Delete

        // POST: api/adjustment
        [HttpPost, Route("{accountId}/{fromBudgetId}/{toBudgetId}/transfer")]
        public IHttpActionResult PostTransfer(string accountId, string fromBudgetId, string toBudgetId, [FromBody]Adjustment adjustment)
        {
            try
            {
                Budget fromBudget = GetBudget(accountId, fromBudgetId);

                if (fromBudget == null)
                {
                    throw new ValidationException(new[] { new ValidationFailure("FromBudgetId", "From Budget is Required") });
                }

                Budget toBudget = GetBudget(accountId, toBudgetId);

                if (toBudget == null)
                {
                    throw new ValidationException(new[] { new ValidationFailure("ToBudgetId", "To Budget is Required") });
                }

                Adjustment fromAdjustment = new Adjustment
                {
                    Date = adjustment.Date,
                    Amount = -adjustment.Amount,
                    Name = "XFER to " + toBudget.Name
                };

                Adjustment toAdjustment = new Adjustment
                {
                    Date = adjustment.Date,
                    Amount = adjustment.Amount,
                    Name = "XFER from " + fromBudget.Name
                };

                _adjustmentService.Insert(fromBudget, fromAdjustment);
                _adjustmentService.Insert(toBudget, toAdjustment);

                return Ok();
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        // POST: api/adjustment
        [HttpPost, Route("{accountId}")]
        public IHttpActionResult Post(string accountId, [FromBody]Adjustment adjustment)
        {
            try
            {
                if (adjustment.BudgetId.IsNullOrEmpty())
                {
                    throw new ValidationException(new[] { new ValidationFailure("BudgetId", "Budget is Required") });
                }

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
                _adjustmentService.Update(model);

                JsonSerializerSettings jss = GetPayloadSettings();

                return Json(model, jss);
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
                .AddStandardPayload<Adjustment>()
                .ToSettings();
        }

        #endregion
    }
}
