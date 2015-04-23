using System.Collections.Generic;
using Augment;

namespace NerdBudget.Web
{
    public class CacheManifestHttpHandler : Augment.Caching.CacheManifestHttpHandler
    {
        #region Static Methods

        private static List<string> _filesToCache;

        private static object _lock = new object();

        #endregion

        protected override IEnumerable<string> GetCacheSectionFiles()
        {
            return GetNerdBudgetFiles();
        }

        private IEnumerable<string> GetNerdBudgetFiles()
        {
            lock (_lock)
            {
                if (_filesToCache == null)
                {
                    _filesToCache = new List<string>();

                    foreach (string file in GetUrlPathToFiles("~/App"))
                    {
                        _filesToCache.Add(file);
                    }

                    foreach (string file in GetUrlPathToFiles("~/Content"))
                    {
                        _filesToCache.Add(file);
                    }
                }

                return _filesToCache;
            }
        }

        protected override IEnumerable<string> GetNetworkSectionFiles()
        {
            yield break;
        }

        public override string Version
        {
            get
            {
                if (_version.IsNullOrEmpty())
                {
                    _version = GetType().Assembly.GetName().Version.ToString();
                }
                return _version;
            }
        }
        private string _version;
    }
}