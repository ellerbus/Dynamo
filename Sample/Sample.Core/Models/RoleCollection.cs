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
	public partial class RoleCollection : Collection<Role>
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
				
		#endregion
	}
}