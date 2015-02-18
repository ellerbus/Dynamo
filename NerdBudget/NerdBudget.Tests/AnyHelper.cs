using System;
using System.Collections.Generic;
using System.Linq;
using Augment;
using Augment.Caching;
using Moq;

namespace NerdBudget.Tests
{
    public class Helpers
    {
        #region Static Helpers

        public const string OriginalLedgerText = "02/01/2015\tAB\t$1\t\t$2\tA";

        #endregion
    }

    public class AnyHelper<T> where T : class
    {
        #region Value Types

        public string String { get { return It.IsAny<string>(); } }

        public string[] StringArray { get { return It.IsAny<string[]>(); } }

        public int Int { get { return It.IsAny<int>(); } }

        public double Double { get { return It.IsAny<double>(); } }

        public DateTime DateTime { get { return It.IsAny<DateTime>(); } }

        public TimeSpan TimeSpan { get { return It.IsAny<TimeSpan>(); } }

        public CacheExpiration CacheExpiration { get { return It.IsAny<CacheExpiration>(); } }

        public CachePriority CachePriority { get { return It.IsAny<CachePriority>(); } }

        public string Key { get { return typeof(T).FullName + ";"; } }

        public string ListKey { get { return Key + "Enumerable;"; } }

        #endregion

        #region Reference Types

        public T Item { get { return It.IsAny<T>(); } }

        public IList<T> List { get { return It.IsAny<IList<T>>(); } }

        public IEnumerable<T> Enumerable { get { return It.IsAny<IEnumerable<T>>(); } }

        public Func<T> FuncItem { get { return It.IsAny<Func<T>>(); } }

        public Func<IList<T>> FuncList { get { return It.IsAny<Func<IList<T>>>(); } }

        #endregion

        #region Methods

        public string GetByPrimaryKey(params object[] pk)
        {
            string pkey = pk.Select(x => x.ToString()).Join(",");

            string key = "{0}{1};".FormatArgs(Key, pkey);

            return key;
        }

        #endregion
    }
}
