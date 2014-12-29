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
		/////	<summary>
		/////	Dynamic SQL Sample
		/////	</summary>
		//[Sql("select * from Role where createdAt > @createdAfter")]
		//IList<Role> GetList(DateTime createdAfter);

		///	<summary>
		///	Gets many Roles by calling the
		///	stored procedure RoleSelectMany
		///	</summary>
		[Sql("RoleSelectMany")]
		IList<Role> Get();
	
		///	<summary>
		///	Gets a single Role by primary key calling the
		///	stored procedure RoleSelectOne
		///	</summary>
		[Sql("RoleSelectOne")]
		Role Get(int id);

		///	<summary>
		///	Inserts a single Role calling the
		///	stored procedure RoleInsertOne
		///	</summary>
		[Sql("RoleInsertOne")]
		void Insert(Role role);

		///	<summary>
		///	Inserts many Roles calling the
		///	stored procedure RoleInsertMany
		///	</summary>
		[Sql("RoleInsertMany")]
		void Insert(IEnumerable<Role> roles);

		///	<summary>
		///	Updates a single Role calling the
		///	stored procedure RoleUpdateOne
		///	</summary>
		[Sql("RoleUpdateOne")]
		void Update(Role role);

		///	<summary>
		///	Updates many Roles calling the
		///	stored procedure RoleUpdateMany
		///	</summary>
		[Sql("RoleUpdateMany")]
		void Update(IEnumerable<Role> roles);

		///	<summary>
		///	Deletes a single Role calling the
		///	stored procedure RoleDeleteOne
		///	</summary>
		[Sql("RoleDeleteOne")]
		void Delete(Role role);

		///	<summary>
		///	Deletes many Roles calling the
		///	stored procedure RoleDeleteMany
		///	</summary>
		[Sql("RoleDeleteMany")]
		void Delete(IEnumerable<Role> roles);

		///	<summary>
		///	Saves a single Role calling the
		///	stored procedure RoleUpsertOne
		///	</summary>
		[Sql("RoleUpsertOne")]
		void Save(Role role);

		///	<summary>
		///	Saves many Roles calling the
		///	stored procedure RoleUpsertMany
		///	</summary>
		[Sql("RoleUpsertMany")]
		void Save(IEnumerable<Role> roles);
	}
}