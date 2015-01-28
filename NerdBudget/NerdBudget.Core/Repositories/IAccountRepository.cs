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
        [Recordset(1, typeof(Category), IsChild = true, Id = "Id", GroupBy = "AccountId", Into = "AllCategories")]
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
    }
}