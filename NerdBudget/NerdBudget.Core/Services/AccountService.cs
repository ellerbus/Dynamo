using System;
using System.Collections.Generic;
using System.Linq;
using Augment;
using Augment.Caching;
using FluentValidation;
using NerdBudget.Core.Models;
using NerdBudget.Core.Repositories;

namespace NerdBudget.Core.Services
{
    #region Cache Extensions

    static class AccountCacheExtensions
    {
        public static ICacheObject<Account> ByPrimaryKey(this ICacheObject<Account> cache, string id)
        {
            return cache.By(id);
        }
        public static ICacheRetrieval<Account> ByPrimaryKey(this ICacheRetrieval<Account> cache, string id)
        {
            return cache.By(id);
        }
    }

    #endregion

    #region Service interface

    /// <summary>
    /// Service Interface for Account
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Gets a list of Accounts
        /// </summary>
        /// <returns></returns>
        IList<Account> GetList();

        /// <summary>
        /// Gets a singe Account based on the given primary key
        /// </summary>
        Account Get(string id);

        /// <summary>
        /// Inserts a Account
        /// </summary>
        void Insert(Account account);

        /// <summary>
        /// Updates a Account
        /// </summary>
        void Update(Account account);

        /// <summary>
        /// Deletes a Account
        /// </summary>
        void Delete(Account account);

        /// <summary>
        /// Saves many balances
        /// </summary>
        void Save(IEnumerable<Balance> balances);

        /// <summary>
        /// Saves many maps
        /// </summary>
        void Save(IEnumerable<Map> maps);

        /// <summary>
        /// Saves many ledgers
        /// </summary>
        void Save(IEnumerable<Ledger> ledgers);
    }

    #endregion

    /// <summary>
    /// Service Implementation for Account
    /// </summary>
    public class AccountService : IAccountService
    {
        #region Members

        private IAccountRepository _repository;
        private IValidator<Account> _validator;
        private ICacheManager _cache;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public AccountService(IAccountRepository repository, IValidator<Account> validator, ICacheManager cache)
        {
            _repository = repository;
            _validator = validator;
            _cache = cache;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a list of Accounts
        /// </summary>
        /// <returns></returns>
        public IList<Account> GetList()
        {
            IList<Account> accounts = _cache
                .Cache(() => _repository.GetList())
                .DurationOf(20.Minutes(), CacheExpiration.Sliding)
                .CachedObject;

            return accounts;
        }

        /// <summary>
        /// Gets a singe Account based on the given primary key
        /// </summary>
        public Account Get(string id)
        {
            Account account = _cache
                .Cache(() => _repository.Get(id))
                .By(id)
                .DurationOf(20.Minutes(), CacheExpiration.Sliding)
                .CachedObject;

            return account;
        }

        /// <summary>
        /// Inserts a Account
        /// </summary>
        public void Insert(Account account)
        {
            account.Id = CreateUniqueId();
            account.Type = "C";
            account.StartedAt = DateTime.UtcNow;

            Utilities.AuditUpdate(account);

            _validator.ValidateAndThrow(account);

            _repository.Save(account);

            _cache.Find<IList<Account>>().RemoveAll();
        }

        /// <summary>
        /// Attempt to create a unique ID
        /// </summary>
        /// <returns></returns>
        private string CreateUniqueId()
        {
            IEnumerable<string> list = GetList().Select(x => x.Id);

            HashSet<string> ids = new HashSet<string>(list);

            for (int x = 0; x < 5; x++)
            {
                string id = Utilities.CreateId(2);

                if (!ids.Contains(id))
                {
                    return id;
                }
            }

            throw new Exception("ACCOUNT - Unable to create unique ID");
        }

        /// <summary>
        /// Updates a Account
        /// </summary>
        public void Update(Account account)
        {
            _validator.ValidateAndThrow(account);

            Utilities.AuditUpdate(account);

            _repository.Save(account);

            _cache.Find<Account>().ByPrimaryKey(account.Id).Remove();

            _cache.Find<IList<Account>>().RemoveAll();
        }

        /// <summary>
        /// Deletes a Account
        /// </summary>
        public void Delete(Account account)
        {
            _repository.Delete(account);

            _cache.Find<Account>().ByPrimaryKey(account.Id).Remove();

            _cache.Find<IList<Account>>().RemoveAll();
        }

        /// <summary>
        /// Saves balances
        /// </summary>
        public void Save(IEnumerable<Balance> balances)
        {
            IList<Balance> balancesToSave = balances.Where(x => x.IsModified).ToList();

            if (balancesToSave.Count > 0)
            {
                foreach (Balance bal in balancesToSave)
                {
                    if (bal.CreatedAt == DateTime.MinValue)
                    {
                        bal.CreatedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        bal.UpdatedAt = DateTime.UtcNow;
                    }
                }

                _repository.Save(balancesToSave);

                foreach (Balance bal in balancesToSave)
                {
                    bal.IsModified = false;
                }
            }
        }

        /// <summary>
        /// Saves maps
        /// </summary>
        public void Save(IEnumerable<Map> maps)
        {
            IList<Map> mapsToSave = maps.Where(x => x.IsModified).ToList();

            if (mapsToSave.Count > 0)
            {
                foreach (Map map in mapsToSave)
                {
                    if (map.CreatedAt == DateTime.MinValue)
                    {
                        map.CreatedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        map.UpdatedAt = DateTime.UtcNow;
                    }
                }

                _repository.Save(mapsToSave);

                foreach (Map map in mapsToSave)
                {
                    //  budgetId rules them ALL
                    Budget current = map.Budget;

                    if (current != null && current.Id != map.BudgetId)
                    {
                        //  OK - we need to move somethings around
                        map.Budget = map.Account.Categories.SelectMany(x => x.Budgets).First(x => x.Id == map.BudgetId);
                    }

                    map.IsModified = false;
                }
            }
        }

        /// <summary>
        /// Saves balances
        /// </summary>
        public void Save(IEnumerable<Ledger> ledgers)
        {
            IList<Ledger> ledgersToSave = ledgers.Where(x => x.IsModified).ToList();

            if (ledgersToSave.Count > 0)
            {
                foreach (Ledger led in ledgersToSave)
                {
                    if (led.CreatedAt == DateTime.MinValue)
                    {
                        led.CreatedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        led.UpdatedAt = DateTime.UtcNow;
                    }
                }

                _repository.Save(ledgersToSave);

                foreach (Ledger led in ledgersToSave)
                {
                    led.IsModified = false;
                }
            }
        }

        #endregion
    }
}