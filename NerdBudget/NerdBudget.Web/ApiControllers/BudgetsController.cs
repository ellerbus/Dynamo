using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Augment;
using FluentValidation;
using FluentValidation.Results;
using NerdBudget.Core.Models;
using NerdBudget.Core.Services;
using Newtonsoft.Json;

namespace NerdBudget.Web.ApiControllers
{
    ///	<summary>
    /// Represents a basic controller for Budget
    ///	</summary>
    [RoutePrefix("api/budgets")]
    public class BudgetsController : BaseController
    {
        #region Members

        public class Seq { public string[] Sequence { get; set; } }

        private IAccountService _accountService;
        private IBudgetService _budgetService;

        #endregion

        #region Contructors

        public BudgetsController(IAccountService accountSvc, IBudgetService budgetSvc)
        {
            _accountService = accountSvc;
            _budgetService = budgetSvc;
        }

        #endregion

        #region Insert/Update/Delete

        // POST: api/budget
        [HttpPost, Route("{accountId}"), ResponseType(typeof(Budget))]
        public IHttpActionResult Post(string accountId, [FromBody]Budget budget)
        {
            Account account = _accountService.Get(accountId);

            if (account == null)
            {
                return NotFound();
            }

            try
            {
                if (budget.CategoryId.IsNullOrEmpty())
                {
                    throw new ValidationException(new[] { new ValidationFailure("CategoryId", "Category is Required") });
                }

                Category category = account.Categories[budget.CategoryId];

                _budgetService.Insert(category, budget);

                JsonSerializerSettings jss = GetPayloadSettings();

                return Json(budget, jss);
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

        // PUT: api/budget/5
        [HttpPut, Route("{accountId}/{id:regex([A-Z0-9]{2})}")]
        //  NOTE regex is used so we don't interfere with /sequences
        public IHttpActionResult Put(string accountId, string id, [FromBody]Budget item)
        {
            Budget budget = GetBudget(accountId, id);

            if (budget == null)
            {
                return NotFound();
            }

            budget.Name = item.Name;
            budget.CategoryId = item.CategoryId;
            budget.Frequency = item.Frequency;
            budget.Amount = item.Amount;
            budget.StartDate = item.StartDate;
            budget.EndDate = item.EndDate;

            try
            {
                _budgetService.Update(budget);

                JsonSerializerSettings jss = GetPayloadSettings();

                return Json(budget, jss);
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        // PUT: api/budget/5/sequences
        [HttpPut, Route("{accountId}/sequences")]
        public IHttpActionResult PutSequences(string accountId, [FromBody]Seq sequences)
        {
            Account account = _accountService.Get(accountId);

            if (account == null)
            {
                return NotFound();
            }

            try
            {
                List<Budget> budgets = new List<Budget>();

                foreach (Category cat in account.Categories)
                {
                    int seq = 0;

                    foreach (string id in sequences.Sequence)
                    {
                        if (cat.Budgets.Contains(id))
                        {
                            Budget bud = cat.Budgets[id];

                            bud.Sequence = seq;

                            seq += 10;

                            budgets.Add(bud);
                        }
                    }

                    if (seq > 0)
                    {
                        cat.Budgets.Resort();
                    }
                }

                _budgetService.Update(budgets);

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

        // DELETE: api/budget/5/5
        //  NOTE regex is used so we don't interfere with /sequences
        [HttpDelete, Route("{accountId}/{id:regex([A-Z0-9]{2})}")]
        public IHttpActionResult Delete(string accountId, string id)
        {
            Budget budget = GetBudget(accountId, id);

            if (budget == null)
            {
                return NotFound();
            }

            try
            {
                _budgetService.Delete(budget);

                return Ok();
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
            Account account = _accountService.Get(accountId);

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

        private JsonSerializerSettings GetPayloadSettings()
        {
            return PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Category>("Id,AccountId,Name")
                .AddStandardPayload<Budget>()
                .ToSettings();
        }

        #endregion
    }
}
