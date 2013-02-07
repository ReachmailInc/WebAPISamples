using System;
using System.IO;
using System.Net;
using System.Collections.Generic;

namespace ReachmailApi
{
    public partial class Reachmail
    {
        public Reachmail(IHttpClient httpClient) 
        {
    {{#rootModules}}
        {{.}} = new {{.}}.ApiModule(httpClient);
    {{/rootModules}}
    }

        public static Reachmail Create(
            string accountKey, 
            string username, 
            string password, 
            string baseUrl = "{{{url}}}", 
            bool allowSelfSignedCerts = false,
            IWebProxy proxy = null,
            int timeout = 30)
        {
            var httpClient = new HttpClient(baseUrl, accountKey + @"\" + username, password, allowSelfSignedCerts, proxy, timeout);
            var reachmail = new Reachmail(httpClient);
            httpClient.AddParameterDefault("accountId", reachmail.Administration.Users.Current.Get().AccountId);
            return reachmail;
        }

    {{#rootModules}}
    public {{.}}.ApiModule {{.}} { get; private set; }
    {{/rootModules}}
}   

    {{#modules}}
namespace {{namespace}}
    {
        public class ApiModule
        {
            private readonly IHttpClient _httpClient;

            public ApiModule(IHttpClient httpClient) 
            {
                _httpClient = httpClient;
    {{#modules}}
            {{.}} = new {{.}}.ApiModule(httpClient);
    {{/modules}}
        }

    {{#modules}}
        public {{.}}.ApiModule {{.}} { get; private set; }
    {{/modules}}
    {{#endpoints}}

            /// <summary>{{{comments}}}</summary> 
            {{#parameterComments}}/// <param name="{{name}}">{{{comments}}}</param>
            {{/parameterComments}}
            {{#responseComments}}/// <returns>{{{.}}}</returns>
            {{/responseComments}}
public {{{returnType}}} {{{method}}}({{{parameters}}}) 
            {
                {{{returnKeyword}}}_httpClient.Execute("{{{url}}}", 
                    Verb.{{{method}}}, 
                    new Dictionary<string, object> { {{{urlArguments}}} }, 
                    new Dictionary<string, object> { {{{querystringArguments}}} },
                    {{{requestArgument}}},
                    {{{responseArgument}}});
            }
        {{/endpoints}}
}
    }   
    {{/modules}}

    {{#types}}
namespace {{{namespace}}}
    {
        {{#comments}}
        /// {{{.}}}
        {{/comments}}
        public class {{{name}}}
        {
        {{#members}}
        /// <summary>{{{comments}}}</summary> 
        public {{{type}}} {{{name}}} { get; set; }
        {{/members}}

        {{#enums}}
        public enum {{{name}}} 
        {
            {{#values}}
            /// <summary>{{{comments}}}</summary> 
            {{{value}}},
            {{/values}}
        }
        {{/enums}}
    }
    }

    {{/types}}
}
