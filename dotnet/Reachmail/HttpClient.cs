using System;
using System.Collections.Generic;

namespace ReachmailApi
{
    public enum Verb { Get, Post, Put, Delete }

    public interface IHttpClient
    {
        object Execute(
            string url,
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
            string url,
            Verb verb,
            Dictionary<string, object> urlParameters,
            Dictionary<string, object> querystringParameters,
            object request,
            Type responseType)
        {
            return null;
        }
    }
}