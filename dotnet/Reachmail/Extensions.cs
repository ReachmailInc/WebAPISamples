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
    public static class Extensions
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

        public static Stream ToJsonStream(this object source)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(source)));
        }

        public static object FromJson(this string json, Type type)
        {
            return new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.Deserialize(json, type);
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
    }
}