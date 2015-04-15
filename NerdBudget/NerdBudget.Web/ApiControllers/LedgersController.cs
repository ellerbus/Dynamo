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
    /// Represents a basic controller for Ledger
    ///	</summary>
    [RoutePrefix("api/ledgers")]
    public class LedgersController : BaseController
    {
        #region Members

        public class Trx { public string Transactions { get; set; } }

        private IAccountService _service;

        #endregion

        #region Contructors

        public LedgersController(IAccountService service)
        {
            _service = service;
        }

        #endregion

        #region Import Actions

        // POST: api/ledger
        [HttpPost, Route("{accountId}/import")]
        public IHttpActionResult PostImport(string accountId, [FromBody]Trx trx)
        {
            Account account = GetAccount(accountId);

            if (account == null)
            {
                return NotFound();
            }

            try
            {
                account.Ledgers.Import(trx.Transactions);

                _service.Save(account.Ledgers);

                _service.Save(account.Balances);

                _service.Save(account.Maps);

                return Ok();
            }
            catch (ValidationException exp)
            {
                return BadRequest(exp.Errors);
            }
        }

        #endregion

        #region Normal CRUD Actions

        // GET: api/ledger/5
        [HttpGet, Route("{accountId}/{budgetId}/{date}/weekly"), ResponseType(typeof(Ledger))]
        public IHttpActionResult GetLedgers(string accountId, string budgetId, DateTime date)
        {
            Account account = GetAccount(accountId);

            if (account == null)
            {
                return NotFound();
            }

            Range<DateTime> range = date.ToWeeklyBudgetRange();

            IEnumerable<Ledger> ledgers = account.Ledgers
                .Where(x => x.BudgetId == budgetId && range.Contains(x.Date))
                .OrderBy(x => x.Date).ThenBy(x => x.Sequence);

            JsonSerializerSettings jss = GetPayloadSettings();

            return Json(ledgers, jss);
        }

        // GET: api/ledger/5
        [HttpGet, Route("{accountId}/map"), ResponseType(typeof(Ledger))]
        public IHttpActionResult Get(string accountId)
        {
            Account account = GetAccount(accountId);

            if (account == null)
            {
                return NotFound();
            }

            Ledger ledger = account.Ledgers.MissingBudget().FirstOrDefault();

            if (ledger == null)
            {
                return Ok(new { status = 302, url = Url.Content("~/Analysis/" + account.Id) });
            }

            JsonSerializerSettings jss = GetPayloadSettings();

            return Json(ledger, jss);
        }

        // GET: api/ledger/5
        [HttpGet, Route("{accountId}/{id}/{date}"), ResponseType(typeof(Ledger))]
        public IHttpActionResult Get(string accountId, string id, DateTime date)
        {
            Account account = GetAccount(accountId);

            if (account == null)
            {
                return NotFound();
            }

            Ledger ledger = account.Ledgers.Find(id, date);

            if (ledger == null)
            {
                return NotFound();
            }

            JsonSerializerSettings jss = GetPayloadSettings();

            var model = new
            {
                account = account,
                budgets = account.Categories.SelectMany(x => x.Budgets),
                ledger = ledger
            };

            return Json(model, jss);
        }

        // PUT: api/ledger/5
        [HttpPut, Route("{accountId}/{id}/{date}")]
        public IHttpActionResult Put(string accountId, string id, DateTime date, [FromBody]Ledger ledger)
        {
            Account account = GetAccount(accountId);

            if (account == null)
            {
                return NotFound();
            }

            Ledger model = account.Ledgers.Find(id, date);

            if (model == null)
            {
                return NotFound();
            }

            if (model.BudgetId.IsNullOrEmpty())
            {
                //  need to associate a map
                Map map = account.Maps.CreateFor(ledger);

                foreach (Ledger x in account.Ledgers.MissingBudget())
                {
                    if (map.IsMatchFor(x))
                    {
                        x.BudgetId = map.BudgetId;
                    }
                }
            }
            else
            {
                //  otherwise we're just moving "this" ledger
                model.BudgetId = ledger.BudgetId;
            }

            try
            {
                _service.Save(account.Ledgers);

                _service.Save(account.Maps);

                return Ok();
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Errors);
            }
        }

        #endregion

        #region Verb Actions

        //// GET: api/ledger
        //[HttpGet, Route("{accountId}/{startDate:datetime}/{endDate:datetime}")]
        //public IHttpActionResult GetAll(string accountId, DateTime startDate, DateTime endDate)
        //{
        //    Account account = GetAccount(accountId);

        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    Range<DateTime> dateRange = new Range<DateTime>(startDate, endDate);

        //    IList<Ledger> ledgers = account.Ledgers
        //        .Where(x => dateRange.Contains(x.Date))
        //        .OrderBy(x => x.Date)
        //        .ThenBy(x => x.Sequence)
        //        .ToList();

        //    JsonSerializerSettings jss = GetPayloadSettings();

        //    var model = new
        //    {
        //        account = account,
        //        ledgers = ledgers
        //    };

        //    return Json(_service.GetList());
        //}

        #endregion

        #region Helpers

        private Account GetAccount(string accountId)
        {
            return _service.Get(accountId);
        }

        private JsonSerializerSettings GetPayloadSettings()
        {
            return PayloadManager
                .AddPayload<Account>("Id,Name")
                .AddPayload<Budget>("Id,FullName")
                .AddBasicPayload<Ledger>()
                .ToSettings();
        }

        #endregion
    }
}
