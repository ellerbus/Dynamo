using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Augment;

namespace Sample.Core.Models
{
	///	<summary>
	///
	///	</summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public partial class MemberRoleCollection : Collection<MemberRole>
	{	
		#region ToString/DebuggerDisplay

		///	<summary>
		///	DebuggerDisplay for this object
		///	</summary>
		private string DebuggerDisplay
		{
			get { return "Count={0}".FormatArgs(Count); }
		}
		
		#endregion

		#region Foreign Key Properties
		
		/////	<summary>
		/////	Gets / Sets the foreign key to 'memberID'
		/////	</summary>
		//public Member Member { get; internal set; }
		
		
		/////	<summary>
		/////	Gets / Sets the foreign key to 'roleID'
		/////	</summary>
		//public Role Role { get; internal set; }
		
				
		#endregion
	}
}