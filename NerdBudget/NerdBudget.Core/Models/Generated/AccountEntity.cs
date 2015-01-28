using System;
using Insight.Database;

namespace NerdBudget.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents ACCOUNT - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class AccountEntity
	{
		#region Primary Key Properties
		
		///	<summary>
		///	Gets / Sets database column 'account_id' (primary key)
		///	</summary>
		[Column("account_id")]
		public virtual string Id { get; set; }
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'account_name'
		///	</summary>
		[Column("account_name")]
		public virtual string Name { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'account_type'
		///	</summary>
		[Column("account_type")]
		public virtual string Type { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'started_at'
		///	</summary>
		[Column("started_at")]
		public virtual DateTime StartedAt { get; set; }

		
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