using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;

namespace Reachmail
{
    public enum Verb { Get, Post, Put, Delete }

    public interface IHttpClient
    {
        object Execute(
            string relativeUrl,
            Verb verb,
            Dictionary<string, object> parameters,
            object request,
            Type responseType);

        void AddParameterDefault(string name, object value);
    }

    public class HttpClient : IHttpClient
    {
        private const string BinaryContentType = "application/binary";
        private const string JsonContentType = "application/json";

        private readonly string _baseUrl;
        private readonly string _username;
        private readonly string _password;
        private readonly string _token;
        private readonly IWebProxy _proxy;
        private readonly int _timeout;

        private readonly Dictionary<string, object> _defaultValues =
            new Dictionary<string, object>();

        public HttpClient(string baseUrl, string token, bool allowSelfSignedCerts, IWebProxy proxy, int timeout = 30)
        {
            if (allowSelfSignedCerts)
                ServicePointManager.ServerCertificateValidationCallback =
                    ((sender, certificate, chain, sslPolicyErrors) => true);

            _baseUrl = baseUrl;
            _token = token;
            _proxy = proxy;
            _timeout = timeout;
        }

        public HttpClient(string baseUrl, string username, string password, bool allowSelfSignedCerts, IWebProxy proxy, int timeout = 30)
        {
            if (allowSelfSignedCerts)
                ServicePointManager.ServerCertificateValidationCallback =
                    ((sender, certificate, chain, sslPolicyErrors) => true);

            _baseUrl = baseUrl;
            _username = username;
            _password = password;
            _proxy = proxy;
            _timeout = timeout;
        }

        public void AddParameterDefault(string name, object value)
        {
            _defaultValues.Add(name, value);
        }

        public object Execute(
            string relativeUrl,
            Verb verb,
            Dictionary<string, object> parameters,
            object request,
            Type responseType)
        {
            var url = (_baseUrl + relativeUrl).ReplaceAll(parameters.EmptyWhenNull(), _defaultValues);

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Timeout = _timeout * 1000;
            httpRequest.ReadWriteTimeout = _timeout * 1000;
            httpRequest.Method = verb.ToString().ToUpper();

            if (_token != null) httpRequest.SetTokenAuthenticationHeader(_token);
            else if (_username != null && _password != null) httpRequest.SetBasicAuthCredentials(_username, _password);
            else throw new Exception("No credentials specified.");
            
            httpRequest.UserAgent = "Reachmail .NET API Wrapper v" + Assembly.GetExecutingAssembly().GetName().Version;

            if (verb == Verb.Post || verb == Verb.Put)
                httpRequest.ContentType = request is Stream ? BinaryContentType : JsonContentType;
            httpRequest.Accept = responseType == typeof(Stream) ? BinaryContentType : JsonContentType;

            if (_proxy != null) httpRequest.Proxy = _proxy;

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
            HttpStatus = response.StatusCode;
            HttpStatusDescription = response.StatusDescription;
            ResponseText = response.GetResponseText();
        }

        public HttpWebResponse Response { get; private set; }
        public HttpStatusCode HttpStatus { get; set; }
        public string HttpStatusDescription { get; set; }
        public string ResponseText { get; set; }
    }
}