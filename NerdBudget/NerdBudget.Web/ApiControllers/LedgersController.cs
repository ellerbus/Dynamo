using System.Web.Http;
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

        #region Verb Actions

        // GET: api/ledger
        [HttpGet, Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetList());
        }

        #endregion

        #region Helpers

        private Account GetAccout(string accountId)
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
