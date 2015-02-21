using System;
using Insight.Database;

namespace NerdBudget.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents ACCOUNT - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class AccountEntity
	{
		#region Constructors

		protected AccountEntity() {}

		protected AccountEntity(string id, string name, string type, DateTime startedAt, DateTime createdAt, DateTime? updatedAt)
		{ 
			_id = id;
			_name = name;
			_type = type;
			_startedAt = startedAt;
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
		public virtual string Id
		{
			get { return _id; }
			set { IsModified = _id != value; _id = value; }
		}
		private string _id;
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'account_name'
		///	</summary>
		[Column("account_name")]
		public virtual string Name
		{
			get { return _name; }
			set { IsModified = _name != value; _name = value; }
		}
		private string _name;

		
		///	<summary>
		///	Gets / Sets database column 'account_type'
		///	</summary>
		[Column("account_type")]
		public virtual string Type
		{
			get { return _type; }
			set { IsModified = _type != value; _type = value; }
		}
		private string _type;

		
		///	<summary>
		///	Gets / Sets database column 'started_at'
		///	</summary>
		[Column("started_at")]
		public virtual DateTime StartedAt
		{
			get { return _startedAt; }
			set { IsModified = _startedAt != value; _startedAt = value; }
		}
		private DateTime _startedAt;

		
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