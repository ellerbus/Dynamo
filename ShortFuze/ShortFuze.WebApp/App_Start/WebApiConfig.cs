using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ShortFuze.WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            JsonSerializerSettings jss = config.Formatters.OfType<JsonMediaTypeFormatter>().First().SerializerSettings;

            jss.ContractResolver = new CamelCasePropertyNamesContractResolver();

            jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }

    public class Logger : IExceptionLogger
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            log.Fatal("WebAPI Unhandled Exception", context.Exception);

            return Task.FromResult(0);
        }
    }

    public class PlainTextMediaTypeFormatter : MediaTypeFormatter
    {
        public PlainTextMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var taskCompletionSource = new TaskCompletionSource<object>();

            try
            {
                MemoryStream memoryStream = new MemoryStream();
                readStream.CopyTo(memoryStream);

                var s = Encoding.UTF8.GetString(memoryStream.ToArray());
                taskCompletionSource.SetResult(s);
            }
            catch (Exception e)
            {
                taskCompletionSource.SetException(e);
            }

            return taskCompletionSource.Task;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, System.Net.TransportContext transportContext, System.Threading.CancellationToken cancellationToken)
        {
            var buff = Encoding.UTF8.GetBytes(value.ToString());
            return writeStream.WriteAsync(buff, 0, buff.Length, cancellationToken);
        }

        public override bool CanReadType(Type type)
        {
            return type == typeof(string);
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(string);
        }
    }
}
