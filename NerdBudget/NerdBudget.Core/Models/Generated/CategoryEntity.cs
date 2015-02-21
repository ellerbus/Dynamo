using System;
using Insight.Database;

namespace NerdBudget.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents CATEGORY - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class CategoryEntity
	{
		#region Constructors

		protected CategoryEntity() {}

		protected CategoryEntity(string accountId, string id, string name, int multiplier, int sequence, DateTime createdAt, DateTime? updatedAt)
		{ 
			_accountId = accountId;
			_id = id;
			_name = name;
			_multiplier = multiplier;
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
			set { IsModified = _accountId != value; _accountId = value; }
		}
		private string _accountId;
		
		
		///	<summary>
		///	Gets / Sets database column 'category_id' (primary key)
		///	</summary>
		[Column("category_id")]
		public virtual string Id
		{
			get { return _id; }
			set { IsModified = _id != value; _id = value; }
		}
		private string _id;
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'category_name'
		///	</summary>
		[Column("category_name")]
		public virtual string Name
		{
			get { return _name; }
			set { IsModified = _name != value; _name = value; }
		}
		private string _name;

		
		///	<summary>
		///	Gets / Sets database column 'multiplier'
		///	</summary>
		[Column("multiplier")]
		public virtual int Multiplier
		{
			get { return _multiplier; }
			set { IsModified = _multiplier != value; _multiplier = value; }
		}
		private int _multiplier;

		
		///	<summary>
		///	Gets / Sets database column 'sequence'
		///	</summary>
		[Column("sequence")]
		public virtual int Sequence
		{
			get { return _sequence; }
			set { IsModified = _sequence != value; _sequence = value; }
		}
		private int _sequence;

		
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