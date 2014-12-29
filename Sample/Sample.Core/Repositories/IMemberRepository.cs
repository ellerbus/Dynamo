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
		/////	<summary>
		/////	Dynamic SQL Sample
		/////	</summary>
		//[Sql("select * from Member where createdAt > @createdAfter")]
		//IList<Member> GetList(DateTime createdAfter);

		///	<summary>
		///	Gets many Members by calling the
		///	stored procedure MemberSelectMany
		///	</summary>
		[Sql("MemberSelectMany")]
		IList<Member> Get();
	
		///	<summary>
		///	Gets a single Member by primary key calling the
		///	stored procedure MemberSelectOne
		///	</summary>
		[Sql("MemberSelectOne")]
		Member Get(int id);

		///	<summary>
		///	Inserts a single Member calling the
		///	stored procedure MemberInsertOne
		///	</summary>
		[Sql("MemberInsertOne")]
		void Insert(Member member);

		///	<summary>
		///	Inserts many Members calling the
		///	stored procedure MemberInsertMany
		///	</summary>
		[Sql("MemberInsertMany")]
		void Insert(IEnumerable<Member> members);

		///	<summary>
		///	Updates a single Member calling the
		///	stored procedure MemberUpdateOne
		///	</summary>
		[Sql("MemberUpdateOne")]
		void Update(Member member);

		///	<summary>
		///	Updates many Members calling the
		///	stored procedure MemberUpdateMany
		///	</summary>
		[Sql("MemberUpdateMany")]
		void Update(IEnumerable<Member> members);

		///	<summary>
		///	Deletes a single Member calling the
		///	stored procedure MemberDeleteOne
		///	</summary>
		[Sql("MemberDeleteOne")]
		void Delete(Member member);

		///	<summary>
		///	Deletes many Members calling the
		///	stored procedure MemberDeleteMany
		///	</summary>
		[Sql("MemberDeleteMany")]
		void Delete(IEnumerable<Member> members);

		///	<summary>
		///	Saves a single Member calling the
		///	stored procedure MemberUpsertOne
		///	</summary>
		[Sql("MemberUpsertOne")]
		void Save(Member member);

		///	<summary>
		///	Saves many Members calling the
		///	stored procedure MemberUpsertMany
		///	</summary>
		[Sql("MemberUpsertMany")]
		void Save(IEnumerable<Member> members);
	}
}