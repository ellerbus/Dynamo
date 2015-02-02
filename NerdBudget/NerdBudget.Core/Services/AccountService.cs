using System;
using System.Collections.Generic;
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
            account.Id = Utilities.CreateId(2);
            account.Type = "C";
            account.StartedAt = DateTime.UtcNow;
            account.CreatedAt = DateTime.UtcNow;

            _validator.ValidateAndThrow(account);

            _repository.Insert(account);

            _cache.Find<IList<Account>>().RemoveAll();
        }

        /// <summary>
        /// Updates a Account
        /// </summary>
        public void Update(Account account)
        {
            _validator.ValidateAndThrow(account);

            account.UpdatedAt = DateTime.UtcNow;

            _repository.Update(account);

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

        #endregion
    }
}