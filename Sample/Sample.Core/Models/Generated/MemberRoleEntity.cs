using System;
using Insight.Database;

namespace Sample.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents MemberRole - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class MemberRoleEntity
	{
		#region Primary Key Properties
		
		///	<summary>
		///	Gets / Sets database column 'memberID' (primary key)
		///	</summary>
		[Column("memberID")]
		public virtual int MemberId { get; set; }
		
		
		///	<summary>
		///	Gets / Sets database column 'roleID' (primary key)
		///	</summary>
		[Column("roleID")]
		public virtual int RoleId { get; set; }
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'createdAt'
		///	</summary>
		[Column("createdAt")]
		public virtual DateTime CreatedAt { get; set; }

		
		#endregion
	}
}