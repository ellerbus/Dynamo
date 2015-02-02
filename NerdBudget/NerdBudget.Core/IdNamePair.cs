
using System;
using System.Collections.Generic;
using Augment;
namespace NerdBudget.Core
{
    /// <summary>
    /// Constructs an ID/Name pair for use with various listings
    /// </summary>
    public class IdNamePair
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public IdNamePair() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public IdNamePair(string id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an Id/Name pair from an enum using the base enum.ToString()
        /// for the id and enum.ToDescription() extension for the name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<IdNamePair> CreateFromEnum<T>()
        {
            foreach (Enum t in Enum.GetValues(typeof(T)))
            {
                string id = t.ToString();

                string nm = EnumExtensions.ToDescription(t);

                yield return new IdNamePair(id, nm);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}