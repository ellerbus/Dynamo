using System;
using System.Collections.Generic;
using Augment;
using Augment.Caching;
using FluentValidation;
using NerdBudget.Core.Models;
using NerdBudget.Core.Repositories;

namespace NerdBudget.Core.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for Category
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Inserts a Category
        /// </summary>
        void Insert(Account account, Category category);

        /// <summary>
        /// Updates a Category
        /// </summary>
        void Update(Category category);

        /// <summary>
        /// Updates a Categories
        /// </summary>
        void Update(IEnumerable<Category> categories);

        /// <summary>
        /// Deletes a Category
        /// </summary>
        void Delete(Category category);
    }

    #endregion

    /// <summary>
    /// Service Implementation for Category
    /// </summary>
    public class CategoryService : ICategoryService
    {
        #region Members

        private ICategoryRepository _repository;
        private IValidator<Category> _validator;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public CategoryService(ICategoryRepository repository, IValidator<Category> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a Category
        /// </summary>
        public void Insert(Account account, Category category)
        {
            category.Account = account;

            category.Id = Utilities.CreateId(2);
            category.Multiplier = category.Name.AssertNotNull().Contains("INCOME") ? 1 : -1;
            category.CreatedAt = DateTime.UtcNow;
            category.Sequence = account.Categories.Count * 10;

            _validator.ValidateAndThrow(category);

            _repository.Save(category);

            account.Categories.Add(category);
        }

        /// <summary>
        /// Updates a Category
        /// </summary>
        public void Update(Category category)
        {
            _validator.ValidateAndThrow(category);

            category.UpdatedAt = DateTime.UtcNow;

            _repository.Save(category);
        }

        /// <summary>
        /// Updates a Category
        /// </summary>
        public void Update(IEnumerable<Category> categories)
        {
            foreach (Category cat in categories)
            {
                _validator.ValidateAndThrow(cat);
            }

            foreach (Category cat in categories)
            {
                cat.UpdatedAt = DateTime.UtcNow;
            }

            _repository.Save(categories);
        }

        /// <summary>
        /// Deletes a Category
        /// </summary>
        public void Delete(Category category)
        {
            _repository.Delete(category);

            category.Account.Categories.Remove(category);
        }

        #endregion
    }
}