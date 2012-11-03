using System;
using System.IO;
using System.Collections.Generic;
using System.Net;

namespace ReachmailApi
{
    public enum Verb { Get, Post, Put, Delete }

    public interface IHttpClient
    {
        object Execute(
            string relativeUrl,
            Verb verb,
            Dictionary<string, object> urlParameters,
            Dictionary<string, object> querystringParameters,
            object request,
            Type responseType);
    }

    public class HttpClient : IHttpClient
    {
        private readonly string _baseUrl;
        private readonly string _username;
        private readonly string _password;
        private readonly Dictionary<string, Lazy<object>> _defaultValues = 
            new Dictionary<string, Lazy<object>>();  

        public HttpClient(string baseUrl, string username, string password)
        {
            _baseUrl = baseUrl;
            _username = username;
            _password = password;
        }

        public void AddParameterDefault(string name, Func<object> value)
        {
            _defaultValues.Add(name, new Lazy<object>(value));
        }

        public object Execute(
            string relativeUrl,
            Verb verb,
            Dictionary<string, object> urlParameters,
            Dictionary<string, object> querystringParameters,
            object request,
            Type responseType)
        {
            var url = (_baseUrl + relativeUrl).ReplaceAll(
                    urlParameters.EmptyWhenNull(), 
                    querystringParameters.EmptyWhenNull(), 
                    _defaultValues.FromLazyDictionary());
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = verb.ToString().ToUpper();
            httpRequest.Credentials = new NetworkCredential(_username, _password);
            httpRequest.Accept = httpRequest.ContentType = "application/json";

            if (request != null)
            {
                using (Stream sourceStream = request as Stream ?? request.ToJsonStream(), 
                              requestStream = httpRequest.GetRequestStream())
                    sourceStream.CopyTo(requestStream);
            }

            using (var response = GetResponse(httpRequest))
            {
                if ((int)response.StatusCode >= 300) throw new RequestException(response);
                if (responseType == null) return null;
                if (responseType == typeof(Stream)) return response.GetResponseStream();
                return response.GetResponseText().FromJson(responseType);
            }
        }

        private static HttpWebResponse GetResponse(HttpWebRequest request)
        {
            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                var response = e.Response as HttpWebResponse;
                if (response == null) throw;
                return response;
            }
        }
    }

    public class RequestException : Exception
    {
        public RequestException(HttpWebResponse response)
            : base(string.Format("Request failed: {0} - {1}", response.StatusCode, response.StatusDescription))
        {
            Response = response;
            ResponseText = response.GetResponseText();
        }

        public HttpWebResponse Response { get; private set; }
        public string ResponseText { get; set; }
    }
}