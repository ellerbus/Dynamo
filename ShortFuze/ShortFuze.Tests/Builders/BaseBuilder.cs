using System;
using System.Collections.Generic;

namespace ShortFuze.Tests.Builders
{
    public abstract class BaseBuilder<T>
    {
        #region Members

        public readonly string CreatedBy = "created-tester";
        public readonly DateTime CreatedAt = DateTime.Now;

        public readonly string UpdatedBy = "updated-tester";
        public readonly DateTime UpdatedAt = DateTime.Now;

        #endregion

        #region Constructors

        #endregion

        #region Methods

        public abstract T CreateNew(int idx = 0);

        public abstract T CreateExisting(int idx = 0);

        public IList<T> CreateList(int size = 10)
        {
            List<T> list = new List<T>();

            for (int x = 0; x < size; x++)
            {
                list.Add(CreateNew(x));
            }

            return list;
        }

        #endregion

        #region Properties

        #endregion
    }

}
