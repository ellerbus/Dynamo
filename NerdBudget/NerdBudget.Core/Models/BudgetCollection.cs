using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Augment;

namespace NerdBudget.Core.Models
{
	///	<summary>
	///
	///	</summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public partial class BudgetCollection : Collection<Budget>
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
		/////	Gets / Sets the foreign key to 'account_id'
		/////	</summary>
		//public Account Account { get; internal set; }
		
		
		/////	<summary>
		/////	Gets / Sets the foreign key to 'category_id'
		/////	</summary>
		//public Category Category { get; internal set; }
		
				
		#endregion
	}
}