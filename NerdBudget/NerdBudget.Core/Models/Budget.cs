using System;
using System.Diagnostics;
using Augment;

namespace NerdBudget.Core.Models
{
	///	<summary>
	///
	///	</summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public class Budget : Entities.BudgetEntity
	{	
		#region ToString/DebuggerDisplay

		///	<summary>
		///	DebuggerDisplay for this object
		///	</summary>
		private string DebuggerDisplay
		{
			get
			{
				string pk = "[" + AccountId + ","  + Id + "]";
				
				string uq = "[" + AccountId + ","  + Name + "]";
				
				return "{0}, pk={1}, uq={2}".FormatArgs(GetType().Name, pk, uq);
			}
		}
		
		#endregion

		#region Foreign Key Properties
		
		/////	<summary>
		/////	Gets / Sets the foreign key to 'account_id'
		/////	</summary>
		//public Account Account
		//{
		//	get
		//	{
		//		return _account;
		//	}
		//	internal set
		//	{
		//		_account = value;
		//
		//		AccountId = value == null ? default(string) : value.AccountId;
		//	}
		//}
		//private Account _account;
		
		
		/////	<summary>
		/////	Gets / Sets the foreign key to 'category_id'
		/////	</summary>
		//public Category Category
		//{
		//	get
		//	{
		//		return _category;
		//	}
		//	internal set
		//	{
		//		_category = value;
		//
		//		CategoryId = value == null ? default(string) : value.CategoryId;
		//	}
		//}
		//private Category _category;
		
				
		#endregion
	}
}