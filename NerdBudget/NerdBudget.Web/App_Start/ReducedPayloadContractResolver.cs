using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NerdBudget.Web
{
    /// <summary>
    /// Adds a fluent flavor
    /// </summary>
    /// <remarks>
    /// Usage:
    /// <code>
    /// <![CDATA[
    /// JsonSerializerSettings jss = PayloadManager
    ///   .AddPayload<Member>("Id", "Name", "EMail", "Addresses")
    ///   .AddPayload<Address>("Street", "City", "State")
    ///   .ToSettings();
    ///   
    /// JsonSerializerSettings jss = PayloadManager
    ///   .AddPayload<Member>()            // includes all public|instance value or string type properties
    ///   .AddPayload<Member>("Addresses") // appends 'Addresses' property to the existing list for Member
    ///   .AddPayload<Address>()
    ///   .ToSettings();
    /// ]]>
    /// </code>
    /// </remarks>
    public static class PayloadManager
    {
        /// <summary>
        /// Adds a list of property names to be included in the JSON serialization. Automatically
        /// includes all Public|Instance properties that are a Value Type or String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IReducedPayloadContractResolver AddStandardPayload<T>()
        {
            return new ReducedPayloadContractResolver().AddStandardPayload<T>();
        }

        /// <summary>
        /// Adds a list of property names to be included in the JSON serialization. Automatically
        /// includes all Public|Instance properties that are a Value Type or String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="additionalProperties">Additional Properties that are not value types or strings</param>
        /// <returns></returns>
        public static IReducedPayloadContractResolver AddStandardPayload<T>(string additionalProperties)
        {
            return new ReducedPayloadContractResolver().AddStandardPayload<T>(additionalProperties);
        }

        /// <summary>
        /// Adds a list of property names to be included in the JSON serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="properties">comma-delimited</param>
        /// <returns></returns>
        public static IReducedPayloadContractResolver AddPayload<T>(string properties)
        {
            return new ReducedPayloadContractResolver().AddPayload<T>(properties);
        }

        /// <summary>
        /// Adds a list of property names to be included in the JSON serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IReducedPayloadContractResolver AddPayload<T>(IEnumerable<string> properties)
        {
            return new ReducedPayloadContractResolver().AddPayload<T>(properties);
        }
    }

    /// <summary>
    /// Interface for a fluent API
    /// </summary>
    public interface IReducedPayloadContractResolver
    {
        /// <summary>
        /// Adds a list of property names to be included in the JSON serialization. Automatically
        /// includes all Public|Instance properties that are a Value Type or String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IReducedPayloadContractResolver AddStandardPayload<T>();

        /// <summary>
        /// Adds a list of property names to be included in the JSON serialization. Automatically
        /// includes all Public|Instance properties that are a Value Type or String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="additionalProperties">Additional Properties that are not value types or strings (Comma delimited)</param>
        /// <returns></returns>
        IReducedPayloadContractResolver AddStandardPayload<T>(string additionalProperties);

        /// <summary>
        /// Adds a list of property names to be included in the JSON serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="properties"></param>
        /// <returns></returns>
        IReducedPayloadContractResolver AddPayload<T>(string properties);

        /// <summary>
        /// Adds a list of property names to be included in the JSON serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="properties"></param>
        /// <returns></returns>
        IReducedPayloadContractResolver AddPayload<T>(IEnumerable<string> properties);

        /// <summary>
        /// Create a JSON Serializer Settings object using the ReducedPayloadContractResolver
        /// setting 'PreserveReferencesHandling = PreserveReferencesHandling.Objects'
        /// </summary>
        /// <returns></returns>
        JsonSerializerSettings ToSettings();
    }

    /// <summary>
    /// Easily reduce the outbound payload of a JSON serialized object
    /// </summary>
    /// <remarks>
    /// <code>
    /// <![CDATA[
    /// ReducedPayloadContractResolver x = new ReducedPayloadContractResolver()
    ///   .AddPayload<Member>("Id", "Name", "EMail", "Addresses")
    ///   .AddPayload<Address>("Street", "City", "State");
    /// ]]>
    /// </code>
    /// </remarks>
    public class ReducedPayloadContractResolver : DefaultContractResolver, IReducedPayloadContractResolver
    {
        #region Members

        private const char Sep = ',';

        private Dictionary<Type, HashSet<string>> _payload = new Dictionary<Type, HashSet<string>>();

        #endregion

        #region Methods

        /// <summary>
        /// Adds a list of property names to be included in the JSON serialization. Automatically
        /// includes all Public|Instance properties that are a Value Type or String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="names">Comma delimited list</param>
        /// <returns></returns>
        public IReducedPayloadContractResolver AddStandardPayload<T>()
        {
            Type type = typeof(T);

            IEnumerable<string> props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.PropertyType.IsValueType || x.PropertyType == typeof(string))
                .Select(x => x.Name);

            return AddPayload<T>(props);
        }

        /// <summary>
        /// Adds a list of property names to be included in the JSON serialization. Automatically
        /// includes all Public|Instance properties that are a Value Type or String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="properties">Comma delimited list</param>
        /// <returns></returns>
        public IReducedPayloadContractResolver AddStandardPayload<T>(string properties)
        {
            Type type = typeof(T);

            AddStandardPayload<T>();

            return AddPayload<T>(properties);
        }

        /// <summary>
        /// Adds a list of property names to be included in the JSON serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="properties">Comma delimited list</param>
        /// <returns></returns>
        public IReducedPayloadContractResolver AddPayload<T>(string properties)
        {
            return AddPayload<T>(properties.Split(Sep));
        }

        /// <summary>
        /// Adds a list of property names to be included in the JSON serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="properties"></param>
        /// <returns></returns>
        public IReducedPayloadContractResolver AddPayload<T>(IEnumerable<string> properties)
        {
            Type type = typeof(T);

            HashSet<string> list = null;

            if (_payload.TryGetValue(type, out list))
            {
                foreach (string nm in properties)
                {
                    list.Add(nm);
                }
            }
            else
            {
                _payload.Add(type, new HashSet<string>(properties));
            }

            return this;
        }

        /// <summary>
        /// Create a JSON Serializer Settings object using the ReducedPayloadContractResolver
        /// setting 'PreserveReferencesHandling = PreserveReferencesHandling.Objects'
        /// </summary>
        /// <returns></returns>
        public JsonSerializerSettings ToSettings()
        {
            JsonSerializerSettings jss = new JsonSerializerSettings
            {
                ContractResolver = this,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            return jss;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            List<JsonProperty> props = new List<JsonProperty>(base.CreateProperties(type, memberSerialization));

            HashSet<string> names = null;

            if (_payload.TryGetValue(type, out names))
            {
                props.RemoveAll(x => !names.Contains(x.UnderlyingName));
            }

            return props;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            if (propertyName.Length == 1)
            {
                return propertyName.ToLower();
            }

            return char.ToLower(propertyName[0]) + propertyName.Substring(1);
        }

        #endregion
    }
}