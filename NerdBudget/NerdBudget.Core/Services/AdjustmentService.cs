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
    /// Service Interface for Adjustment
    /// </summary>
    public interface IAdjustmentService
    {
        /// <summary>
        /// Inserts a Adjustment
        /// </summary>
        void Insert(Budget budget, Adjustment adjustment);

        /// <summary>
        /// Updates a Adjustment
        /// </summary>
        void Update(Adjustment adjustment);

        /// <summary>
        /// Deletes a Adjustment
        /// </summary>
        void Delete(Adjustment adjustment);
    }

    #endregion

    /// <summary>
    /// Service Implementation for Adjustment
    /// </summary>
    public class AdjustmentService : IAdjustmentService
    {
        #region Members

        private IAdjustmentRepository _repository;
        private IValidator<Adjustment> _validator;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public AdjustmentService(IAdjustmentRepository repository, IValidator<Adjustment> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a Adjustment
        /// </summary>
        public void Insert(Budget budget, Adjustment adjustment)
        {
            Account account = budget.Account;

            adjustment.Budget = budget;

            adjustment.Id = CreateUniqueId(account);
            adjustment.CreatedAt = DateTime.UtcNow;

            _validator.ValidateAndThrow(adjustment);

            _repository.Save(adjustment);

            budget.Adjustments.Add(adjustment);

            account.Adjustments.Add(adjustment);
        }

        /// <summary>
        /// Attempt to create a unique ID
        /// </summary>
        /// <returns></returns>
        private string CreateUniqueId(Account account)
        {
            IEnumerable<string> list = account.Adjustments.Select(x => x.Id);

            HashSet<string> ids = new HashSet<string>(list);

            for (int x = 0; x < 5; x++)
            {
                string id = Utilities.CreateId(2);

                if (!ids.Contains(id))
                {
                    return id;
                }
            }

            throw new Exception("ADJUSTMENT - Unable to create unique ID");
        }

        /// <summary>
        /// Updates a Adjustment
        /// </summary>
        public void Update(Adjustment adjustment)
        {
            _validator.ValidateAndThrow(adjustment);

            adjustment.UpdatedAt = DateTime.UtcNow;

            _repository.Save(adjustment);

            //  budgetId rules them ALL
            Budget current = adjustment.Budget;

            if (current != null && current.Id != adjustment.BudgetId)
            {
                //  OK - we need to move somethings around
                Account account = adjustment.Account;

                current.Adjustments.Remove(adjustment);

                account.Budgets[adjustment.BudgetId].Adjustments.Add(adjustment);
            }
        }

        /// <summary>
        /// Deletes a Budget
        /// </summary>
        public void Delete(Adjustment adjustment)
        {
            _repository.Delete(adjustment);

            adjustment.Account.Adjustments.Remove(adjustment);

            adjustment.Budget.Adjustments.Remove(adjustment);
        }

        #endregion
    }
}