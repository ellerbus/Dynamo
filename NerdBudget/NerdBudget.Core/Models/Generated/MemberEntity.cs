using System;
using Insight.Database;

namespace NerdBudget.Core.Models.Entities
{
	///	<summary>
	/// Base Entity class that represents MEMBER - Intended to be extended
	///	for business rule implementation
	///	</summary>
	public abstract class MemberEntity
	{
		#region Constructors

		protected MemberEntity() {}

		protected MemberEntity(string name, string password, DateTime loggedInAt, DateTime createdAt, DateTime? updatedAt)
		{ 
			_name = name;
			_password = password;
			_loggedInAt = loggedInAt;
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
		///	Gets / Sets database column 'member_name' (primary key)
		///	</summary>
		[Column("member_name")]
		public virtual string Name
		{
			get { return _name; }
			set { IsModified |= _name != value; _name = value; }
		}
		private string _name;
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'member_password'
		///	</summary>
		[Column("member_password")]
		public virtual string Password
		{
			get { return _password; }
			set { IsModified |= _password != value; _password = value; }
		}
		private string _password;

		
		///	<summary>
		///	Gets / Sets database column 'logged_in_at'
		///	</summary>
		[Column("logged_in_at")]
		public virtual DateTime LoggedInAt
		{
			get { return _loggedInAt; }
			set { IsModified |= _loggedInAt != value; _loggedInAt = value; }
		}
		private DateTime _loggedInAt;

		
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