using System;
using System.Collections.Generic;
using Insight.Database;
using NerdBudget.Core.Models;

namespace NerdBudget.Core.Repositories
{
	///	<summary>
	/// Repository Interface for 
	///	</summary>
	public interface IAdjustmentRepository
	{
		///	<summary>
		///	Deletes a single Adjustment calling the
		///	stored procedure AdjustmentDeleteOne
		///	</summary>
		[Sql("AdjustmentDeleteOne")]
		void Delete(Adjustment adjustment);

		///	<summary>
		///	Saves a single Adjustment calling the
		///	stored procedure AdjustmentUpsertOne
		///	</summary>
		[Sql("AdjustmentUpsertOne")]
		void Save(Adjustment adjustment);
	}
}