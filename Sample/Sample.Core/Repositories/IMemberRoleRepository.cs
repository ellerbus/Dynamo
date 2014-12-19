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
		///	<summary>
		///	Gets a single MemberRole by primary key calling the
		///	stored procedure MemberRoleSelect
		///	</summary>
		[Sql("MemberRoleSelect")]
		MemberRole Get(int memberId, int roleId);

		///	<summary>
		///	Deletes a single MemberRole calling the
		///	stored procedure MemberRoleDelete
		///	</summary>
		[Sql("MemberRoleDelete")]
		void Delete(MemberRole memberRole);

		///	<summary>
		///	Deletes many MemberRoles calling the
		///	stored procedure MemberRoleDeleteMany
		///	</summary>
		[Sql("MemberRoleDeleteMany")]
		void Delete(IEnumerable<MemberRole> memberRoles);

		///	<summary>
		///	Saves a single MemberRole calling the
		///	stored procedure MemberRoleUpsert
		///	</summary>
		[Sql("MemberRoleUpsert")]
		void Save(MemberRole memberRole);

		///	<summary>
		///	Saves many MemberRoles calling the
		///	stored procedure MemberRoleUpsertMany
		///	</summary>
		[Sql("MemberRoleUpsertMany")]
		void Save(IEnumerable<MemberRole> memberRoles);
	}
}