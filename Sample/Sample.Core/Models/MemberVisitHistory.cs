using System;
using System.Diagnostics;
using Augment;

namespace Sample.Core.Models
{
	///	<summary>
	///
	///	</summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public partial class MemberVisitHistory
	{
		#region ToString/DebuggerDisplay

		///	<summary>
		///	DebuggerDisplay for this object
		///	</summary>
		private string DebuggerDisplay
		{
			get
			{
				string pk = "[" + MemberId + ","  + VisitedAt.ToString("yyyy-MM-dd") +  "]";	
				
				string uq = "[" +  "]";
				
				return "{0}, pk={1}, uq={2}".FormatArgs(GetType().Name, pk, uq);
			}
		}
		
		#endregion

		#region Foreign Key Properties
		
		/////	<summary>
		/////	Gets / Sets the foreign key to 'memberID'
		/////	</summary>
		//public Member Member { get; internal set; }
		
				
		#endregion
	}
}