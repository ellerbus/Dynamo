using System.Collections.Generic;
using Insight.Database;
using NerdBudget.Core.Models;

namespace NerdBudget.Core.Repositories
{
    ///	<summary>
    /// Repository Interface for 
    ///	</summary>
    public interface IBalanceRepository
    {
        ///	<summary>
        ///	Saves many Balances calling the
        ///	stored procedure BalanceUpsertMany
        ///	</summary>
        [Sql("BalanceUpsertMany")]
        void Save(IEnumerable<Balance> balances);
    }
}