using System;
using Insight.Database;

namespace Sample.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents Member - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class MemberEntity
	{
		#region Primary Key Properties
		
		///	<summary>
		///	Gets / Sets database column 'memberID' (primary key)
		///	</summary>
		[Column("memberID")]
		public virtual int Id { get; set; }
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'memberName'
		///	</summary>
		[Column("memberName")]
		public virtual string Name { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'createdAt'
		///	</summary>
		[Column("createdAt")]
		public virtual DateTime CreatedAt { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'updatedAt'
		///	</summary>
		[Column("updatedAt")]
		public virtual DateTime? UpdatedAt { get; set; }

		
		#endregion
	}
}