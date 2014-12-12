using System;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace Reachmail
{
    public partial class Api
    {
        private const string Host = "https://{{args 'apiHost'}}";
        private readonly IHttpClient _client;

        public Api(IHttpClient client)
        {
            _client = client;
            _client.AddParameterDefault("accountId", Administration.Users.Current.Get().AccountId);
        }

        public static Api Connect(
            string accountKey, 
            string username, 
            string password, 
            string baseUrl = Host, 
            bool allowSelfSignedCerts = false,
            IWebProxy proxy = null,
            int timeout = 30)
        {
            return Connect(new HttpClient(baseUrl, accountKey + @"\" + username, password, allowSelfSignedCerts, proxy, timeout));
        }

        public static Api Connect(
            string token, 
            string baseUrl = Host, 
            bool allowSelfSignedCerts = false,
            IWebProxy proxy = null,
            int timeout = 30)
        {
            return Connect(new HttpClient(baseUrl, token, allowSelfSignedCerts, proxy, timeout));
        }
        
        private static Api Connect(HttpClient client)
        {
            return new Api(client);
        }
    }
    {{#Modules}}
    {{#pathAsTree Resources 'Name' '/' 1}}
    {{^if parent}}

    public partial class Api
    {
        public {{initialCap name}}.Dsl {{initialCap name}}
        {
            get
            {
                return new {{initialCap name}}.Dsl(_client);
            }
        } 
    }
    {{/if}}

    namespace {{join (initialCap path) "."}}
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            {{#children}}

            public {{initialCap name}}.Dsl {{initialCap name}}
            {
                get
                {
                    return new {{initialCap name}}.Dsl(_client);
                }
            }
            {{/children}} 
            {{#leaf}}
            {{#Endpoints}}
            
            /// <summary>
            {{#split (htmlToText Comments)}}
            /// {{this}}
            {{/split}}
            /// </summary>
            public {{#hasResponse}}{{#if stream}}Stream{{else}}{{endpointName "AccountId"}}.Response.{{Response.Body.Type.Name}}{{/if}}{{else}}void{{/hasResponse}} {{{initialCap Method}}}(
                {{#parameters ", " "accountId"}}
                    {{#if type}}{{simpleClrType type optional false}}{{else}}{{#if stream}}Stream{{else}}{{endpointName "AccountId"}}.Request.{{Request.Body.Type.Name}}{{/if}}{{/if}} {{name}}{{#if optional}} = null{{/if}}
                {{/parameters}})
            {
                {{#hasResponse}}return ({{#if stream}}Stream{{else}}{{endpointName "AccountId"}}.Response.{{Response.Body.Type.Name}}{{/if}}){{/hasResponse}}_client.Execute("{{{Url}}}", 
                    Verb.{{{initialCap Method}}}, 
                    new Dictionary<string, object> { 
                        {{#parameters ", " "request" "accountId"}}
                            { "{{name}}", {{name}} }
                        {{/parameters}} }, 
                    {{#hasRequest}}request{{else}}null{{/hasRequest}},
                    {{#hasResponse}}typeof({{#if stream}}Stream{{else}}{{endpointName "AccountId"}}.Response.{{Response.Body.Type.Name}}{{/if}}){{else}}null{{/hasResponse}});
            }
            {{/Endpoints}}
            {{/leaf}}
        }

        {{#leaf}}
        {{#Endpoints}}
        namespace {{endpointName "AccountId"}}
        {
            {{#if Request.Body.Type}}
            namespace Request
            {
                {{#visitTree Request.Body.Type "Members.Type" "ArrayItem.Type" "DictionaryEntry.KeyType" "DictionaryEntry.ValueType"}}
                {{#if IsComplex}}
                /// <summary>
                {{#split (htmlToText Comments)}}
                /// {{this}}
                {{/split}}
                /// </summary>
                public class {{join ShortNamespace ''}}{{Name}}
                {
                    {{#Members}}
                    /// <summary>
                    {{#split (htmlToText Comments)}}
                    /// {{this}}
                    {{/split}}
                    /// </summary>
                    public {{join Type.ShortNamespace ''}}{{#if Type.IsArray}}{{pluralize Type.Name}}{{else}}{{#if Type.IsSimple}}{{#if Type.Options}}{{simpleClrType Type.Options.Name Optional true}}{{else}}{{simpleClrType Type.Name Optional false}}{{/if}}{{else}}{{Type.Name}}{{/if}}{{/if}} {{Name}} { get; set; }
                    {{/Members}}
                }

                {{/if}}
                {{#if IsArray}}
                public class {{join ShortNamespace ''}}{{pluralize Name}} : 
                    List<{{join ArrayItem.Type.ShortNamespace ''}}{{simpleClrType ArrayItem.Type.Name false false}}> { }

                {{/if}}
                {{#if IsDictionary}}
                public class {{join ShortNamespace ''}}{{pluralize Name}} : Dictionary<
                        {{join DictionaryEntry.KeyType.ShortNamespace ''}}{{simpleClrType DictionaryEntry.KeyType.Name false false}},
                        {{join DictionaryEntry.ValueType.ShortNamespace ''}}{{simpleClrType DictionaryEntry.ValueType.Name false false}}> { }

                {{/if}}
                {{#if IsSimple}}
                {{#Options}}
                /// <summary>
                {{#split (htmlToText Comments)}}
                /// {{this}}
                {{/split}}
                /// </summary>
                public enum {{Name}} 
                {
                    {{#Options}}
                    /// <summary>
                    {{#split (htmlToText Comments)}}
                    /// {{this}}
                    {{/split}}
                    /// </summary>
                    {{Value}},
                    {{/Options}}
                };

                {{/Options}}
                {{/if}}
                {{/visitTree}}
            }
            {{/if}}

            {{#if Response.Body.Type}}
            namespace Response
            {
                {{#visitTree Response.Body.Type "Members.Type" "ArrayItem.Type" "DictionaryEntry.KeyType" "DictionaryEntry.ValueType"}}
                {{#if IsComplex}}
                /// <summary>
                {{#split (htmlToText Comments)}}
                /// {{this}}
                {{/split}}
                /// </summary>
                public class {{join ShortNamespace ''}}{{Name}}
                {
                    {{#Members}}
                    /// <summary>
                    {{#split (htmlToText Comments)}}
                    /// {{this}}
                    {{/split}}
                    /// </summary>
                    public {{join Type.ShortNamespace ''}}{{#if Type.IsArray}}{{pluralize Type.Name}}{{else}}{{#if Type.IsSimple}}{{#if Type.Options}}{{simpleClrType Type.Options.Name Optional true}}{{else}}{{simpleClrType Type.Name Optional false}}{{/if}}{{else}}{{Type.Name}}{{/if}}{{/if}} {{Name}} { get; set; }
                    {{/Members}}
                }

                {{/if}}
                {{#if IsArray}}
                public class {{join ShortNamespace ''}}{{pluralize Name}} : 
                    List<{{join ArrayItem.Type.ShortNamespace ''}}{{simpleClrType ArrayItem.Type.Name false false}}> { }

                {{/if}}
                {{#if IsDictionary}}
                public class {{join ShortNamespace ''}}{{pluralize Name}} : Dictionary<
                        {{join DictionaryEntry.KeyType.ShortNamespace ''}}{{simpleClrType DictionaryEntry.KeyType.Name false false}},
                        {{join DictionaryEntry.ValueType.ShortNamespace ''}}{{simpleClrType DictionaryEntry.ValueType.Name false false}}> { }

                {{/if}}
                {{#if IsSimple}}
                {{#Options}}
                /// <summary>
                {{#split (htmlToText Comments)}}
                /// {{this}}
                {{/split}}
                /// </summary>
                public enum {{Name}} 
                {
                    {{#Options}}
                    /// <summary>
                    {{#split (htmlToText Comments)}}
                    /// {{this}}
                    {{/split}}
                    /// </summary>
                    {{Value}},
                    {{/Options}}
                };

                {{/Options}}
                {{/if}}
                {{/visitTree}}
            }
            {{/if}}
        }

        {{/Endpoints}}
        {{/leaf}}
    }
    {{/pathAsTree}}
    {{/Modules}}
}