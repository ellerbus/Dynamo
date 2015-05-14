using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FluentValidation;
using FluentValidation.Results;
using NerdBudget.Core.Models;
using NerdBudget.Core.Services;
using NerdBudget.Core.Validators;
using Newtonsoft.Json;

namespace NerdBudget.Web.ApiControllers
{
    ///	<summary>
    /// Represents a basic controller for Balance
    ///	</summary>
    [RoutePrefix("api/balances")]
    public class BalancesController : BaseController
    {
        #region Members

        private IAccountService _accountService;

        #endregion

        #region Contructors

        public BalancesController(IAccountService service)
        {
            _accountService = service;
        }

        #endregion

        #region Verb Actions

        // PUT: api/balance/5
        [HttpPut, Route("{accountId}/{asOf}")]
        public IHttpActionResult Put(string accountId, DateTime asOf, [FromBody]Balance balance)
        {
            Balance model = GetBalance(accountId, asOf);

            if (model == null)
            {
                return NotFound();
            }

            model.Amount = balance.Amount;

            try
            {
                _accountService.Save(model.Account.Balances);

                return Json(model, GetPayloadSettings());
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        #endregion

        #region Helpers

        private Balance GetBalance(string accountId, DateTime asof)
        {
            Account account = _accountService.Get(accountId);

            if (account == null)
            {
                return null;
            }

            if (!account.Balances.Contains(asof))
            {
                return null;
            }

            Balance balance = account.Balances[asof];

            return balance;
        }

        private JsonSerializerSettings GetPayloadSettings()
        {
            JsonSerializerSettings jss = PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Balance>("AccountId,AsOf,Amount")
                .ToSettings();

            return jss;
        }

        #endregion
    }
}
