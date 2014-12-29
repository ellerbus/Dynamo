using System;
using System.Collections.Generic;
using Insight.Database;
using Sample.Core.Models;

namespace Sample.Core.Repositories
{
	///	<summary>
	/// Repository Interface for 
	///	</summary>
	public interface IMemberRoleRepository
	{
		/////	<summary>
		/////	Dynamic SQL Sample
		/////	</summary>
		//[Sql("select * from MemberRole where createdAt > @createdAfter")]
		//IList<MemberRole> GetList(DateTime createdAfter);

		///	<summary>
		///	Gets many MemberRoles by calling the
		///	stored procedure MemberRoleSelectMany
		///	</summary>
		[Sql("MemberRoleSelectMany")]
		IList<MemberRole> Get();
	
		///	<summary>
		///	Gets a single MemberRole by primary key calling the
		///	stored procedure MemberRoleSelectOne
		///	</summary>
		[Sql("MemberRoleSelectOne")]
		MemberRole Get(int memberId, int roleId);

		///	<summary>
		///	Inserts a single MemberRole calling the
		///	stored procedure MemberRoleInsertOne
		///	</summary>
		[Sql("MemberRoleInsertOne")]
		void Insert(MemberRole memberRole);

		///	<summary>
		///	Inserts many MemberRoles calling the
		///	stored procedure MemberRoleInsertMany
		///	</summary>
		[Sql("MemberRoleInsertMany")]
		void Insert(IEnumerable<MemberRole> memberRoles);

		///	<summary>
		///	Updates a single MemberRole calling the
		///	stored procedure MemberRoleUpdateOne
		///	</summary>
		[Sql("MemberRoleUpdateOne")]
		void Update(MemberRole memberRole);

		///	<summary>
		///	Updates many MemberRoles calling the
		///	stored procedure MemberRoleUpdateMany
		///	</summary>
		[Sql("MemberRoleUpdateMany")]
		void Update(IEnumerable<MemberRole> memberRoles);

		///	<summary>
		///	Deletes a single MemberRole calling the
		///	stored procedure MemberRoleDeleteOne
		///	</summary>
		[Sql("MemberRoleDeleteOne")]
		void Delete(MemberRole memberRole);

		///	<summary>
		///	Deletes many MemberRoles calling the
		///	stored procedure MemberRoleDeleteMany
		///	</summary>
		[Sql("MemberRoleDeleteMany")]
		void Delete(IEnumerable<MemberRole> memberRoles);

		///	<summary>
		///	Saves a single MemberRole calling the
		///	stored procedure MemberRoleUpsertOne
		///	</summary>
		[Sql("MemberRoleUpsertOne")]
		void Save(MemberRole memberRole);

		///	<summary>
		///	Saves many MemberRoles calling the
		///	stored procedure MemberRoleUpsertMany
		///	</summary>
		[Sql("MemberRoleUpsertMany")]
		void Save(IEnumerable<MemberRole> memberRoles);
	}
}