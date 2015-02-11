using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NerdBudget.Tests
{
    /// <summary>
    /// Helper Extensions for Testing API Controllers using Reduced Payload Manager
    /// </summary>
    /// <remarks>
    /// <code>
    /// <![CDATA[
    ///     //		arrange...
    /// 
    ///     var settings = PayloadManager.AddPayload<Account>("Id", "Name").ToSettings();
    /// 
    ///     //		act...
    ///     var result = SubjectUnderTest.GetAll();
    /// 
    ///     var response = result.ExecuteAsync(CancellationToken.None);
    /// 
    ///     response.Wait();
    /// 
    ///     //		assert...
    ///     Assert.IsTrue(response.Result.IsSuccessStatusCode);
    /// 
    ///     var actual = response.Result.Content.AssertJsonArrayEquality(
    /// 
    ///     var expected = accounts.ToJsonArray(settings);
    /// 
    ///     Assert.IsTrue(JToken.DeepEquals(actual, expected));
    /// ]]>
    /// </code>
    /// </remarks>
    static class JsonTestExtensions
    {
        #region Message

        public static HttpResponseMessage ToMessage(this IHttpActionResult results)
        {
            var response = results.ExecuteAsync(CancellationToken.None);

            response.Wait();

            return response.Result;
        }

        #endregion

        #region JArray

        private static JArray ToJsonArray(this HttpContent content)
        {
            var json = content.ReadAsStringAsync().Result;

            var actual = JsonConvert.DeserializeObject(json) as JArray;

            Assert.IsNotNull(actual, "Expected JArray on deserialize");

            return actual;
        }

        private static JArray ToJsonArray<T>(this T expected, JsonSerializerSettings settings)
        {
            var json = JsonConvert.SerializeObject(expected, settings);

            var array = JsonConvert.DeserializeObject(json) as JArray;

            Assert.IsNotNull(array, "Expected JArray on deserialize");

            return array;
        }

        #endregion

        #region JObject

        private static JObject ToJsonObject(this HttpContent content)
        {
            var json = content.ReadAsStringAsync().Result;

            var actual = JsonConvert.DeserializeObject(json) as JObject;

            return actual;
        }

        private static JObject ToJsonObject<T>(this T expected, JsonSerializerSettings settings)
        {
            var json = JsonConvert.SerializeObject(expected, settings);

            return JsonConvert.DeserializeObject(json) as JObject;
        }

        #endregion

        #region Assert Equality

        public static void AssertJsonArrayEquality<T>(this HttpContent content, T expected, JsonSerializerSettings expectedSettings)
        {
            var actualJson = content.ToJsonArray();

            var expectedJson = expected.ToJsonArray(expectedSettings);

            Assert.IsTrue(JToken.DeepEquals(actualJson, expectedJson));
        }

        public static void AssertJsonObjectEquality<T>(this HttpContent content, T expected, JsonSerializerSettings expectedSettings)
        {
            var actualJson = content.ToJsonObject();

            var expectedJson = expected.ToJsonObject(expectedSettings);

            Assert.IsTrue(JToken.DeepEquals(actualJson, expectedJson));
        }

        #endregion
    }
}
