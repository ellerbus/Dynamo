using System.Collections.Generic;
using Insight.Database;
using NerdBudget.Core.Models;

namespace NerdBudget.Core.Repositories
{
    ///	<summary>
    /// Repository Interface for 
    ///	</summary>
    public interface ICategoryRepository
    {
        ///	<summary>
        ///	Saves a single Category calling the
        ///	stored procedure CategoryInsertOne
        ///	</summary>
        [Sql("CategoryUpsertOne")]
        void Save(Category category);

        ///	<summary>
        ///	Saves a many Categories calling the
        ///	stored procedure CategoryUpsertMany
        ///	</summary>
        [Sql("CategoryUpsertMany")]
        void Save(IEnumerable<Category> categories);

        ///	<summary>
        ///	Deletes a single Category calling the
        ///	stored procedure CategoryDeleteOne
        ///	</summary>
        [Sql("CategoryDeleteOne")]
        void Delete(Category category);
    }
}