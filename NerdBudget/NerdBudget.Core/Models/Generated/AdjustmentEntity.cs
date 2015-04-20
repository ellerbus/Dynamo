using System;
using Insight.Database;

namespace NerdBudget.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents ADJUSTMENT - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class AdjustmentEntity
	{
		#region Constructors

		protected AdjustmentEntity() {}

		protected AdjustmentEntity(string accountId, string id, string budgetId, string name, DateTime? date, double amount, DateTime createdAt, DateTime? updatedAt)
		{ 
			_accountId = accountId;
			_id = id;
			_budgetId = budgetId;
			_name = name;
			_date = date;
			_amount = amount;
			_createdAt = createdAt;
			_updatedAt = updatedAt;
		}
		
		#endregion

		#region Misc Properties
		
		///	<summary>
		///	Internally Managed 'Is' Modified Flag
		///	</summary>
		public bool IsModified { get; internal set; }
		
		#endregion

		#region Primary Key Properties
		
		///	<summary>
		///	Gets / Sets database column 'account_id' (primary key)
		///	</summary>
		[Column("account_id")]
		public virtual string AccountId
		{
			get { return _accountId; }
			set { IsModified |= _accountId != value; _accountId = value; }
		}
		private string _accountId;
		
		
		///	<summary>
		///	Gets / Sets database column 'adjustment_id' (primary key)
		///	</summary>
		[Column("adjustment_id")]
		public virtual string Id
		{
			get { return _id; }
			set { IsModified |= _id != value; _id = value; }
		}
		private string _id;
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'budget_id'
		///	</summary>
		[Column("budget_id")]
		public virtual string BudgetId
		{
			get { return _budgetId; }
			set { IsModified |= _budgetId != value; _budgetId = value; }
		}
		private string _budgetId;

		
		///	<summary>
		///	Gets / Sets database column 'adjustment_name'
		///	</summary>
		[Column("adjustment_name")]
		public virtual string Name
		{
			get { return _name; }
			set { IsModified |= _name != value; _name = value; }
		}
		private string _name;

		
		///	<summary>
		///	Gets / Sets database column 'adjustment_date'
		///	</summary>
		[Column("adjustment_date")]
		public virtual DateTime? Date
		{
			get { return _date; }
			set { IsModified |= _date != value; _date = value; }
		}
		private DateTime? _date;

		
		///	<summary>
		///	Gets / Sets database column 'amount'
		///	</summary>
		[Column("amount")]
		public virtual double Amount
		{
			get { return _amount; }
			set { IsModified |= _amount != value; _amount = value; }
		}
		private double _amount;

		
		///	<summary>
		///	Gets / Sets database column 'created_at'
		///	</summary>
		[Column("created_at")]
		public virtual DateTime CreatedAt
		{
			get { return _createdAt; }
			set { IsModified |= _createdAt != value; _createdAt = value; }
		}
		private DateTime _createdAt;

		
		///	<summary>
		///	Gets / Sets database column 'updated_at'
		///	</summary>
		[Column("updated_at")]
		public virtual DateTime? UpdatedAt
		{
			get { return _updatedAt; }
			set { IsModified |= _updatedAt != value; _updatedAt = value; }
		}
		private DateTime? _updatedAt;

		
		#endregion
	}
}