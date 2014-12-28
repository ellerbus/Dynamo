using System;
using System.Collections.Generic;
using Insight.Database;
using Sample.Core.Models;

namespace Sample.Core.Repositories
{
	///	<summary>
	/// Repository Interface for 
	///	</summary>
	public interface IRoleRepository
	{
		///	<summary>
		///	Gets a list Roles using SQL
		///	</summary>
		[Sql("select * from Role")]
		IList<Role> GetList();
	
		///	<summary>
		///	Gets a single Role by primary key calling the
		///	stored procedure RoleSelect
		///	</summary>
		[Sql("RoleSelect")]
		Role Get(int roleId);

		///	<summary>
		///	Deletes a single Role calling the
		///	stored procedure RoleDelete
		///	</summary>
		[Sql("RoleDelete")]
		void Delete(Role role);

		///	<summary>
		///	Deletes many Roles calling the
		///	stored procedure RoleDeleteMany
		///	</summary>
		[Sql("RoleDeleteMany")]
		void Delete(IEnumerable<Role> roles);

		///	<summary>
		///	Saves a single Role calling the
		///	stored procedure RoleUpsert
		///	</summary>
		[Sql("RoleUpsert")]
		void Save(Role role);

		///	<summary>
		///	Saves many Roles calling the
		///	stored procedure RoleUpsertMany
		///	</summary>
		[Sql("RoleUpsertMany")]
		void Save(IEnumerable<Role> roles);
	}
}