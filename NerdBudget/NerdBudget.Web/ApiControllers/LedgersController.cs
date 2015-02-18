using System.Linq;
using System.Web.Http;
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
        public IHttpActionResult GetImport(string accountId, [FromBody]string transactions)
        {
            Account account = GetAccount(accountId);

            if (account == null)
            {
                return NotFound();
            }

            try
            {
                JsonSerializerSettings jss = GetPayloadSettings();

                var model = new
                {
                    account = account,
                    ledger = account.Ledgers.LastOrDefault()
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

                return Ok();
            }
            catch (ValidationException exp)
            {
                return BadRequest(exp.Errors);
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

        //// GET: api/ledger/5
        //[HttpGet, Route("{accountId}/{id}/{date}"), ResponseType(typeof(Ledger))]
        //public IHttpActionResult Get(string accountId, string id, DateTime date)
        //{
        //    Ledger ledger = _service.Get(accountId, id, date);

        //    if (ledger == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(ledger);
        //}

        //// POST: api/ledger
        //[HttpPost, Route("")]
        //public IHttpActionResult Post([FromBody]Ledger ledger)
        //{
        //    try
        //    {
        //        _service.Insert(ledger);

        //        return Ok();
        //    }
        //    catch (ValidationException ve)
        //    {
        //        return BadRequest(ve.Errors);
        //    }
        //}

        //// PUT: api/ledger/5
        //[HttpPut, Route("{accountId}/{id}/{date}")]
        //public IHttpActionResult Put(string accountId, string id, DateTime date, [FromBody]Ledger ledger)
        //{
        //    Ledger model = _service.Get(accountId, id, date);

        //    if (model == null)
        //    {
        //        return NotFound();
        //    }

        //    model.BudgetId = ledger.BudgetId;
        //    model.OriginalText = ledger.OriginalText;
        //    model.Description = ledger.Description;
        //    model.Amount = ledger.Amount;
        //    model.Balance = ledger.Balance;
        //    model.Sequence = ledger.Sequence;
        //    model.CreatedAt = ledger.CreatedAt;
        //    model.UpdatedAt = ledger.UpdatedAt;
        //    try
        //    {
        //        _service.Update(model);

        //        return Ok();
        //    }
        //    catch (ValidationException ve)
        //    {
        //        return BadRequest(ve.Errors);
        //    }
        //}

        //// DELETE: api/ledger/5
        //[HttpDelete, Route("{accountId}/{id}/{date}")]
        //public IHttpActionResult Delete(string accountId, string id, DateTime date)
        //{
        //    Ledger model = _service.Get(accountId, id, date);

        //    if (model == null)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        _service.Delete(model);

        //        return Ok();
        //    }
        //    catch (ValidationException ve)
        //    {
        //        return BadRequest(ve.Errors);
        //    }
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
                .AddBasicPayload<Ledger>()
                .ToSettings();
        }

        #endregion
    }
}
