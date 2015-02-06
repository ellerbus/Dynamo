using System;
using Insight.Database;

namespace NerdBudget.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents BALANCE - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class BalanceEntity
	{
		#region Primary Key Properties
		
		///	<summary>
		///	Gets / Sets database column 'account_id' (primary key)
		///	</summary>
		[Column("account_id")]
		public virtual string AccountId { get; set; }
		
		
		///	<summary>
		///	Gets / Sets database column 'as_of' (primary key)
		///	</summary>
		[Column("as_of")]
		public virtual DateTime AsOf { get; set; }
		
				
		#endregion
		
		#region Properties
		
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