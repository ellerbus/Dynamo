using System;
using Insight.Database;

namespace NerdBudget.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents LEDGER - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class LedgerEntity
	{
		#region Constructors

		protected LedgerEntity() {}

		protected LedgerEntity(string accountId, string id, DateTime date, string budgetId, string originalText, string description, double amount, double balance, int sequence, DateTime createdAt, DateTime? updatedAt)
		{ 
			_accountId = accountId;
			_id = id;
			_date = date;
			_budgetId = budgetId;
			_originalText = originalText;
			_description = description;
			_amount = amount;
			_balance = balance;
			_sequence = sequence;
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
		///	Gets / Sets database column 'ledger_id' (primary key)
		///	</summary>
		[Column("ledger_id")]
		public virtual string Id
		{
			get { return _id; }
			set { IsModified |= _id != value; _id = value; }
		}
		private string _id;
		
		
		///	<summary>
		///	Gets / Sets database column 'ledger_date' (primary key)
		///	</summary>
		[Column("ledger_date")]
		public virtual DateTime Date
		{
			get { return _date; }
			set { IsModified |= _date != value; _date = value; }
		}
		private DateTime _date;
		
				
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
		///	Gets / Sets database column 'original_text'
		///	</summary>
		[Column("original_text")]
		public virtual string OriginalText
		{
			get { return _originalText; }
			set { IsModified |= _originalText != value; _originalText = value; }
		}
		private string _originalText;

		
		///	<summary>
		///	Gets / Sets database column 'description'
		///	</summary>
		[Column("description")]
		public virtual string Description
		{
			get { return _description; }
			set { IsModified |= _description != value; _description = value; }
		}
		private string _description;

		
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
		///	Gets / Sets database column 'balance'
		///	</summary>
		[Column("balance")]
		public virtual double Balance
		{
			get { return _balance; }
			set { IsModified |= _balance != value; _balance = value; }
		}
		private double _balance;

		
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