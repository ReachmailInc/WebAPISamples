using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace ReachmailApi
{
    internal static class Extensions
    {
        public static IDictionary<TKey, TValue> EmptyWhenNull<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            return source ?? new Dictionary<TKey, TValue>();
        }

        public static string ReplaceAll(this string source, params IDictionary<string, object>[] replacements)
        {
            return replacements.SelectMany(x => x)
                .Where(x => x.Value != null)
                .Aggregate(source, (input, replacement) => 
                    Regex.Replace(input, replacement.Key, replacement.Value.ToString(), RegexOptions.IgnoreCase));
        }

        private static readonly Lazy<JavaScriptSerializer> Serializer = new Lazy<JavaScriptSerializer>(()=> 
            new JavaScriptSerializer()
                .AddConverters(x => x
                    .Add<DateTime>(y => y.ToString("o")) //.ToUniversalTime()
                    .Add<Enum>(y => y.ToString())));

        public static Stream ToJsonStream(this object source)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(Serializer.Value.Serialize(source)));
        }

        public static JavaScriptSerializer AddConverters(this JavaScriptSerializer serializer, Action<JsonConverter> config)
        {
            var converter = new JsonConverter();
            config(converter);
            serializer.RegisterConverters(new [] { converter});
            return serializer;
        }

        private static readonly Lazy<JavaScriptSerializer> Deserializer = new Lazy<JavaScriptSerializer>(() => 
            new JavaScriptSerializer { MaxJsonLength = int.MaxValue }); 

        public static object FromJson(this string json, Type type)
        {
            return Deserializer.Value.Deserialize(json, type);
        }

        public static string GetResponseText(this HttpWebResponse response)
        {
            using (var responseStream = response.GetResponseStream())
                return response.ContentLength != 0 ? new StreamReader(responseStream).ReadToEnd() : "";
        }

        public static HttpWebRequest SetBasicAuthCredentials(this HttpWebRequest request, string username, string password)
        {
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password)));
            return request;
        }

        public static bool IsNullableEnum(this Type type)
        {
            return type.IsGenericType && 
                type.GetGenericTypeDefinition() == typeof(Nullable<>) && 
                Nullable.GetUnderlyingType(type).IsEnum;
        }
    }
}