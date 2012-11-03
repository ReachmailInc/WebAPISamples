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
        private const string BinaryContentType = "application/binary";
        private const string JsonContentType = "application/json";

        private readonly string _baseUrl;
        private readonly string _username;
        private readonly string _password;
        private readonly Dictionary<string, object> _defaultValues = 
            new Dictionary<string, object>();  

        public HttpClient(string baseUrl, string username, string password, bool allowSelfSignedCerts)
        {
            if (allowSelfSignedCerts)
                ServicePointManager.ServerCertificateValidationCallback =
                    ((sender, certificate, chain, sslPolicyErrors) => true);

            _baseUrl = baseUrl;
            _username = username;
            _password = password;
        }

        public void AddParameterDefault(string name, object value)
        {
            _defaultValues.Add(name, value);
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
                    _defaultValues);
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = verb.ToString().ToUpper();
            httpRequest.Credentials = new NetworkCredential(_username, _password);
            httpRequest.ContentType = request is Stream ? BinaryContentType : JsonContentType;
            httpRequest.Accept = responseType == typeof(Stream) ? BinaryContentType : JsonContentType;

            if (request != null)
            {
                using (Stream sourceStream = request as Stream ?? request.ToJsonStream(), 
                              requestStream = httpRequest.GetRequestStream())
                    sourceStream.CopyTo(requestStream);
            }

            var response = GetResponse(httpRequest);
            if (responseType == typeof(Stream) && (int)response.StatusCode < 300) return response.GetResponseStream();
            using(response)
            {
                if ((int)response.StatusCode >= 300) throw new RequestException(response);
                return responseType == null ? null : response.GetResponseText().FromJson(responseType);
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