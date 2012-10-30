using System;
using System.Collections.Generic;

namespace ReachmailApi
{
    public class Reachmail
    {
        public Reachmail(string accountKey, string username, string password) 
        {
            var httpClient = new HttpClient(accountKey + @"\" + username, password);
    {{#rootModules}}
        {{.}} = new {{.}}.Module(httpClient);
    {{/rootModules}}
    
            httpClient.AddParameterDefault("accountId", Administration.Users.Current.Get().AccountId);
        }

    {{#rootModules}}
    public {{.}}.Module {{.}} { get; set; }
    {{/rootModules}}
}   

    {{#modules}}
namespace {{namespace}}
    {
        public class Module
        {
            private readonly HttpClient _httpClient;

            internal Module(HttpClient httpClient) 
            {
                _httpClient = httpClient;
    {{#modules}}
            {{.}} = new {{.}}.Module(httpClient);
    {{/modules}}
        }

    {{#modules}}
        public {{.}}.Module {{.}} { get; set; }
    {{/modules}}
    {{#endpoints}}

            /// <summary>{{Comments}}</summary> 
            {{#UrlParameters}}/// <param name="{{Name}}">{{Comments}}</param>
            {{/UrlParameters}}
{{#Request}}/// <param name="request">{{Comments}}</param>
{{/Request}}
            {{#QuerystringParameters}}/// <param name="{{Name}}">{{Comments}}</param>
            {{/QuerystringParameters}}
public {{returnType}} {{Method}}({{parameters}}) 
            {
                {{returnKeyword}}_httpClient.Execute("{{Url}}", 
                    HttpClient.Verb.{{Method}}, 
                    new Dictionary<string, object> { {{{urlArguments}}} }, 
                    new Dictionary<string, object> { {{{querystringArguments}}} });
            }
        {{/endpoints}}
}
    }   
    {{#endpoints}}
    
    namespace {{namespace}}.{{Method}}
    {
        public class Request 
        {
        }

        public class Response 
        {
        }
    }
        {{/endpoints}}

    {{/modules}}

}
