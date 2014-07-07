using System.Collections.ObjectModel;

namespace Dynamo.Core
{
    public sealed class GeneratorColumnCollection : KeyedCollection<string, GeneratorColumn>
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override string GetKeyForItem(GeneratorColumn item)
        {
            return item.Name;
        }

        #endregion
    }
}