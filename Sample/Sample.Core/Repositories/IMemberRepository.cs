using System;
using System.Collections.Generic;
using Insight.Database;
using Sample.Core.Models;

namespace Sample.Core.Repositories
{
	///	<summary>
	/// Repository Interface for 
	///	</summary>
	public interface IMemberRepository
	{
		///	<summary>
		///	Gets a single Member by primary key calling the
		///	stored procedure MemberSelect
		///	</summary>
		[Sql("MemberSelect")]
		Member Get(int memberId);

		///	<summary>
		///	Deletes a single Member calling the
		///	stored procedure MemberDelete
		///	</summary>
		[Sql("MemberDelete")]
		void Delete(Member member);

		///	<summary>
		///	Deletes many Members calling the
		///	stored procedure MemberDeleteMany
		///	</summary>
		[Sql("MemberDeleteMany")]
		void Delete(IEnumerable<Member> members);

		///	<summary>
		///	Saves a single Member calling the
		///	stored procedure MemberUpsert
		///	</summary>
		[Sql("MemberUpsert")]
		void Save(Member member);

		///	<summary>
		///	Saves many Members calling the
		///	stored procedure MemberUpsertMany
		///	</summary>
		[Sql("MemberUpsertMany")]
		void Save(IEnumerable<Member> members);
	}
}