using System;
using System.Collections.Generic;
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
            budget.Category = category;

            budget.Id = Utilities.CreateId(2);
            budget.CreatedAt = DateTime.UtcNow;
            budget.Sequence = category.Budgets.Count * 10;

            _validator.ValidateAndThrow(budget);

            _repository.Save(budget);

            category.Budgets.Add(budget);

            category.Account.Budgets.Add(budget);
        }

        /// <summary>
        /// Updates a Budget
        /// </summary>
        public void Update(Budget budget)
        {
            _validator.ValidateAndThrow(budget);

            budget.UpdatedAt = DateTime.UtcNow;

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
                bud.UpdatedAt = DateTime.UtcNow;
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