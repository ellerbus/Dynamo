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
		///	stored procedure SelectMember
		///	</summary>
		[Sql("SelectMember")]
		Member Get(int memberId);

		///	<summary>
		///	Deletes a single Member calling the
		///	stored procedure DeleteMember
		///	</summary>
		[Sql("DeleteMember")]
		void Delete(Member member);

		///	<summary>
		///	Deletes many Members calling the
		///	stored procedure DeleteMembers
		///	</summary>
		[Sql("DeleteMembers")]
		void Delete(IEnumerable<Member> members);

		///	<summary>
		///	Saves a single Member calling the
		///	stored procedure UpsertMember
		///	</summary>
		[Sql("UpsertMember")]
		void Save(Member member);

		///	<summary>
		///	Saves many Members calling the
		///	stored procedure UpsertMembers
		///	</summary>
		[Sql("UpsertMembers")]
		void Save(IEnumerable<Member> members);
	}
}