using System.Net.Http;
using System.Threading;
using System.Web.Http;
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
    ///     var actual = response.Result.Content.ToJsonArray();
    /// 
    ///     var expected = accounts.ToJsonArray(settings);
    /// 
    ///     Assert.IsTrue(JToken.DeepEquals(actual, expected));
    /// ]]>
    /// </code>
    /// </remarks>
    static class JsonTestExtensions
    {
        public static HttpResponseMessage ToMessage(this IHttpActionResult results)
        {
            var response = results.ExecuteAsync(CancellationToken.None);

            response.Wait();

            return response.Result;
        }

        public static JArray ToJsonArray(this HttpContent content)
        {
            var json = content.ReadAsStringAsync().Result;

            var actual = JsonConvert.DeserializeObject(json) as JArray;

            return actual;
        }

        public static JArray ToJsonArray<T>(this T expected, JsonSerializerSettings settings)
        {
            var json = JsonConvert.SerializeObject(expected, settings);

            return JsonConvert.DeserializeObject(json) as JArray;
        }

        public static JObject ToJsonObject(this HttpContent content)
        {
            var json = content.ReadAsStringAsync().Result;

            var actual = JsonConvert.DeserializeObject(json) as JObject;

            return actual;
        }

        public static JObject ToJsonObject<T>(this T expected, JsonSerializerSettings settings)
        {
            var json = JsonConvert.SerializeObject(expected, settings);

            return JsonConvert.DeserializeObject(json) as JObject;
        }
    }
}
