using System;
using Insight.Database;

namespace NerdBudget.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents BUDGET - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class BudgetEntity
	{
		#region Constructors

		protected BudgetEntity() {}

		protected BudgetEntity(string accountId, string id, string categoryId, string name, string frequency, int sequence, DateTime? startDate, DateTime? endDate, double amount, DateTime createdAt, DateTime? updatedAt)
		{ 
			_accountId = accountId;
			_id = id;
			_categoryId = categoryId;
			_name = name;
			_frequency = frequency;
			_sequence = sequence;
			_startDate = startDate;
			_endDate = endDate;
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
		///	Gets / Sets database column 'budget_id' (primary key)
		///	</summary>
		[Column("budget_id")]
		public virtual string Id
		{
			get { return _id; }
			set { IsModified |= _id != value; _id = value; }
		}
		private string _id;
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'category_id'
		///	</summary>
		[Column("category_id")]
		public virtual string CategoryId
		{
			get { return _categoryId; }
			set { IsModified |= _categoryId != value; _categoryId = value; }
		}
		private string _categoryId;

		
		///	<summary>
		///	Gets / Sets database column 'budget_name'
		///	</summary>
		[Column("budget_name")]
		public virtual string Name
		{
			get { return _name; }
			set { IsModified |= _name != value; _name = value; }
		}
		private string _name;

		
		///	<summary>
		///	Gets / Sets database column 'budget_frequency'
		///	</summary>
		[Column("budget_frequency")]
		public virtual string Frequency
		{
			get { return _frequency; }
			set { IsModified |= _frequency != value; _frequency = value; }
		}
		private string _frequency;

		
		///	<summary>
		///	Gets / Sets database column 'sequence'
		///	</summary>
		[Column("sequence")]
		public virtual int Sequence
		{
			get { return _sequence; }
			set { IsModified |= _sequence != value; _sequence = value; }
		}
		private int _sequence;

		
		///	<summary>
		///	Gets / Sets database column 'start_date'
		///	</summary>
		[Column("start_date")]
		public virtual DateTime? StartDate
		{
			get { return _startDate; }
			set { IsModified |= _startDate != value; _startDate = value; }
		}
		private DateTime? _startDate;

		
		///	<summary>
		///	Gets / Sets database column 'end_date'
		///	</summary>
		[Column("end_date")]
		public virtual DateTime? EndDate
		{
			get { return _endDate; }
			set { IsModified |= _endDate != value; _endDate = value; }
		}
		private DateTime? _endDate;

		
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