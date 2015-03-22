using Insight.Database;
using NerdBudget.Core.Models;

namespace NerdBudget.Core.Repositories
{
    ///	<summary>
    /// Repository Interface for 
    ///	</summary>
    public interface IMemberRepository
    {
        ///	<summary>
        ///	Gets a single Member by primary key
        ///	</summary>
        [Sql("select * from MEMBER where member_name = @member_name")]
        Member Get(string member_name);

        ///	<summary>
        ///	Saves a single Member calling the
        ///	stored procedure MemberUpsertOne
        ///	</summary>
        [Sql("MemberUpsertOne")]
        void Save(Member member);
    }
}