using System;
using Insight.Database;

namespace NerdBudget.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents MAP - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class MapEntity
	{
		#region Constructors

		protected MapEntity() {}

		protected MapEntity(string accountId, string budgetId, string id, string regexPattern, DateTime createdAt, DateTime? updatedAt)
		{ 
			_accountId = accountId;
			_budgetId = budgetId;
			_id = id;
			_regexPattern = regexPattern;
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
			set { IsModified = _accountId != value; _accountId = value; }
		}
		private string _accountId;
		
		
		///	<summary>
		///	Gets / Sets database column 'budget_id' (primary key)
		///	</summary>
		[Column("budget_id")]
		public virtual string BudgetId
		{
			get { return _budgetId; }
			set { IsModified = _budgetId != value; _budgetId = value; }
		}
		private string _budgetId;
		
		
		///	<summary>
		///	Gets / Sets database column 'map_id' (primary key)
		///	</summary>
		[Column("map_id")]
		public virtual string Id
		{
			get { return _id; }
			set { IsModified = _id != value; _id = value; }
		}
		private string _id;
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'regex_pattern'
		///	</summary>
		[Column("regex_pattern")]
		public virtual string RegexPattern
		{
			get { return _regexPattern; }
			set { IsModified = _regexPattern != value; _regexPattern = value; }
		}
		private string _regexPattern;

		
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