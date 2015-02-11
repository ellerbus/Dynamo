using System;
using Insight.Database;

namespace NerdBudget.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents BALANCE - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class BalanceEntity
	{
		#region Constructors

		protected BalanceEntity() {}

		protected BalanceEntity(string accountId, DateTime asOf, double amount, DateTime createdAt, DateTime? updatedAt)
		{ 
			_accountId = accountId;
			_asOf = asOf;
			_amount = amount;
			_createdAt = createdAt;
			_updatedAt = updatedAt;
		}
		
		#endregion

		#region Misc Properties
		
		///	<summary>
		///	Internally Managed 'Is' Modified Flag
		///	</summary>
		public bool IsModified { get; private set; }
		
		#endregion

		#region Primary Key Properties
		
		///	<summary>
		///	Gets / Sets database column 'account_id' (primary key)
		///	</summary>
		[Column("account_id")]
		public virtual string AccountId
		{
			get { return _accountId; }
			set { IsModified = _accountId != value; _accountId = value; }
		}
		private string _accountId;
		
		
		///	<summary>
		///	Gets / Sets database column 'as_of' (primary key)
		///	</summary>
		[Column("as_of")]
		public virtual DateTime AsOf
		{
			get { return _asOf; }
			set { IsModified = _asOf != value; _asOf = value; }
		}
		private DateTime _asOf;
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'amount'
		///	</summary>
		[Column("amount")]
		public virtual double Amount
		{
			get { return _amount; }
			set { IsModified = _amount != value; _amount = value; }
		}
		private double _amount;

		
		///	<summary>
		///	Gets / Sets database column 'created_at'
		///	</summary>
		[Column("created_at")]
		public virtual DateTime CreatedAt
		{
			get { return _createdAt; }
			set { IsModified = _createdAt != value; _createdAt = value; }
		}
		private DateTime _createdAt;

		
		///	<summary>
		///	Gets / Sets database column 'updated_at'
		///	</summary>
		[Column("updated_at")]
		public virtual DateTime? UpdatedAt
		{
			get { return _updatedAt; }
			set { IsModified = _updatedAt != value; _updatedAt = value; }
		}
		private DateTime? _updatedAt;

		
		#endregion
	}
}