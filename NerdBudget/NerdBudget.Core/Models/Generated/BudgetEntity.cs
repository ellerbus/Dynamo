using System;
using Insight.Database;

namespace NerdBudget.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents BUDGET - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class BudgetEntity
	{
		#region Primary Key Properties
		
		///	<summary>
		///	Gets / Sets database column 'account_id' (primary key)
		///	</summary>
		[Column("account_id")]
		public virtual string AccountId { get; set; }
		
		
		///	<summary>
		///	Gets / Sets database column 'budget_id' (primary key)
		///	</summary>
		[Column("budget_id")]
		public virtual string Id { get; set; }
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'category_id'
		///	</summary>
		[Column("category_id")]
		public virtual string CategoryId { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'budget_name'
		///	</summary>
		[Column("budget_name")]
		public virtual string Name { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'budget_frequency'
		///	</summary>
		[Column("budget_frequency")]
		public virtual string Frequency { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'sequence'
		///	</summary>
		[Column("sequence")]
		public virtual int Sequence { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'start_date'
		///	</summary>
		[Column("start_date")]
		public virtual DateTime? StartDate { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'end_date'
		///	</summary>
		[Column("end_date")]
		public virtual DateTime? EndDate { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'amount'
		///	</summary>
		[Column("amount")]
		public virtual double Amount { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'created_at'
		///	</summary>
		[Column("created_at")]
		public virtual DateTime CreatedAt { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'updated_at'
		///	</summary>
		[Column("updated_at")]
		public virtual DateTime? UpdatedAt { get; set; }

		
		#endregion
	}
}