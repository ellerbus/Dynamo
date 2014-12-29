using System;
using System.Diagnostics;
using Augment;

namespace Sample.Core.Models
{
	///	<summary>
	///
	///	</summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public class MemberRole : Entities.MemberRoleEntity
	{	
		#region ToString/DebuggerDisplay

		///	<summary>
		///	DebuggerDisplay for this object
		///	</summary>
		private string DebuggerDisplay
		{
			get
			{
				string pk = "[" + MemberId + ","  + RoleId + "]";
				
				string uq = "[" + "]";
				
				return "{0}, pk={1}, uq={2}".FormatArgs(GetType().Name, pk, uq);
			}
		}
		
		#endregion

		#region Foreign Key Properties
		
		/////	<summary>
		/////	Gets / Sets the foreign key to 'memberID'
		/////	</summary>
		//public Member Member
		//{
		//	get
		//	{
		//		return _member;
		//	}
		//	internal set
		//	{
		//		_member = value;
		//
		//		MemberId = value == null ? default(int) : value.MemberId
		//	}
		//}
		//private Member _member
		
		
		/////	<summary>
		/////	Gets / Sets the foreign key to 'roleID'
		/////	</summary>
		//public Role Role
		//{
		//	get
		//	{
		//		return _role;
		//	}
		//	internal set
		//	{
		//		_role = value;
		//
		//		RoleId = value == null ? default(int) : value.RoleId
		//	}
		//}
		//private Role _role
		
				
		#endregion
	}
}