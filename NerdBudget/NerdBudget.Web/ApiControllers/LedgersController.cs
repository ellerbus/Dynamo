using System;
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
    /// Represents a basic controller for Ledger
    ///	</summary>
    [RoutePrefix("api/ledgers")]
    public class LedgersController : BaseController
    {
        #region Members

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
        [HttpGet, Route("{accountId}/import")]
        public IHttpActionResult GetImport(string accountId)
        {
            Account account = GetAccount(accountId);

            if (account == null)
            {
                return NotFound();
            }

            try
            {
                JsonSerializerSettings jss = GetPayloadSettings();

                Ledger ledger = account.Ledgers
                    .OrderBy(x => x.Date)
                    .ThenBy(x => x.Sequence)
                    .LastOrDefault();

                var model = new
                {
                    account = account,
                    ledger = ledger
                };

                return Json(model, jss);
            }
            catch (ValidationException exp)
            {
                return BadRequest(exp.Errors);
            }
        }

        // POST: api/ledger
        [HttpPost, Route("{accountId}/import")]
        public IHttpActionResult PostImport(string accountId, [FromBody]string transactions)
        {
            Account account = GetAccount(accountId);

            if (account == null)
            {
                return NotFound();
            }

            try
            {
                account.Ledgers.Import(transactions);

                _service.Save(account.Ledgers);

                _service.Save(account.Balances);

                return Ok();
            }
            catch (ValidationException exp)
            {
                return BadRequest(exp.Errors);
            }
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

            model.BudgetId = ledger.BudgetId;

            try
            {
                _service.Save(account.Ledgers);

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
                .AddPayload<Account>("Id", "Name")
                .AddPayload<Budget>("Id", "FullName")
                .AddBasicPayload<Ledger>()
                .ToSettings();
        }

        #endregion
    }
}
