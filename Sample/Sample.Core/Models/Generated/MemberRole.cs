using System;
using Insight.Database;

namespace Sample.Core.Models
{
	///	<summary>
	///
	///	</summary>
	public partial class MemberRole
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
		
		#endregion
	}
}