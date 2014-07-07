using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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