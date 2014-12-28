using System;
using System.Diagnostics;
using Augment;

namespace Sample.Core.Models
{
	///	<summary>
	///
	///	</summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public partial class Role
	{	
		#region ToString/DebuggerDisplay

		///	<summary>
		///	DebuggerDisplay for this object
		///	</summary>
		private string DebuggerDisplay
		{
			get
			{
				string pk = "[" + Id + "]";
				
				string uq = "[" + Name + "]";
				
				return "{0}, pk={1}, uq={2}".FormatArgs(GetType().Name, pk, uq);
			}
		}
		
		#endregion

		#region Foreign Key Properties
				
		#endregion
	}
}