using System;
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
		///	Inserts a single Budget calling the
		///	stored procedure BudgetInsertOne
		///	</summary>
		[Sql("BudgetInsertOne")]
		void Insert(Budget budget);

		///	<summary>
		///	Updates a single Budget calling the
		///	stored procedure BudgetUpdateOne
		///	</summary>
		[Sql("BudgetUpdateOne")]
		void Update(Budget budget);

		///	<summary>
		///	Updates many Budgets calling the
		///	stored procedure BudgetUpdateMany
		///	</summary>
		[Sql("BudgetUpdateMany")]
		void Update(IEnumerable<Budget> budgets);

		///	<summary>
		///	Deletes a single Budget calling the
		///	stored procedure BudgetDeleteOne
		///	</summary>
		[Sql("BudgetDeleteOne")]
		void Delete(Budget budget);
	}
}