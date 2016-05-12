using System;
using System.Collections.Generic;
using Augment;

namespace ShortFuze.Core
{
    public interface ISecurityActor
    {
        string UserName { get; }

        void EnsureAudit<T>(T item);

        void EnsureAudits<T>(IEnumerable<T> items);

        //bool Can (View|Update) Model (Model item);
        //VerifyCan (View|Update) Model (Model item); throws SecurityException
    }

    public class SecurityActor : ISecurityActor
    {
        #region Constructors

        public SecurityActor(string userName)
        {
            UserName = userName;
        }

        #endregion

        #region Methods

        public void EnsureAudits<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                EnsureAudit<T>(item);
            }
        }

        public void EnsureAudit<T>(T item)
        {
            if (ReflectionHelper.HasProperty(item, "CreatedBy"))
            {
                string name = (string)ReflectionHelper.GetValueOfProperty(item, "CreatedBy");

                if (name.IsNullOrEmpty())
                {
                    ReflectionHelper.SetValueOfProperty(item, "CreatedBy", name);
                }
                else if (ReflectionHelper.HasProperty(item, "ModifiedBy"))
                {
                    ReflectionHelper.SetValueOfProperty(item, "ModifiedBy", name);
                }
            }

            if (ReflectionHelper.HasProperty(item, "CreatedAt"))
            {
                DateTime dttm = (DateTime)ReflectionHelper.GetValueOfProperty(item, "CreatedAt");

                if (dttm == DateTime.MinValue)
                {
                    ReflectionHelper.SetValueOfProperty(item, "CreatedAt", dttm);
                }
                else if (ReflectionHelper.HasProperty(item, "ModifiedAt"))
                {
                    ReflectionHelper.SetValueOfProperty(item, "ModifiedAt", dttm);
                }
            }
        }

        #endregion

        #region Properties

        public string UserName { get; private set; }

        #endregion
    }
}
