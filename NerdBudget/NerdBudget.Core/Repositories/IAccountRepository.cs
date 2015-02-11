using System.Collections.Generic;
using Insight.Database;
using NerdBudget.Core.Models;

namespace NerdBudget.Core.Repositories
{
    ///	<summary>
    /// Repository Interface for 
    ///	</summary>
    public interface IAccountRepository
    {
        ///	<summary>
        ///	Gets many Accounts by calling the
        ///	stored procedure AccountSelectMany
        ///	</summary>
        [Sql("AccountSelectMany")]
        IList<Account> GetList();

        ///	<summary>
        ///	Gets a single Account by primary key calling the
        ///	stored procedure AccountSelectOne
        ///	</summary>
        [Sql("AccountSelectOne")]
        [Recordset(1, typeof(Balance), IsChild = true, Id = "Id", GroupBy = "AccountId", Into = "AllBalances")]
        [Recordset(2, typeof(Category), IsChild = true, Id = "Id", GroupBy = "AccountId", Into = "AllCategories")]
        [Recordset(3, typeof(Budget), IsChild = true, Id = "Id", GroupBy = "AccountId", Into = "AllBudgets")]
        //[Recordset(4, typeof(Adjustment), IsChild = true, Id = "Id", GroupBy = "AccountId", Into = "AllAdjustment")]
        //[Recordset(5, typeof(Map), IsChild = true, Id = "Id", GroupBy = "AccountId", Into = "AllMaps")]
        [Recordset(4, typeof(Ledger), IsChild = true, Id = "Id", GroupBy = "AccountId", Into = "AllLedgers")]
        Account Get(string account_id);

        ///	<summary>
        ///	Inserts a single Account calling the
        ///	stored procedure AccountInsertOne
        ///	</summary>
        [Sql("AccountInsertOne")]
        void Insert(Account account);

        ///	<summary>
        ///	Updates a single Account calling the
        ///	stored procedure AccountUpdateOne
        ///	</summary>
        [Sql("AccountUpdateOne")]
        void Update(Account account);

        ///	<summary>
        ///	Deletes a single Account calling the
        ///	stored procedure AccountDeleteOne
        ///	</summary>
        [Sql("AccountDeleteOne")]
        void Delete(Account account);

        ///	<summary>
        ///	Saves many Balances calling the
        ///	stored procedure BalanceUpsertMany
        ///	</summary>
        [Sql("BalanceUpsertMany")]
        void Save(IEnumerable<Balance> balances);

        ///	<summary>
        ///	Saves many Ledgers calling the
        ///	stored procedure LedgerUpsertMany
        ///	</summary>
        [Sql("LedgerUpsertMany")]
        void Save(IEnumerable<Ledger> ledgers);
    }
}