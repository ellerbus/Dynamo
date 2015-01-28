using System;
using Insight.Database;

namespace NerdBudget.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents CATEGORY - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class CategoryEntity
	{
		#region Primary Key Properties
		
		///	<summary>
		///	Gets / Sets database column 'account_id' (primary key)
		///	</summary>
		[Column("account_id")]
		public virtual string AccountId { get; set; }
		
		
		///	<summary>
		///	Gets / Sets database column 'category_id' (primary key)
		///	</summary>
		[Column("category_id")]
		public virtual string Id { get; set; }
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'category_name'
		///	</summary>
		[Column("category_name")]
		public virtual string Name { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'multiplier'
		///	</summary>
		[Column("multiplier")]
		public virtual int Multiplier { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'sequence'
		///	</summary>
		[Column("sequence")]
		public virtual int Sequence { get; set; }

		
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