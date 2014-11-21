using System;
using System.Collections.Generic;
using Insight.Database;
using Sample.Core.Models;

namespace Sample.Core.Repositories
{
	///	<summary>
	/// Repository Interface for 
	///	</summary>
	public interface IMemberVisitHistoryRepository
	{
		///	<summary>
		///	Gets a single MemberVisitHistory by primary key calling the
		///	stored procedure SelectMemberVisitHistory
		///	</summary>
		[Sql("SelectMemberVisitHistory")]
		MemberVisitHistory Get(int memberId, DateTime visitedAt);

		///	<summary>
		///	Deletes a single MemberVisitHistory calling the
		///	stored procedure DeleteMemberVisitHistory
		///	</summary>
		[Sql("DeleteMemberVisitHistory")]
		void Delete(MemberVisitHistory memberVisitHistory);

		///	<summary>
		///	Deletes many MemberVisitHistories calling the
		///	stored procedure DeleteMemberVisitHistories
		///	</summary>
		[Sql("DeleteMemberVisitHistories")]
		void Delete(IEnumerable<MemberVisitHistory> memberVisitHistories);

		///	<summary>
		///	Saves a single MemberVisitHistory calling the
		///	stored procedure UpsertMemberVisitHistory
		///	</summary>
		[Sql("UpsertMemberVisitHistory")]
		void Save(MemberVisitHistory memberVisitHistory);

		///	<summary>
		///	Saves many MemberVisitHistories calling the
		///	stored procedure UpsertMemberVisitHistories
		///	</summary>
		[Sql("UpsertMemberVisitHistories")]
		void Save(IEnumerable<MemberVisitHistory> memberVisitHistories);
	}
}