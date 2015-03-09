using System.Collections.Generic;
using Insight.Database;
using NerdBudget.Core.Models;

namespace NerdBudget.Core.Repositories
{
    ///	<summary>
    /// Repository Interface for 
    ///	</summary>
    public interface IBudgetRepository
    {
        ///	<summary>
        ///	Saves a single Budget calling the
        ///	stored procedure BudgetInsertOne
        ///	</summary>
        [Sql("BudgetUpsertOne")]
        void Save(Budget budget);

        ///	<summary>
        ///	Save many Budgets calling the
        ///	stored procedure BudgetInsertOne
        ///	</summary>
        [Sql("BudgetUpsertMany")]
        void Save(IEnumerable<Budget> budgets);

        ///	<summary>
        ///	Deletes a single Budget calling the
        ///	stored procedure BudgetDeleteOne
        ///	</summary>
        [Sql("BudgetDeleteOne")]
        void Delete(Budget budget);
    }
}