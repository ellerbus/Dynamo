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
        ///	Inserts a single Category calling the
        ///	stored procedure CategoryInsertOne
        ///	</summary>
        [Sql("CategoryInsertOne")]
        void Insert(Category category);

        ///	<summary>
        ///	Updates a single Category calling the
        ///	stored procedure CategoryUpdateOne
        ///	</summary>
        [Sql("CategoryUpdateOne")]
        void Update(Category category);

        ///	<summary>
        ///	Updates a single Category calling the
        ///	stored procedure CategoryUpdateOne
        ///	</summary>
        [Sql("CategoryUpdateMany")]
        void Update(IEnumerable<Category> categories);

        ///	<summary>
        ///	Deletes a single Category calling the
        ///	stored procedure CategoryDeleteOne
        ///	</summary>
        [Sql("CategoryDeleteOne")]
        void Delete(Category category);
    }
}