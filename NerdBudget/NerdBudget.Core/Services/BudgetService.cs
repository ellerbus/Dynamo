using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using NerdBudget.Core.Models;
using NerdBudget.Core.Repositories;

namespace NerdBudget.Core.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for Budget
    /// </summary>
    public interface IBudgetService
    {
        /// <summary>
        /// Inserts a Budget
        /// </summary>
        void Insert(Category category, Budget budget);

        /// <summary>
        /// Updates a Budget
        /// </summary>
        void Update(Budget budget);

        /// <summary>
        /// Updates a Budgets
        /// </summary>
        void Update(IEnumerable<Budget> budgets);

        /// <summary>
        /// Deletes a Budget
        /// </summary>
        void Delete(Budget budget);
    }

    #endregion

    /// <summary>
    /// Service Implementation for Budget
    /// </summary>
    public class BudgetService : IBudgetService
    {
        #region Members

        private IBudgetRepository _repository;
        private IValidator<Budget> _validator;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public BudgetService(IBudgetRepository repository, IValidator<Budget> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a Budget
        /// </summary>
        public void Insert(Category category, Budget budget)
        {
            Account account = category.Account;

            budget.Category = category;

            budget.Id = CreateUniqueId(account);
            budget.Sequence = category.Budgets.Count * 10;

            Utilities.AuditUpdate(budget);

            _validator.ValidateAndThrow(budget);

            _repository.Save(budget);

            category.Budgets.Add(budget);

            account.Budgets.Add(budget);
        }

        /// <summary>
        /// Attempt to create a unique ID
        /// </summary>
        /// <returns></returns>
        private string CreateUniqueId(Account account)
        {
            IEnumerable<string> list = account.Budgets.Select(x => x.Id);

            HashSet<string> ids = new HashSet<string>(list);

            for (int x = 0; x < 5; x++)
            {
                string id = Utilities.CreateId(2);

                if (!ids.Contains(id))
                {
                    return id;
                }
            }

            throw new Exception("BUDGET - Unable to create unique ID");
        }

        /// <summary>
        /// Updates a Budget
        /// </summary>
        public void Update(Budget budget)
        {
            _validator.ValidateAndThrow(budget);

            Utilities.AuditUpdate(budget);

            _repository.Save(budget);

            //  categoryId rules them ALL
            Category current = budget.Category;

            if (current != null && current.Id != budget.CategoryId)
            {
                //  OK - we need to move somethings around
                Account account = budget.Account;

                current.Budgets.Remove(budget);

                account.Categories[budget.CategoryId].Budgets.Add(budget);
            }
        }

        /// <summary>
        /// Updates a Budget
        /// </summary>
        public void Update(IEnumerable<Budget> budgets)
        {
            foreach (Budget bud in budgets)
            {
                _validator.ValidateAndThrow(bud);
            }

            foreach (Budget bud in budgets)
            {
                Utilities.AuditUpdate(bud);
            }

            _repository.Save(budgets);
        }

        /// <summary>
        /// Deletes a Budget
        /// </summary>
        public void Delete(Budget budget)
        {
            _repository.Delete(budget);

            budget.Account.Budgets.Remove(budget);

            budget.Category.Budgets.Remove(budget);
        }

        #endregion
    }
}