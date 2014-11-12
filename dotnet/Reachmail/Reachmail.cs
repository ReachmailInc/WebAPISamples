using System;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace Reachmail
{
    public partial class Api
    {
        private const string Host = "https://services.ci.reachmail.net";
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

    public partial class Api
    {
        public Administration.Dsl Administration
        {
            get
            {
                return new Administration.Dsl(_client);
            }
        } 
    }

    namespace Administration
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Addresses.Dsl Addresses
            {
                get
                {
                    return new Addresses.Dsl(_client);
                }
            }

            public Users.Dsl Users
            {
                get
                {
                    return new Users.Dsl(_client);
                }
            }
        }

    }

    namespace Administration.Addresses
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Get account addresses.
            /// </summary>
            public Get.Response.Addresses Get(
)
            {
                return (Get.Response.Addresses)_client.Execute("/administration/addresses/{AccountId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
 }, 
                    null,
                    typeof(Get.Response.Addresses));
            }
        }

        namespace Get
        {

            namespace Response
            {
                public class Addresses : 
                    List<Address> { }

                /// <summary>
                /// 
                /// </summary>
                public class Address
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public AddressType Type { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Attention { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Line1 { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Line2 { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Line3 { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String City { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String State { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ZipCode { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Country { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum AddressType 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Billing,
                    /// <summary>
                    /// 
                    /// </summary>
                    CanSpam,
                    /// <summary>
                    /// 
                    /// </summary>
                    Mailing,
                };

            }
        }

    }

    namespace Administration.Users
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Current.Dsl Current
            {
                get
                {
                    return new Current.Dsl(_client);
                }
            }
        }

    }

    namespace Administration.Users.Current
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Get the currently logged in user.
            /// </summary>
            public Get.Response.User Get(
)
            {
                return (Get.Response.User)_client.Execute("/administration/users/current", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
 }, 
                    null,
                    typeof(Get.Response.User));
            }
        }

        namespace Get
        {

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class User
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid AccountId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String AccountKey { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Username { get; set; }
                }

            }
        }

    }

    public partial class Api
    {
        public Campaigns.Dsl Campaigns
        {
            get
            {
                return new Campaigns.Dsl(_client);
            }
        } 
    }

    namespace Campaigns
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Messagetesting.Dsl Messagetesting
            {
                get
                {
                    return new Messagetesting.Dsl(_client);
                }
            }
            
            /// <summary>
            /// Campaign management.
            /// </summary>
            public Post.Response.Queue Post(
                    Post.Request.QueueParameters request
                )
            {
                return (Post.Response.Queue)_client.Execute("/campaigns/{AccountId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Queue));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class QueueParameters
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public ListIds ListIds { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MailingId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Properties Properties { get; set; }
                }

                public class ListIds : 
                    List<Guid> { }

                /// <summary>
                /// 
                /// </summary>
                public class Properties
                {
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? DeliveryTime { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? IsTest { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SuppressionLists SuppressionLists { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public TestFormat? TestFormat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Filter Filter { get; set; }
                }

                public class SuppressionLists : 
                    List<Guid> { }

                /// <summary>
                /// 
                /// </summary>
                public enum TestFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    HtmlOnly,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                /// <summary>
                /// 
                /// </summary>
                public class Filter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Conditions Conditions { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Operator Operator { get; set; }
                }

                public class Conditions : 
                    List<Condition> { }

                /// <summary>
                /// 
                /// </summary>
                public class Condition
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Comparison Comparison { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Property { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Comparison 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Contains,
                    /// <summary>
                    /// 
                    /// </summary>
                    EndsWith,
                    /// <summary>
                    /// 
                    /// </summary>
                    Equals,
                    /// <summary>
                    /// 
                    /// </summary>
                    GreaterThan,
                    /// <summary>
                    /// 
                    /// </summary>
                    GreaterThanOrEqualTo,
                    /// <summary>
                    /// 
                    /// </summary>
                    IsNull,
                    /// <summary>
                    /// 
                    /// </summary>
                    LessThan,
                    /// <summary>
                    /// 
                    /// </summary>
                    LessThanOrEqualTo,
                    /// <summary>
                    /// 
                    /// </summary>
                    NotEqual,
                    /// <summary>
                    /// 
                    /// </summary>
                    StartsWith,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum Operator 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    And,
                    /// <summary>
                    /// 
                    /// </summary>
                    Or,
                };

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Queue
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                }

            }
        }

    }

    namespace Campaigns.Messagetesting
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Message test all account messages.
            /// </summary>
            public Post.Response.Queue Post(
                    Post.Request.QueueParameters request
                )
            {
                return (Post.Response.Queue)_client.Execute("/campaigns/messagetesting/{AccountId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Queue));
            }
            
            /// <summary>
            /// Message test a particular message.
            /// </summary>
            public void Post(
                    Guid campaignId
                ,                     PostByCampaignId.Request.QueueRemainderParameters request
                )
            {
                _client.Execute("/campaigns/messagetesting/{AccountId}/{CampaignId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "campaignId", campaignId }
                         }, 
                    request,
                    null);
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class QueueParameters
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public ListIds ListIds { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public MailingIds MailingIds { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal MessageTestingPercentage { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Properties Properties { get; set; }
                }

                public class ListIds : 
                    List<Guid> { }

                public class MailingIds : 
                    List<Guid> { }

                /// <summary>
                /// 
                /// </summary>
                public class Properties
                {
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? DeliveryTime { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? IsTest { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SuppressionLists SuppressionLists { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public TestFormat? TestFormat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Filter Filter { get; set; }
                }

                public class SuppressionLists : 
                    List<Guid> { }

                /// <summary>
                /// 
                /// </summary>
                public enum TestFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    HtmlOnly,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                /// <summary>
                /// 
                /// </summary>
                public class Filter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Conditions Conditions { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Operator Operator { get; set; }
                }

                public class Conditions : 
                    List<Condition> { }

                /// <summary>
                /// 
                /// </summary>
                public class Condition
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Comparison Comparison { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Property { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Comparison 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Contains,
                    /// <summary>
                    /// 
                    /// </summary>
                    EndsWith,
                    /// <summary>
                    /// 
                    /// </summary>
                    Equals,
                    /// <summary>
                    /// 
                    /// </summary>
                    GreaterThan,
                    /// <summary>
                    /// 
                    /// </summary>
                    GreaterThanOrEqualTo,
                    /// <summary>
                    /// 
                    /// </summary>
                    IsNull,
                    /// <summary>
                    /// 
                    /// </summary>
                    LessThan,
                    /// <summary>
                    /// 
                    /// </summary>
                    LessThanOrEqualTo,
                    /// <summary>
                    /// 
                    /// </summary>
                    NotEqual,
                    /// <summary>
                    /// 
                    /// </summary>
                    StartsWith,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum Operator 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    And,
                    /// <summary>
                    /// 
                    /// </summary>
                    Or,
                };

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Queue
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                }

            }
        }

        namespace PostByCampaignId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class QueueRemainderParameters
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MessageId { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? DeliveryTime { get; set; }
                }

            }

        }

    }

    public partial class Api
    {
        public Data.Dsl Data
        {
            get
            {
                return new Data.Dsl(_client);
            }
        } 
    }

    namespace Data
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Upload data.
            /// </summary>
            public Post.Response.Data Post(
                    Stream request
                )
            {
                return (Post.Response.Data)_client.Execute("/data", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Stream));
            }
            
            /// <summary>
            /// Download data.
            /// </summary>
            public Stream Get(
                    Guid dataId
                )
            {
                return (Stream)_client.Execute("/data/{DataId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "dataId", dataId }
                         }, 
                    null,
                    typeof(Stream));
            }
            
            /// <summary>
            /// Download data as a file.
            /// </summary>
            public Stream Get(
                    Guid dataId
                ,                     String filename
                )
            {
                return (Stream)_client.Execute("/data/{DataId}/{Filename}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "dataId", dataId }
                        ,                             { "filename", filename }
                         }, 
                    null,
                    typeof(Stream));
            }
        }

        namespace Post
        {

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Data
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                }

            }
        }

        namespace GetByDataId
        {

        }

        namespace GetByDataIdAndFilename
        {

        }

    }

    public partial class Api
    {
        public Easysmtp.Dsl Easysmtp
        {
            get
            {
                return new Easysmtp.Dsl(_client);
            }
        } 
    }

    namespace Easysmtp
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// EasySMTP delivery.
            /// </summary>
            public Post.Response.DeliveryResponse Post(
                    Post.Request.DeliveryRequest request
                )
            {
                return (Post.Response.DeliveryResponse)_client.Execute("/easysmtp/{AccountId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.DeliveryResponse));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class DeliveryRequest
                {
                    /// <summary>
                    /// Message from address.
                    /// </summary>
                    public String FromAddress { get; set; }
                    /// <summary>
                    /// Message recipients.
                    /// </summary>
                    public Recipients Recipients { get; set; }
                    /// <summary>
                    /// Email subject.
                    /// </summary>
                    public String Subject { get; set; }
                    /// <summary>
                    /// Plain text message body.
                    /// </summary>
                    public String BodyText { get; set; }
                    /// <summary>
                    /// HTML message body.
                    /// </summary>
                    public String BodyHtml { get; set; }
                    /// <summary>
                    /// SMTP headers.
                    /// </summary>
                    public Headers Headers { get; set; }
                    /// <summary>
                    /// Message attachments.
                    /// </summary>
                    public Attachments Attachments { get; set; }
                    /// <summary>
                    /// Enable message tracking.
                    /// </summary>
                    public Boolean? Tracking { get; set; }
                    /// <summary>
                    /// Mailing address of the sender.
                    /// </summary>
                    public String FooterAddress { get; set; }
                    /// <summary>
                    /// DKIM signature domain.
                    /// </summary>
                    public String SignatureDomain { get; set; }
                }

                public class Recipients : 
                    List<Recipient> { }

                /// <summary>
                /// 
                /// </summary>
                public class Recipient
                {
                    /// <summary>
                    /// Recipient email address.
                    /// </summary>
                    public String Address { get; set; }
                }

                public class Headers : Dictionary<
                        String,
                        String> { }

                public class Attachments : 
                    List<EmailAttachment> { }

                /// <summary>
                /// 
                /// </summary>
                public class EmailAttachment
                {
                    /// <summary>
                    /// Attachment filename.
                    /// </summary>
                    public String Filename { get; set; }
                    /// <summary>
                    /// Base64 encoded file data.
                    /// </summary>
                    public String Data { get; set; }
                    /// <summary>
                    /// Attachment MIME type as described here [http://www.iana.org/assignments/media-types/media-types.xhtml] .
                    /// </summary>
                    public String ContentType { get; set; }
                    /// <summary>
                    /// Content disposition as described here [http://www.iana.org/assignments/cont-disp/cont-disp.xhtml] .
                    /// </summary>
                    public String ContentDisposition { get; set; }
                    /// <summary>
                    /// MIME content id as described here [http://tools.ietf.org/html/rfc2392] .
                    /// </summary>
                    public String Cid { get; set; }
                }

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class DeliveryResponse
                {
                    /// <summary>
                    /// Id of the message.
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// Indicates if delivery failed for one or more recipients.
                    /// </summary>
                    public Boolean Failures { get; set; }
                    /// <summary>
                    /// List of delivery failures.
                    /// </summary>
                    public FailedAddresses FailedAddresses { get; set; }
                    /// <summary>
                    /// Current volume as of this mailing.
                    /// </summary>
                    public Int32 CurrentVolume { get; set; }
                }

                public class FailedAddresses : 
                    List<FailedAddress> { }

                /// <summary>
                /// 
                /// </summary>
                public class FailedAddress
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public DeliveryFailure Reason { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum DeliveryFailure 
                {
                    /// <summary>
                    /// Addresses blocked by global opt-out list, pattern opt-out list, or account
                    /// opt-out list.
                    /// </summary>
                    AddressBlocked,
                    /// <summary>
                    /// Known hard bounces
                    /// </summary>
                    AddressInvalid,
                    /// <summary>
                    /// Poorly formatted addresses, e.g. &#x27;@whatever.&#x27;
                    /// </summary>
                    AddressSyntaxInvalid,
                };

            }
        }

    }

    public partial class Api
    {
        public Lists.Dsl Lists
        {
            get
            {
                return new Lists.Dsl(_client);
            }
        } 
    }

    namespace Lists
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Export.Dsl Export
            {
                get
                {
                    return new Export.Dsl(_client);
                }
            }

            public Fields.Dsl Fields
            {
                get
                {
                    return new Fields.Dsl(_client);
                }
            }

            public Filtered.Dsl Filtered
            {
                get
                {
                    return new Filtered.Dsl(_client);
                }
            }

            public Groups.Dsl Groups
            {
                get
                {
                    return new Groups.Dsl(_client);
                }
            }

            public Import.Dsl Import
            {
                get
                {
                    return new Import.Dsl(_client);
                }
            }

            public Optout.Dsl Optout
            {
                get
                {
                    return new Optout.Dsl(_client);
                }
            }

            public Recipients.Dsl Recipients
            {
                get
                {
                    return new Recipients.Dsl(_client);
                }
            }

            public Subscriptionform.Dsl Subscriptionform
            {
                get
                {
                    return new Subscriptionform.Dsl(_client);
                }
            }
            
            /// <summary>
            /// Create a list.
            /// </summary>
            public Post.Response.List Post(
                    Post.Request.ListProperties request
                )
            {
                return (Post.Response.List)_client.Execute("/lists/{AccountId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.List));
            }
            
            /// <summary>
            /// Get a list.
            /// </summary>
            public GetByListId.Response.List Get(
                    Guid listId
                )
            {
                return (GetByListId.Response.List)_client.Execute("/lists/{AccountId}/{ListId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                         }, 
                    null,
                    typeof(GetByListId.Response.List));
            }
            
            /// <summary>
            /// Modify a list.
            /// </summary>
            public void Put(
                    Guid listId
                ,                     PutByListId.Request.ListProperties request
                )
            {
                _client.Execute("/lists/{AccountId}/{ListId}", 
                    Verb.Put, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                         }, 
                    request,
                    null);
            }
            
            /// <summary>
            /// Delete a list.
            /// </summary>
            public void Delete(
                    Guid listId
                )
            {
                _client.Execute("/lists/{AccountId}/{ListId}", 
                    Verb.Delete, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                         }, 
                    null,
                    null);
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class ListProperties
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public ListType? Type { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? GroupId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Fields Fields { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum ListType 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    GlobalOptOut,
                    /// <summary>
                    /// 
                    /// </summary>
                    Md5Suppression,
                    /// <summary>
                    /// 
                    /// </summary>
                    Recipient,
                    /// <summary>
                    /// 
                    /// </summary>
                    Suppression,
                };

                public class Fields : 
                    List<String> { }

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class List
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                }

            }
        }

        namespace GetByListId
        {

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class List
                {
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime CreateDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid GroupId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ContentType { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalActiveRecipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalInactiveRecipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalRecipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public ListType Type { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Fields Fields { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum ListType 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    GlobalOptOut,
                    /// <summary>
                    /// 
                    /// </summary>
                    Md5Suppression,
                    /// <summary>
                    /// 
                    /// </summary>
                    Recipient,
                    /// <summary>
                    /// 
                    /// </summary>
                    Suppression,
                };

                public class Fields : 
                    List<Field> { }

                /// <summary>
                /// 
                /// </summary>
                public class Field
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Description { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Length { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Type { get; set; }
                }

            }
        }

        namespace PutByListId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class ListProperties
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? GroupId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Fields Fields { get; set; }
                }

                public class Fields : 
                    List<String> { }

            }

        }

        namespace DeleteByListId
        {

        }

    }

    namespace Lists.Export
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Status.Dsl Status
            {
                get
                {
                    return new Status.Dsl(_client);
                }
            }
            
            /// <summary>
            /// Export a list.
            /// </summary>
            public PostByListId.Response.Export Post(
                    Guid listId
                ,                     PostByListId.Request.ExportParameters request
                )
            {
                return (PostByListId.Response.Export)_client.Execute("/lists/export/{AccountId}/{ListId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                         }, 
                    request,
                    typeof(PostByListId.Response.Export));
            }
        }

        namespace PostByListId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class ExportParameters
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public ExportOptions ExportOptions { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class ExportOptions
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public ExportDataFormat Format { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? HeaderRow { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public CharacterSeperatedData CharacterSeperatedData { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public ExportOptionsFieldMappings FieldMapping { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum ExportDataFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    CharacterSeperated,
                };

                /// <summary>
                /// 
                /// </summary>
                public class CharacterSeperatedData
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Delimiter Delimiter { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Delimiter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Comma,
                    /// <summary>
                    /// 
                    /// </summary>
                    Tab,
                };

                public class ExportOptionsFieldMappings : 
                    List<FieldMapping> { }

                /// <summary>
                /// 
                /// </summary>
                public class FieldMapping
                {
                    /// <summary>
                    /// Name of the source list field. These can be enumerated
                    /// [here](#Lists@/lists/fields).
                    /// </summary>
                    public String SourceFieldName { get; set; }
                    /// <summary>
                    /// Name of the destination column if the file has a header. If a destination column
                    /// name is not specified a column index must be.
                    /// </summary>
                    public String DestinationFieldName { get; set; }
                }

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Export
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                }

            }
        }

    }

    namespace Lists.Export.Status
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Get the status of a list export.
            /// </summary>
            public GetByExportId.Response.ExportStatus Get(
                    Guid exportId
                )
            {
                return (GetByExportId.Response.ExportStatus)_client.Execute("/lists/export/status/{AccountId}/{ExportId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "exportId", exportId }
                         }, 
                    null,
                    typeof(GetByExportId.Response.ExportStatus));
            }
        }

        namespace GetByExportId
        {

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class ExportStatus
                {
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan Duration { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Complete { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 RecordsProcessed { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid DataId { get; set; }
                }

            }
        }

    }

    namespace Lists.Fields
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Enumerate list fields.
            /// </summary>
            public Get.Response.Fields Get(
)
            {
                return (Get.Response.Fields)_client.Execute("/lists/fields/{AccountId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
 }, 
                    null,
                    typeof(Get.Response.Fields));
            }
        }

        namespace Get
        {

            namespace Response
            {
                public class Fields : 
                    List<Field> { }

                /// <summary>
                /// 
                /// </summary>
                public class Field
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Description { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Length { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Type { get; set; }
                }

            }
        }

    }

    namespace Lists.Filtered
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Query lists.
            /// </summary>
            public Post.Response.Lists Post(
                    Post.Request.ListFilter request
                )
            {
                return (Post.Response.Lists)_client.Execute("/lists/filtered/{AccountId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Lists));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class ListFilter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? GroupId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? IncludeSubGroups { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? ActiveLargerThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? ActiveSmallerThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? InactiveLargerThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? InactiveSmallerThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? LargerThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? SmallerThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? StartIndex { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? MaxResults { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OlderThan { get; set; }
                }

            }

            namespace Response
            {
                public class Lists : 
                    List<List> { }

                /// <summary>
                /// 
                /// </summary>
                public class List
                {
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime CreateDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid GroupId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ContentType { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalActiveRecipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalInactiveRecipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalRecipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public ListType Type { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Fields Fields { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum ListType 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    GlobalOptOut,
                    /// <summary>
                    /// 
                    /// </summary>
                    Md5Suppression,
                    /// <summary>
                    /// 
                    /// </summary>
                    Recipient,
                    /// <summary>
                    /// 
                    /// </summary>
                    Suppression,
                };

                public class Fields : 
                    List<Field> { }

                /// <summary>
                /// 
                /// </summary>
                public class Field
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Description { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Length { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Type { get; set; }
                }

            }
        }

    }

    namespace Lists.Groups
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Enumerate list groups.
            /// </summary>
            public Get.Response.Groups Get(
)
            {
                return (Get.Response.Groups)_client.Execute("/lists/groups/{AccountId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
 }, 
                    null,
                    typeof(Get.Response.Groups));
            }
            
            /// <summary>
            /// Create a list group.
            /// </summary>
            public Post.Response.Group Post(
                    Post.Request.GroupProperties request
                )
            {
                return (Post.Response.Group)_client.Execute("/lists/groups/{AccountId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Group));
            }
            
            /// <summary>
            /// Get a list group.
            /// </summary>
            public GetByGroupId.Response.Group Get(
                    Guid groupId
                )
            {
                return (GetByGroupId.Response.Group)_client.Execute("/lists/groups/{AccountId}/{GroupId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "groupId", groupId }
                         }, 
                    null,
                    typeof(GetByGroupId.Response.Group));
            }
            
            /// <summary>
            /// Modify a list group.
            /// </summary>
            public void Put(
                    Guid groupId
                ,                     PutByGroupId.Request.GroupProperties request
                )
            {
                _client.Execute("/lists/groups/{AccountId}/{GroupId}", 
                    Verb.Put, 
                    new Dictionary<string, object> { 
                            { "groupId", groupId }
                         }, 
                    request,
                    null);
            }
            
            /// <summary>
            /// Delete a list group.
            /// </summary>
            public void Delete(
                    Guid groupId
                )
            {
                _client.Execute("/lists/groups/{AccountId}/{GroupId}", 
                    Verb.Delete, 
                    new Dictionary<string, object> { 
                            { "groupId", groupId }
                         }, 
                    null,
                    null);
            }
        }

        namespace Get
        {

            namespace Response
            {
                public class Groups : 
                    List<Group> { }

                /// <summary>
                /// 
                /// </summary>
                public class Group
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Modified { get; set; }
                }

            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class GroupProperties
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                }

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Group
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                }

            }
        }

        namespace GetByGroupId
        {

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Group
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Modified { get; set; }
                }

            }
        }

        namespace PutByGroupId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class GroupProperties
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                }

            }

        }

        namespace DeleteByGroupId
        {

        }

    }

    namespace Lists.Import
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Status.Dsl Status
            {
                get
                {
                    return new Status.Dsl(_client);
                }
            }
            
            /// <summary>
            /// Import a list.
            /// </summary>
            public PostByListId.Response.Import Post(
                    Guid listId
                ,                     PostByListId.Request.Parameters request
                )
            {
                return (PostByListId.Response.Import)_client.Execute("/lists/import/{AccountId}/{ListId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                         }, 
                    request,
                    typeof(PostByListId.Response.Import));
            }
        }

        namespace PostByListId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class Parameters
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid DataId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public FieldMappings FieldMappings { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public ImportOptions ImportOptions { get; set; }
                }

                public class FieldMappings : 
                    List<FieldMapping> { }

                /// <summary>
                /// 
                /// </summary>
                public class FieldMapping
                {
                    /// <summary>
                    /// Name of the destination list field. These can be enumerated
                    /// [here](#Lists@/lists/fields).
                    /// </summary>
                    public String DestinationFieldName { get; set; }
                    /// <summary>
                    /// 1-based index of the source column. If a source column name is not specified an
                    /// index must be.
                    /// </summary>
                    public Int32? SourceFieldPosition { get; set; }
                    /// <summary>
                    /// Name of the source column if the file has a header. If a source column name is
                    /// not specified a column index must be.
                    /// </summary>
                    public String SourceFieldName { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class ImportOptions
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public ImportDataFormat Format { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? HeaderRow { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? SkipRecordsWithErrors { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? AllowStringTruncation { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? AbortImportOnError { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public CharacterSeperatedOptions CharacterSeperatedOptions { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public ExcelOptions ExcelOptions { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum ImportDataFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    CharacterSeperated,
                    /// <summary>
                    /// 
                    /// </summary>
                    Excel,
                };

                /// <summary>
                /// 
                /// </summary>
                public class CharacterSeperatedOptions
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Delimiter Delimiter { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Delimiter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Comma,
                    /// <summary>
                    /// 
                    /// </summary>
                    Tab,
                };

                /// <summary>
                /// 
                /// </summary>
                public class ExcelOptions
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String WorksheetName { get; set; }
                }

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Import
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                }

            }
        }

    }

    namespace Lists.Import.Status
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Check the status of a list import.
            /// </summary>
            public GetByImportId.Response.ImportStatus Get(
                    Guid importId
                )
            {
                return (GetByImportId.Response.ImportStatus)_client.Execute("/lists/import/status/{AccountId}/{ImportId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "importId", importId }
                         }, 
                    null,
                    typeof(GetByImportId.Response.ImportStatus));
            }
        }

        namespace GetByImportId
        {

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class ImportStatus
                {
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan Duration { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Complete { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 RecordsProcessed { get; set; }
                }

            }
        }

    }

    namespace Lists.Optout
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Opt out of a list.
            /// </summary>
            public void Post(
                    Guid listId
                ,                     PostByListId.Request.Recipient request
                )
            {
                _client.Execute("/lists/optout/{AccountId}/{ListId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                         }, 
                    request,
                    null);
            }
        }

        namespace PostByListId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class Recipient
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                }

            }

        }

    }

    namespace Lists.Recipients
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Filtered.Dsl Filtered
            {
                get
                {
                    return new Filtered.Dsl(_client);
                }
            }

            public Subscribe.Dsl Subscribe
            {
                get
                {
                    return new Subscribe.Dsl(_client);
                }
            }
            
            /// <summary>
            /// Create a recipient.
            /// </summary>
            public void Post(
                    Guid listId
                ,                     PostByListId.Request.RecipientProperties request
                )
            {
                _client.Execute("/lists/recipients/{AccountId}/{ListId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                         }, 
                    request,
                    null);
            }
            
            /// <summary>
            /// Get a recipient.
            /// </summary>
            public GetByListIdAndEmail.Response.Recipient Get(
                    Guid listId
                ,                     String email
                )
            {
                return (GetByListIdAndEmail.Response.Recipient)_client.Execute("/lists/recipients/{AccountId}/{ListId}/{Email}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                        ,                             { "email", email }
                         }, 
                    null,
                    typeof(GetByListIdAndEmail.Response.Recipient));
            }
            
            /// <summary>
            /// Delete a recipient.
            /// </summary>
            public void Delete(
                    Guid listId
                ,                     String email
                )
            {
                _client.Execute("/lists/recipients/{AccountId}/{ListId}/{Email}", 
                    Verb.Delete, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                        ,                             { "email", email }
                         }, 
                    null,
                    null);
            }
            
            /// <summary>
            /// Modify a recipient.
            /// </summary>
            public void Put(
                    Guid listId
                ,                     String lookupEmail
                ,                     PutByListIdAndLookupEmail.Request.RecipientProperties request
                )
            {
                _client.Execute("/lists/recipients/{AccountId}/{ListId}/{LookupEmail}", 
                    Verb.Put, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                        ,                             { "lookupEmail", lookupEmail }
                         }, 
                    request,
                    null);
            }
        }

        namespace PostByListId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class RecipientProperties
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreference? EmailFormatPreference { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? OptOut { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Properties Properties { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreference 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class Properties : 
                    List<Property> { }

                /// <summary>
                /// 
                /// </summary>
                public class Property
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

            }

        }

        namespace GetByListIdAndEmail
        {

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Recipient
                {
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime BounceDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public BounceSeverity BounceStatus { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime CreateDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreference EmailFormatPreference { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean OptOut { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime OptOutDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Properties Properties { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum BounceSeverity 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Hard,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Soft,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreference 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class Properties : 
                    List<Property> { }

                /// <summary>
                /// 
                /// </summary>
                public class Property
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

            }
        }

        namespace DeleteByListIdAndEmail
        {

        }

        namespace PutByListIdAndLookupEmail
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class RecipientProperties
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreference? EmailFormatPreference { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? OptOut { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Properties Properties { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreference 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class Properties : 
                    List<Property> { }

                /// <summary>
                /// 
                /// </summary>
                public class Property
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

            }

        }

    }

    namespace Lists.Recipients.Filtered
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Delete.Dsl Delete
            {
                get
                {
                    return new Delete.Dsl(_client);
                }
            }

            public Modify.Dsl Modify
            {
                get
                {
                    return new Modify.Dsl(_client);
                }
            }
            
            /// <summary>
            /// Query recipients.
            /// </summary>
            public PostByListId.Response.Recipients Post(
                    Guid listId
                ,                     PostByListId.Request.RecipientFilter request
                )
            {
                return (PostByListId.Response.Recipients)_client.Execute("/lists/recipients/filtered/{AccountId}/{ListId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                         }, 
                    request,
                    typeof(PostByListId.Response.Recipients));
            }
        }

        namespace PostByListId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class RecipientFilter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? StartIndex { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? MaxResults { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public BounceFilter? BounceFilter { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? BounceDateNewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? BouncesDateOlderThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreferenceFilter? EmailFormatPreferenceFilter { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Expression Expression { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OlderThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OptOutDateNewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OptOutDateOlderThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public OptOutFilter? OptOutFilter { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum BounceFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Hard,
                    /// <summary>
                    /// 
                    /// </summary>
                    HardAndSoft,
                    /// <summary>
                    /// 
                    /// </summary>
                    Non,
                    /// <summary>
                    /// 
                    /// </summary>
                    NonAndSoft,
                    /// <summary>
                    /// 
                    /// </summary>
                    Soft,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreferenceFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    NoPreference,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                };

                /// <summary>
                /// 
                /// </summary>
                public class Expression
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Conditions Conditions { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Operator Operator { get; set; }
                }

                public class Conditions : 
                    List<Condition> { }

                /// <summary>
                /// 
                /// </summary>
                public class Condition
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Comparison Comparison { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Property { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Comparison 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Contains,
                    /// <summary>
                    /// 
                    /// </summary>
                    EndsWith,
                    /// <summary>
                    /// 
                    /// </summary>
                    Equals,
                    /// <summary>
                    /// 
                    /// </summary>
                    GreaterThan,
                    /// <summary>
                    /// 
                    /// </summary>
                    GreaterThanOrEqualTo,
                    /// <summary>
                    /// 
                    /// </summary>
                    IsNull,
                    /// <summary>
                    /// 
                    /// </summary>
                    LessThan,
                    /// <summary>
                    /// 
                    /// </summary>
                    LessThanOrEqualTo,
                    /// <summary>
                    /// 
                    /// </summary>
                    NotEqual,
                    /// <summary>
                    /// 
                    /// </summary>
                    StartsWith,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum Operator 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    And,
                    /// <summary>
                    /// 
                    /// </summary>
                    Or,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum OptOutFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Non,
                    /// <summary>
                    /// 
                    /// </summary>
                    OptOuts,
                };

            }

            namespace Response
            {
                public class Recipients : 
                    List<Recipient> { }

                /// <summary>
                /// 
                /// </summary>
                public class Recipient
                {
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime BounceDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public BounceSeverity BounceStatus { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime CreateDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreference EmailFormatPreference { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean OptOut { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime OptOutDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Properties Properties { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum BounceSeverity 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Hard,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Soft,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreference 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class Properties : 
                    List<Property> { }

                /// <summary>
                /// 
                /// </summary>
                public class Property
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

            }
        }

    }

    namespace Lists.Recipients.Filtered.Delete
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Delete recipients by query.
            /// </summary>
            public void Post(
                    Guid listId
                ,                     PostByListId.Request.RecipientFilter request
                )
            {
                _client.Execute("/lists/recipients/filtered/delete/{AccountId}/{ListId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                         }, 
                    request,
                    null);
            }
        }

        namespace PostByListId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class RecipientFilter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? MaxResults { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public BounceFilter? BounceFilter { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? BounceDateNewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? BouncesDateOlderThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreferenceFilter? EmailFormatPreferenceFilter { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Expression Expression { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OlderThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OptOutDateNewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OptOutDateOlderThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public OptOutFilter? OptOutFilter { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum BounceFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Hard,
                    /// <summary>
                    /// 
                    /// </summary>
                    HardAndSoft,
                    /// <summary>
                    /// 
                    /// </summary>
                    Non,
                    /// <summary>
                    /// 
                    /// </summary>
                    NonAndSoft,
                    /// <summary>
                    /// 
                    /// </summary>
                    Soft,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreferenceFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    NoPreference,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                };

                /// <summary>
                /// 
                /// </summary>
                public class Expression
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Conditions Conditions { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Operator Operator { get; set; }
                }

                public class Conditions : 
                    List<Condition> { }

                /// <summary>
                /// 
                /// </summary>
                public class Condition
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Comparison Comparison { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Property { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Comparison 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Contains,
                    /// <summary>
                    /// 
                    /// </summary>
                    EndsWith,
                    /// <summary>
                    /// 
                    /// </summary>
                    Equals,
                    /// <summary>
                    /// 
                    /// </summary>
                    GreaterThan,
                    /// <summary>
                    /// 
                    /// </summary>
                    GreaterThanOrEqualTo,
                    /// <summary>
                    /// 
                    /// </summary>
                    IsNull,
                    /// <summary>
                    /// 
                    /// </summary>
                    LessThan,
                    /// <summary>
                    /// 
                    /// </summary>
                    LessThanOrEqualTo,
                    /// <summary>
                    /// 
                    /// </summary>
                    NotEqual,
                    /// <summary>
                    /// 
                    /// </summary>
                    StartsWith,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum Operator 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    And,
                    /// <summary>
                    /// 
                    /// </summary>
                    Or,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum OptOutFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Non,
                    /// <summary>
                    /// 
                    /// </summary>
                    OptOuts,
                };

            }

        }

    }

    namespace Lists.Recipients.Filtered.Modify
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Modify recipients by query.
            /// </summary>
            public void Post(
                    Guid listId
                ,                     PostByListId.Request.RecipientParameters request
                )
            {
                _client.Execute("/lists/recipients/filtered/modify/{AccountId}/{ListId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                         }, 
                    request,
                    null);
            }
        }

        namespace PostByListId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class RecipientParameters
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public RecipientParametersProperties Properties { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Filter Filter { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class RecipientParametersProperties
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreference? EmailFormatPreference { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? OptOut { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Properties Properties { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreference 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class Properties : 
                    List<Property> { }

                /// <summary>
                /// 
                /// </summary>
                public class Property
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class Filter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? MaxResults { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public BounceFilter? BounceFilter { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? BounceDateNewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? BouncesDateOlderThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreferenceFilter? EmailFormatPreferenceFilter { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Expression Expression { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OlderThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OptOutDateNewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OptOutDateOlderThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public OptOutFilter? OptOutFilter { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum BounceFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Hard,
                    /// <summary>
                    /// 
                    /// </summary>
                    HardAndSoft,
                    /// <summary>
                    /// 
                    /// </summary>
                    Non,
                    /// <summary>
                    /// 
                    /// </summary>
                    NonAndSoft,
                    /// <summary>
                    /// 
                    /// </summary>
                    Soft,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreferenceFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    NoPreference,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                };

                /// <summary>
                /// 
                /// </summary>
                public class Expression
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Conditions Conditions { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Operator Operator { get; set; }
                }

                public class Conditions : 
                    List<Condition> { }

                /// <summary>
                /// 
                /// </summary>
                public class Condition
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Comparison Comparison { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Property { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Comparison 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Contains,
                    /// <summary>
                    /// 
                    /// </summary>
                    EndsWith,
                    /// <summary>
                    /// 
                    /// </summary>
                    Equals,
                    /// <summary>
                    /// 
                    /// </summary>
                    GreaterThan,
                    /// <summary>
                    /// 
                    /// </summary>
                    GreaterThanOrEqualTo,
                    /// <summary>
                    /// 
                    /// </summary>
                    IsNull,
                    /// <summary>
                    /// 
                    /// </summary>
                    LessThan,
                    /// <summary>
                    /// 
                    /// </summary>
                    LessThanOrEqualTo,
                    /// <summary>
                    /// 
                    /// </summary>
                    NotEqual,
                    /// <summary>
                    /// 
                    /// </summary>
                    StartsWith,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum Operator 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    And,
                    /// <summary>
                    /// 
                    /// </summary>
                    Or,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum OptOutFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Non,
                    /// <summary>
                    /// 
                    /// </summary>
                    OptOuts,
                };

            }

        }

    }

    namespace Lists.Recipients.Subscribe
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Subscribe a recipient.
            /// </summary>
            public void Post(
                    Guid listId
                ,                     PostByListId.Request.RecipientParameters request
                )
            {
                _client.Execute("/lists/recipients/subscribe/{AccountId}/{ListId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "listId", listId }
                         }, 
                    request,
                    null);
            }
        }

        namespace PostByListId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class RecipientParameters
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public RecipientParametersProperties Properties { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Filter Filter { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class RecipientParametersProperties
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreference? EmailFormatPreference { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? OptOut { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Properties Properties { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreference 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class Properties : 
                    List<Property> { }

                /// <summary>
                /// 
                /// </summary>
                public class Property
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class Filter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public BounceFilter? BounceFilter { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? BounceDateNewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? BouncesDateOlderThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreferenceFilter? EmailFormatPreferenceFilter { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Expression Expression { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OlderThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OptOutDateNewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OptOutDateOlderThan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public OptOutFilter? OptOutFilter { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum BounceFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Hard,
                    /// <summary>
                    /// 
                    /// </summary>
                    HardAndSoft,
                    /// <summary>
                    /// 
                    /// </summary>
                    Non,
                    /// <summary>
                    /// 
                    /// </summary>
                    NonAndSoft,
                    /// <summary>
                    /// 
                    /// </summary>
                    Soft,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreferenceFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    NoPreference,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                };

                /// <summary>
                /// 
                /// </summary>
                public class Expression
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Conditions Conditions { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Operator Operator { get; set; }
                }

                public class Conditions : 
                    List<Condition> { }

                /// <summary>
                /// 
                /// </summary>
                public class Condition
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Comparison Comparison { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Property { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Comparison 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Contains,
                    /// <summary>
                    /// 
                    /// </summary>
                    EndsWith,
                    /// <summary>
                    /// 
                    /// </summary>
                    Equals,
                    /// <summary>
                    /// 
                    /// </summary>
                    GreaterThan,
                    /// <summary>
                    /// 
                    /// </summary>
                    GreaterThanOrEqualTo,
                    /// <summary>
                    /// 
                    /// </summary>
                    IsNull,
                    /// <summary>
                    /// 
                    /// </summary>
                    LessThan,
                    /// <summary>
                    /// 
                    /// </summary>
                    LessThanOrEqualTo,
                    /// <summary>
                    /// 
                    /// </summary>
                    NotEqual,
                    /// <summary>
                    /// 
                    /// </summary>
                    StartsWith,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum Operator 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    And,
                    /// <summary>
                    /// 
                    /// </summary>
                    Or,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum OptOutFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Non,
                    /// <summary>
                    /// 
                    /// </summary>
                    OptOuts,
                };

            }

        }

    }

    namespace Lists.Subscriptionform
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Filtered.Dsl Filtered
            {
                get
                {
                    return new Filtered.Dsl(_client);
                }
            }
            
            /// <summary>
            /// Get a subscription form.
            /// </summary>
            public GetBySubscriptionFormId.Response.SubscriptionForm Get(
                    Guid subscriptionFormId
                )
            {
                return (GetBySubscriptionFormId.Response.SubscriptionForm)_client.Execute("/lists/subscriptionform/{AccountId}/{SubscriptionFormId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "subscriptionFormId", subscriptionFormId }
                         }, 
                    null,
                    typeof(GetBySubscriptionFormId.Response.SubscriptionForm));
            }
        }

        namespace GetBySubscriptionFormId
        {

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class SubscriptionForm
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public OptInType OptInType { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Content { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public List List { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SubscriptionFormFields Fields { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ConfirmationUrl { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum OptInType 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Double,
                    /// <summary>
                    /// 
                    /// </summary>
                    Single,
                };

                /// <summary>
                /// 
                /// </summary>
                public class List
                {
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime CreateDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid GroupId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ContentType { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalActiveRecipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalInactiveRecipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalRecipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public ListType Type { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public ListFields Fields { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum ListType 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    GlobalOptOut,
                    /// <summary>
                    /// 
                    /// </summary>
                    Md5Suppression,
                    /// <summary>
                    /// 
                    /// </summary>
                    Recipient,
                    /// <summary>
                    /// 
                    /// </summary>
                    Suppression,
                };

                public class ListFields : 
                    List<ListFieldsField> { }

                /// <summary>
                /// 
                /// </summary>
                public class ListFieldsField
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Description { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Length { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Type { get; set; }
                }

                public class SubscriptionFormFields : 
                    List<SubscriptionFormFieldsField> { }

                /// <summary>
                /// 
                /// </summary>
                public class SubscriptionFormFieldsField
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Required { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FriendlyName { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                }

            }
        }

    }

    namespace Lists.Subscriptionform.Filtered
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Query subscription forms.
            /// </summary>
            public Post.Response.SubscriptionForms Post(
                    Post.Request.SubscriptionFormFilter request
                )
            {
                return (Post.Response.SubscriptionForms)_client.Execute("/lists/subscriptionform/filtered/{AccountId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.SubscriptionForms));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class SubscriptionFormFilter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? StartIndex { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? MaxResults { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OlderThan { get; set; }
                }

            }

            namespace Response
            {
                public class SubscriptionForms : 
                    List<SubscriptionForm> { }

                /// <summary>
                /// 
                /// </summary>
                public class SubscriptionForm
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public OptInType OptInType { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Content { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public List List { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SubscriptionFormFields Fields { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ConfirmationUrl { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum OptInType 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Double,
                    /// <summary>
                    /// 
                    /// </summary>
                    Single,
                };

                /// <summary>
                /// 
                /// </summary>
                public class List
                {
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime CreateDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid GroupId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ContentType { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalActiveRecipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalInactiveRecipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalRecipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public ListType Type { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public ListFields Fields { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum ListType 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    GlobalOptOut,
                    /// <summary>
                    /// 
                    /// </summary>
                    Md5Suppression,
                    /// <summary>
                    /// 
                    /// </summary>
                    Recipient,
                    /// <summary>
                    /// 
                    /// </summary>
                    Suppression,
                };

                public class ListFields : 
                    List<ListFieldsField> { }

                /// <summary>
                /// 
                /// </summary>
                public class ListFieldsField
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Description { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Length { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Type { get; set; }
                }

                public class SubscriptionFormFields : 
                    List<SubscriptionFormFieldsField> { }

                /// <summary>
                /// 
                /// </summary>
                public class SubscriptionFormFieldsField
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Required { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FriendlyName { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                }

            }
        }

    }

    public partial class Api
    {
        public Mailings.Dsl Mailings
        {
            get
            {
                return new Mailings.Dsl(_client);
            }
        } 
    }

    namespace Mailings
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Filtered.Dsl Filtered
            {
                get
                {
                    return new Filtered.Dsl(_client);
                }
            }

            public Groups.Dsl Groups
            {
                get
                {
                    return new Groups.Dsl(_client);
                }
            }
            
            /// <summary>
            /// Create a mailing.
            /// </summary>
            public Post.Response.Mailing Post(
                    Post.Request.MailingProperties request
                )
            {
                return (Post.Response.Mailing)_client.Execute("/mailings/{AccountId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Mailing));
            }
            
            /// <summary>
            /// Get a mailing.
            /// </summary>
            public GetByMailingId.Response.Mailing Get(
                    Guid mailingId
                )
            {
                return (GetByMailingId.Response.Mailing)_client.Execute("/mailings/{AccountId}/{MailingId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "mailingId", mailingId }
                         }, 
                    null,
                    typeof(GetByMailingId.Response.Mailing));
            }
            
            /// <summary>
            /// Modify a mailing.
            /// </summary>
            public void Put(
                    Guid mailingId
                ,                     PutByMailingId.Request.MailingProperties request
                )
            {
                _client.Execute("/mailings/{AccountId}/{MailingId}", 
                    Verb.Put, 
                    new Dictionary<string, object> { 
                            { "mailingId", mailingId }
                         }, 
                    request,
                    null);
            }
            
            /// <summary>
            /// Delete a mailing.
            /// </summary>
            public void Delete(
                    Guid mailingId
                )
            {
                _client.Execute("/mailings/{AccountId}/{MailingId}", 
                    Verb.Delete, 
                    new Dictionary<string, object> { 
                            { "mailingId", mailingId }
                         }, 
                    null,
                    null);
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class MailingProperties
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Subject { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public MailingFormat? MailingFormat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? GroupId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? AddressId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FromEmail { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FromName { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ReplyToEmail { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String HtmlContent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String TextContent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String CustomHtmlUnsubscribeLinkText { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String CustomTextUnsubscribeLinkText { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? IncludeSendToAFriendLink { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String SendToAFriendLinkText { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public TrackedLinks TrackedLinks { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum MailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class TrackedLinks : 
                    List<TrackedLink> { }

                /// <summary>
                /// 
                /// </summary>
                public class TrackedLink
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public TrackedLinkMailingFormat LinkMailingFormat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Url { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum TrackedLinkMailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                };

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Mailing
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                }

            }
        }

        namespace GetByMailingId
        {

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Mailing
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Subject { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public MailingFormat MailingFormat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid GroupId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid AddressId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FromEmail { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FromName { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ReplyToEmail { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String HtmlContent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String TextContent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean IncludeSendToAFriendLink { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String SendToAFriendLinkText { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public TrackedLinks TrackedLinks { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Modified { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum MailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class TrackedLinks : 
                    List<TrackedLink> { }

                /// <summary>
                /// 
                /// </summary>
                public class TrackedLink
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Url { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public TrackedLinkMailingFormat LinkMailingFormat { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Modified { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum TrackedLinkMailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                };

            }
        }

        namespace PutByMailingId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class MailingProperties
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Subject { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public MailingFormat? MailingFormat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? GroupId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? AddressId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FromEmail { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FromName { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ReplyToEmail { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String HtmlContent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String TextContent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String CustomHtmlUnsubscribeLinkText { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String CustomTextUnsubscribeLinkText { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? IncludeSendToAFriendLink { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String SendToAFriendLinkText { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public TrackedLinks TrackedLinks { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum MailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class TrackedLinks : 
                    List<TrackedLink> { }

                /// <summary>
                /// 
                /// </summary>
                public class TrackedLink
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public TrackedLinkMailingFormat LinkMailingFormat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Url { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum TrackedLinkMailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                };

            }

        }

        namespace DeleteByMailingId
        {

        }

    }

    namespace Mailings.Filtered
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Query mailings.
            /// </summary>
            public Post.Response.Mailings Post(
                    Post.Request.MailingFilter request
                )
            {
                return (Post.Response.Mailings)_client.Execute("/mailings/filtered/{AccountId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Mailings));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class MailingFilter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? GroupId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? IncludeSubGroups { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? MaxResults { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? StartIndex { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NewerThan { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? OlderThan { get; set; }
                }

            }

            namespace Response
            {
                public class Mailings : 
                    List<Mailing> { }

                /// <summary>
                /// 
                /// </summary>
                public class Mailing
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Subject { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public MailingFormat MailingFormat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid GroupId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid AddressId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FromEmail { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FromName { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ReplyToEmail { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String HtmlContent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String TextContent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean IncludeSendToAFriendLink { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String SendToAFriendLinkText { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public TrackedLinks TrackedLinks { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Modified { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum MailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class TrackedLinks : 
                    List<TrackedLink> { }

                /// <summary>
                /// 
                /// </summary>
                public class TrackedLink
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Url { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public TrackedLinkMailingFormat LinkMailingFormat { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Modified { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum TrackedLinkMailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                };

            }
        }

    }

    namespace Mailings.Groups
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Get all mailing groups.
            /// </summary>
            public Get.Response.Groups Get(
)
            {
                return (Get.Response.Groups)_client.Execute("/mailings/groups/{AccountId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
 }, 
                    null,
                    typeof(Get.Response.Groups));
            }
            
            /// <summary>
            /// Create a mailing group.
            /// </summary>
            public Post.Response.Group Post(
                    Post.Request.GroupProperties request
                )
            {
                return (Post.Response.Group)_client.Execute("/mailings/groups/{AccountId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Group));
            }
            
            /// <summary>
            /// Get a mailing group.
            /// </summary>
            public GetByGroupId.Response.Group Get(
                    Guid groupId
                )
            {
                return (GetByGroupId.Response.Group)_client.Execute("/mailings/groups/{AccountId}/{GroupId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "groupId", groupId }
                         }, 
                    null,
                    typeof(GetByGroupId.Response.Group));
            }
            
            /// <summary>
            /// Modify a mailing group.
            /// </summary>
            public void Put(
                    Guid groupId
                ,                     PutByGroupId.Request.GroupProperties request
                )
            {
                _client.Execute("/mailings/groups/{AccountId}/{GroupId}", 
                    Verb.Put, 
                    new Dictionary<string, object> { 
                            { "groupId", groupId }
                         }, 
                    request,
                    null);
            }
            
            /// <summary>
            /// Delete a mailing group.
            /// </summary>
            public void Delete(
                    Guid groupId
                )
            {
                _client.Execute("/mailings/groups/{AccountId}/{GroupId}", 
                    Verb.Delete, 
                    new Dictionary<string, object> { 
                            { "groupId", groupId }
                         }, 
                    null,
                    null);
            }
        }

        namespace Get
        {

            namespace Response
            {
                public class Groups : 
                    List<Group> { }

                /// <summary>
                /// 
                /// </summary>
                public class Group
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Modified { get; set; }
                }

            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class GroupProperties
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                }

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Group
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                }

            }
        }

        namespace GetByGroupId
        {

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Group
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Modified { get; set; }
                }

            }
        }

        namespace PutByGroupId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class GroupProperties
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                }

            }

        }

        namespace DeleteByGroupId
        {

        }

    }

    public partial class Api
    {
        public Reports.Dsl Reports
        {
            get
            {
                return new Reports.Dsl(_client);
            }
        } 
    }

    namespace Reports
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Easysmtp.Dsl Easysmtp
            {
                get
                {
                    return new Easysmtp.Dsl(_client);
                }
            }

            public Mailings.Dsl Mailings
            {
                get
                {
                    return new Mailings.Dsl(_client);
                }
            }
        }

    }

    namespace Reports.Easysmtp
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Bounces.Dsl Bounces
            {
                get
                {
                    return new Bounces.Dsl(_client);
                }
            }

            public Clicks.Dsl Clicks
            {
                get
                {
                    return new Clicks.Dsl(_client);
                }
            }

            public Opens.Dsl Opens
            {
                get
                {
                    return new Opens.Dsl(_client);
                }
            }

            public Optouts.Dsl Optouts
            {
                get
                {
                    return new Optouts.Dsl(_client);
                }
            }
            
            /// <summary>
            /// Report on EasySMTP delivery.
            /// </summary>
            public Get.Response.Mailings Get(
                    DateTime? enddate = null
                ,                     DateTime? startdate = null
                )
            {
                return (Get.Response.Mailings)_client.Execute("/reports/easysmtp/{AccountId}?enddate={enddate}&startdate={startdate}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "enddate", enddate }
                        ,                             { "startdate", startdate }
                         }, 
                    null,
                    typeof(Get.Response.Mailings));
            }
        }

        namespace Get
        {

            namespace Response
            {
                public class Mailings : 
                    List<Mailing> { }

                /// <summary>
                /// 
                /// </summary>
                public class Mailing
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid AccountId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid RecipientId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MailId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime MailDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Recipients { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String XCampaign { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Username { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Opens { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Clicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Bounces { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 OptOuts { get; set; }
                }

            }
        }

    }

    namespace Reports.Easysmtp.Bounces
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// 
            /// </summary>
            public Get.Response.Bounces Get(
                    DateTime? enddate = null
                ,                     DateTime? startdate = null
                )
            {
                return (Get.Response.Bounces)_client.Execute("/reports/easysmtp/bounces/{AccountId}?enddate={enddate}&startdate={startdate}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "enddate", enddate }
                        ,                             { "startdate", startdate }
                         }, 
                    null,
                    typeof(Get.Response.Bounces));
            }
        }

        namespace Get
        {

            namespace Response
            {
                public class Bounces : 
                    List<Bounce> { }

                /// <summary>
                /// 
                /// </summary>
                public class Bounce
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Code { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Timestamp { get; set; }
                }

            }
        }

    }

    namespace Reports.Easysmtp.Clicks
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// 
            /// </summary>
            public Get.Response.Clicks Get(
                    DateTime? enddate = null
                ,                     DateTime? startdate = null
                )
            {
                return (Get.Response.Clicks)_client.Execute("/reports/easysmtp/clicks/{AccountId}?enddate={enddate}&startdate={startdate}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "enddate", enddate }
                        ,                             { "startdate", startdate }
                         }, 
                    null,
                    typeof(Get.Response.Clicks));
            }
        }

        namespace Get
        {

            namespace Response
            {
                public class Clicks : 
                    List<Click> { }

                /// <summary>
                /// 
                /// </summary>
                public class Click
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid LinkId { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Timestamp { get; set; }
                }

            }
        }

    }

    namespace Reports.Easysmtp.Opens
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// 
            /// </summary>
            public Get.Response.Opens Get(
                    DateTime? enddate = null
                ,                     DateTime? startdate = null
                )
            {
                return (Get.Response.Opens)_client.Execute("/reports/easysmtp/opens/{AccountId}?enddate={enddate}&startdate={startdate}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "enddate", enddate }
                        ,                             { "startdate", startdate }
                         }, 
                    null,
                    typeof(Get.Response.Opens));
            }
        }

        namespace Get
        {

            namespace Response
            {
                public class Opens : 
                    List<Open> { }

                /// <summary>
                /// 
                /// </summary>
                public class Open
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Timestamp { get; set; }
                }

            }
        }

    }

    namespace Reports.Easysmtp.Optouts
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// 
            /// </summary>
            public Get.Response.OptOuts Get(
                    DateTime? enddate = null
                ,                     DateTime? startdate = null
                )
            {
                return (Get.Response.OptOuts)_client.Execute("/reports/easysmtp/optouts/{AccountId}?enddate={enddate}&startdate={startdate}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "enddate", enddate }
                        ,                             { "startdate", startdate }
                         }, 
                    null,
                    typeof(Get.Response.OptOuts));
            }
        }

        namespace Get
        {

            namespace Response
            {
                public class OptOuts : 
                    List<OptOut> { }

                /// <summary>
                /// 
                /// </summary>
                public class OptOut
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Email { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Type { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Timestamp { get; set; }
                }

            }
        }

    }

    namespace Reports.Mailings
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Bounces.Dsl Bounces
            {
                get
                {
                    return new Bounces.Dsl(_client);
                }
            }

            public Detail.Dsl Detail
            {
                get
                {
                    return new Detail.Dsl(_client);
                }
            }

            public Messagetesting.Dsl Messagetesting
            {
                get
                {
                    return new Messagetesting.Dsl(_client);
                }
            }

            public Opens.Dsl Opens
            {
                get
                {
                    return new Opens.Dsl(_client);
                }
            }

            public Optouts.Dsl Optouts
            {
                get
                {
                    return new Optouts.Dsl(_client);
                }
            }

            public Trackedlinks.Dsl Trackedlinks
            {
                get
                {
                    return new Trackedlinks.Dsl(_client);
                }
            }
        }

    }

    namespace Reports.Mailings.Bounces
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Detail.Dsl Detail
            {
                get
                {
                    return new Detail.Dsl(_client);
                }
            }
        }

    }

    namespace Reports.Mailings.Bounces.Detail
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Report on bounces.
            /// </summary>
            public PostByMailingId.Response.BounceDetails Post(
                    Guid mailingId
                ,                     PostByMailingId.Request.DetailReportFilter request
                )
            {
                return (PostByMailingId.Response.BounceDetails)_client.Execute("/reports/mailings/bounces/detail/{AccountId}/{MailingId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "mailingId", mailingId }
                         }, 
                    request,
                    typeof(PostByMailingId.Response.BounceDetails));
            }
        }

        namespace PostByMailingId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class DetailReportFilter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? MailingListId { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? FilterStartDate { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? FilterEndDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public BounceFilter BounceFilter { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class BounceFilter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public BounceSeverity? BounceSeverity { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public BounceCode? BounceTypeAbbreviation { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum BounceSeverity 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Hard,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Soft,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum BounceCode 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    AR,
                    /// <summary>
                    /// 
                    /// </summary>
                    BN,
                    /// <summary>
                    /// 
                    /// </summary>
                    CR,
                    /// <summary>
                    /// 
                    /// </summary>
                    GB,
                    /// <summary>
                    /// 
                    /// </summary>
                    HB,
                    /// <summary>
                    /// 
                    /// </summary>
                    MBAD,
                    /// <summary>
                    /// 
                    /// </summary>
                    MB,
                    /// <summary>
                    /// 
                    /// </summary>
                    MBKS,
                    /// <summary>
                    /// 
                    /// </summary>
                    MBRD,
                    /// <summary>
                    /// 
                    /// </summary>
                    MBSD,
                    /// <summary>
                    /// 
                    /// </summary>
                    SBDF,
                    /// <summary>
                    /// 
                    /// </summary>
                    SB,
                    /// <summary>
                    /// 
                    /// </summary>
                    SBMF,
                    /// <summary>
                    /// 
                    /// </summary>
                    SBMS,
                    /// <summary>
                    /// 
                    /// </summary>
                    SR,
                    /// <summary>
                    /// 
                    /// </summary>
                    TB,
                    /// <summary>
                    /// 
                    /// </summary>
                    NB,
                    /// <summary>
                    /// 
                    /// </summary>
                    UR,
                };

            }

            namespace Response
            {
                public class BounceDetails : 
                    List<BounceDetail> { }

                /// <summary>
                /// 
                /// </summary>
                public class BounceDetail
                {
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime BounceDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public BounceType BounceType { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Recipient Recipient { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class BounceType
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Abbreviation { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String LongDescription { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ShortDescription { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public BounceSeverity Severity { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum BounceSeverity 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Hard,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Soft,
                };

                /// <summary>
                /// 
                /// </summary>
                public class Recipient
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String EmailAddress { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Active { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreference EmailFormatPreference { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public RecipientProperties RecipientProperties { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreference 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class RecipientProperties : 
                    List<Property> { }

                /// <summary>
                /// 
                /// </summary>
                public class Property
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

            }
        }

    }

    namespace Reports.Mailings.Detail
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Report on multiple mailings.
            /// </summary>
            public Post.Response.MailingReports Post(
                    Post.Request.MailingReportFilter request
                )
            {
                return (Post.Response.MailingReports)_client.Execute("/reports/mailings/detail/{AccountId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.MailingReports));
            }
            
            /// <summary>
            /// Report on a individual mailing.
            /// </summary>
            public GetByMailingId.Response.MailingReport Get(
                    Guid mailingId
                )
            {
                return (GetByMailingId.Response.MailingReport)_client.Execute("/reports/mailings/detail/{AccountId}/{MailingId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "mailingId", mailingId }
                         }, 
                    null,
                    typeof(GetByMailingId.Response.MailingReport));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class MailingReportFilter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? MailingId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? StartIndex { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? MaxResults { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? ScheduledDeliveryOnOrAfter { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? ScheduledDeliveryOnOrBefore { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? MessageId { get; set; }
                }

            }

            namespace Response
            {
                public class MailingReports : 
                    List<MailingReport> { }

                /// <summary>
                /// 
                /// </summary>
                public class MailingReport
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MailingId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid AccountId { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime ScheduledDate { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime ScheduledDeliveryDate { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DeliveredDate { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Approved { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ApprovedBy { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String CreatedBy { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Modified { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ModifiedBy { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Message Message { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Lists Lists { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalLists { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime FirstListDeliveredDate { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime LastListDeliveredDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public RecipientSummary RecipientSummary { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public MailingReportForwardSummary ForwardSummary { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class Message
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MailingId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MessageId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public MailingFormat MessageFormat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Subject { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FromName { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FromAddress { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ReplyToAddress { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ContentHtml { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ContentText { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String AttachmentFile { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum MailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class Lists : 
                    List<MailingListReport> { }

                /// <summary>
                /// 
                /// </summary>
                public class MailingListReport
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MailingListId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MailingId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid AccountId { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DeliveredDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ListName { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public RecipientCount RecipientCount { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public MailingListReportForwardSummary ForwardSummary { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class RecipientCount
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Total { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Active { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Text { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Html { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TextAndHtml { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Bounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 HardBounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 OptOut { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Expected { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Sent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Suppressed { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Received { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Read { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SoftBounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SpamReport { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class MailingListReportForwardSummary
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Total { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SendToFriend { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Facebook { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Twitter { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 MySpace { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 LinkedIn { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 GoogleBuzz { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class RecipientSummary
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Total { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Active { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Text { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Html { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TextAndHtml { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Bounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 HardBounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 OptOut { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Expected { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Sent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Suppressed { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Received { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Read { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SoftBounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SpamReport { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class MailingReportForwardSummary
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Total { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SendToFriend { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Facebook { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Twitter { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 MySpace { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 LinkedIn { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 GoogleBuzz { get; set; }
                }

            }
        }

        namespace GetByMailingId
        {

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class MailingReport
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MailingId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid AccountId { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime ScheduledDate { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime ScheduledDeliveryDate { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DeliveredDate { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Approved { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ApprovedBy { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String CreatedBy { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Modified { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ModifiedBy { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Message Message { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Lists Lists { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalLists { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime FirstListDeliveredDate { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime LastListDeliveredDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public RecipientSummary RecipientSummary { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public MailingReportForwardSummary ForwardSummary { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class Message
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MailingId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MessageId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public MailingFormat MessageFormat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Subject { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FromName { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String FromAddress { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ReplyToAddress { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ContentHtml { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ContentText { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String AttachmentFile { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum MailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class Lists : 
                    List<MailingListReport> { }

                /// <summary>
                /// 
                /// </summary>
                public class MailingListReport
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MailingListId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid MailingId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid AccountId { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DeliveredDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ListName { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public RecipientCount RecipientCount { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public MailingListReportForwardSummary ForwardSummary { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class RecipientCount
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Total { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Active { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Text { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Html { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TextAndHtml { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Bounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 HardBounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 OptOut { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Expected { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Sent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Suppressed { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Received { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Read { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SoftBounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SpamReport { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class MailingListReportForwardSummary
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Total { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SendToFriend { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Facebook { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Twitter { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 MySpace { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 LinkedIn { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 GoogleBuzz { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class RecipientSummary
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Total { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Active { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Text { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Html { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TextAndHtml { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Bounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 HardBounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 OptOut { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Expected { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Sent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Suppressed { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Received { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Read { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SoftBounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SpamReport { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class MailingReportForwardSummary
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Total { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SendToFriend { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Facebook { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Twitter { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 MySpace { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 LinkedIn { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 GoogleBuzz { get; set; }
                }

            }
        }

    }

    namespace Reports.Mailings.Messagetesting
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Report on message testing.
            /// </summary>
            public Post.Response.MessageTestReports Post(
                    Post.Request.MessageTestReportFilter request
                )
            {
                return (Post.Response.MessageTestReports)_client.Execute("/reports/mailings/messagetesting/{AccountId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.MessageTestReports));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class MessageTestReportFilter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? MailingId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? StartIndex { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? MaxResults { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? ScheduledDeliveryOnOrAfter { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? ScheduledDeliveryOnOrBefore { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? MessageId { get; set; }
                }

            }

            namespace Response
            {
                public class MessageTestReports : 
                    List<MessageTestReport> { }

                /// <summary>
                /// 
                /// </summary>
                public class MessageTestReport
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Number { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid AccountId { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Scheduled { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime ScheduledDelivery { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Delivered { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String SuppressionType { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String RecipientFilter { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Approved { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ApprovedBy { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String CreatedBy { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Modified { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String ModifiedBy { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Messages Messages { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean IsRemainderScheduled { get; set; }
                }

                public class Messages : 
                    List<MailingMessageModel> { }

                /// <summary>
                /// 
                /// </summary>
                public class MailingMessageModel
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime LastEmailDelivered { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public RecipientCount RecipientCount { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class RecipientCount
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Sent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Delivered { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Views { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 ViewsUnique { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Clicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 ClicksUnique { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SoftBounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 HardBounce { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 OptOut { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 SpamReport { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal OpenRate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal ClickRate { get; set; }
                }

            }
        }

    }

    namespace Reports.Mailings.Opens
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Detail.Dsl Detail
            {
                get
                {
                    return new Detail.Dsl(_client);
                }
            }
        }

    }

    namespace Reports.Mailings.Opens.Detail
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Report on opens.
            /// </summary>
            public PostByMailingId.Response.ReadDetails Post(
                    Guid mailingId
                ,                     PostByMailingId.Request.DetailReportFilter request
                )
            {
                return (PostByMailingId.Response.ReadDetails)_client.Execute("/reports/mailings/opens/detail/{AccountId}/{MailingId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "mailingId", mailingId }
                         }, 
                    request,
                    typeof(PostByMailingId.Response.ReadDetails));
            }
        }

        namespace PostByMailingId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class DetailReportFilter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? MailingListId { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? FilterStartDate { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? FilterEndDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public PlatformFilter? Platform { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum PlatformFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Desktop,
                    /// <summary>
                    /// 
                    /// </summary>
                    Mobile,
                };

            }

            namespace Response
            {
                public class ReadDetails : 
                    List<ReadDetail> { }

                /// <summary>
                /// 
                /// </summary>
                public class ReadDetail
                {
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime FirstView { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime LastView { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalViews { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 UniqueViews { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalMobileViews { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 UniqueMobileViews { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalDesktopViews { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 UniqueDesktopViews { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Views Views { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Recipient Recipient { get; set; }
                }

                public class Views : 
                    List<View> { }

                /// <summary>
                /// 
                /// </summary>
                public class View
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String IpAddress { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String UserAgent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Platform Platform { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Browser Browser { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Platform 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Desktop,
                    /// <summary>
                    /// 
                    /// </summary>
                    Mobile,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum Browser 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Chrome,
                    /// <summary>
                    /// 
                    /// </summary>
                    Firefox,
                    /// <summary>
                    /// 
                    /// </summary>
                    InternetExplorer,
                    /// <summary>
                    /// 
                    /// </summary>
                    Opera,
                    /// <summary>
                    /// 
                    /// </summary>
                    Other,
                    /// <summary>
                    /// 
                    /// </summary>
                    Safari,
                    /// <summary>
                    /// 
                    /// </summary>
                    SeaMonkey,
                };

                /// <summary>
                /// 
                /// </summary>
                public class Recipient
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String EmailAddress { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Active { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreference EmailFormatPreference { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public RecipientProperties RecipientProperties { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreference 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class RecipientProperties : 
                    List<Property> { }

                /// <summary>
                /// 
                /// </summary>
                public class Property
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

            }
        }

    }

    namespace Reports.Mailings.Optouts
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Detail.Dsl Detail
            {
                get
                {
                    return new Detail.Dsl(_client);
                }
            }
        }

    }

    namespace Reports.Mailings.Optouts.Detail
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Report on opt outs.
            /// </summary>
            public PostByMailingId.Response.OptOutDetails Post(
                    Guid mailingId
                ,                     PostByMailingId.Request.DetailReportFilter request
                )
            {
                return (PostByMailingId.Response.OptOutDetails)_client.Execute("/reports/mailings/optouts/detail/{AccountId}/{MailingId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "mailingId", mailingId }
                         }, 
                    request,
                    typeof(PostByMailingId.Response.OptOutDetails));
            }
        }

        namespace PostByMailingId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class DetailReportFilter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? MailingListId { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? FilterStartDate { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? FilterEndDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public OptOutFilter OptOutFilter { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public class OptOutFilter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public OptOutType? OptOutType { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum OptOutType 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    GlobalOptOut,
                    /// <summary>
                    /// 
                    /// </summary>
                    List,
                    /// <summary>
                    /// 
                    /// </summary>
                    UnsubscribeRequest,
                };

            }

            namespace Response
            {
                public class OptOutDetails : 
                    List<OptOutDetail> { }

                /// <summary>
                /// 
                /// </summary>
                public class OptOutDetail
                {
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime OptOutDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public OptOutType OptOutType { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Recipient Recipient { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum OptOutType 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    GlobalOptOut,
                    /// <summary>
                    /// 
                    /// </summary>
                    List,
                    /// <summary>
                    /// 
                    /// </summary>
                    UnsubscribeRequest,
                };

                /// <summary>
                /// 
                /// </summary>
                public class Recipient
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String EmailAddress { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Active { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreference EmailFormatPreference { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public RecipientProperties RecipientProperties { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreference 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class RecipientProperties : 
                    List<Property> { }

                /// <summary>
                /// 
                /// </summary>
                public class Property
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

            }
        }

    }

    namespace Reports.Mailings.Trackedlinks
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Detail.Dsl Detail
            {
                get
                {
                    return new Detail.Dsl(_client);
                }
            }

            public Summary.Dsl Summary
            {
                get
                {
                    return new Summary.Dsl(_client);
                }
            }
        }

    }

    namespace Reports.Mailings.Trackedlinks.Detail
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Detailed report on tracked links.
            /// </summary>
            public PostByMailingId.Response.TrackedLinks Post(
                    Guid mailingId
                ,                     PostByMailingId.Request.Filter request
                )
            {
                return (PostByMailingId.Response.TrackedLinks)_client.Execute("/reports/mailings/trackedlinks/detail/{AccountId}/{MailingId}", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
                            { "mailingId", mailingId }
                         }, 
                    request,
                    typeof(PostByMailingId.Response.TrackedLinks));
            }
        }

        namespace PostByMailingId
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class Filter
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? ListId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? TrackedLinkId { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? AdvertiserId { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? StartDate { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? EndDate { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public PlatformFilter? Platform { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum PlatformFilter 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    All,
                    /// <summary>
                    /// 
                    /// </summary>
                    Desktop,
                    /// <summary>
                    /// 
                    /// </summary>
                    Mobile,
                };

            }

            namespace Response
            {
                public class TrackedLinks : 
                    List<TrackedLink> { }

                /// <summary>
                /// 
                /// </summary>
                public class TrackedLink
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Url { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public TrackedLinkMailingFormat MailingFormat { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime FirstClick { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime LastClick { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 UniqueClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalMobileClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 UniqueMobileClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalDesktopClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 UniqueDesktopClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Clicks Clicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Recipient Recipient { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum TrackedLinkMailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                };

                public class Clicks : 
                    List<Click> { }

                /// <summary>
                /// 
                /// </summary>
                public class Click
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String IpAddress { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String UserAgent { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Platform Platform { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Browser Browser { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Platform 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Desktop,
                    /// <summary>
                    /// 
                    /// </summary>
                    Mobile,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum Browser 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Chrome,
                    /// <summary>
                    /// 
                    /// </summary>
                    Firefox,
                    /// <summary>
                    /// 
                    /// </summary>
                    InternetExplorer,
                    /// <summary>
                    /// 
                    /// </summary>
                    Opera,
                    /// <summary>
                    /// 
                    /// </summary>
                    Other,
                    /// <summary>
                    /// 
                    /// </summary>
                    Safari,
                    /// <summary>
                    /// 
                    /// </summary>
                    SeaMonkey,
                };

                /// <summary>
                /// 
                /// </summary>
                public class Recipient
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String EmailAddress { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime Created { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Active { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public EmailFormatPreference EmailFormatPreference { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public RecipientProperties RecipientProperties { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum EmailFormatPreference 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    None,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                    /// <summary>
                    /// 
                    /// </summary>
                    TextAndHtml,
                };

                public class RecipientProperties : 
                    List<Property> { }

                /// <summary>
                /// 
                /// </summary>
                public class Property
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Value { get; set; }
                }

            }
        }

    }

    namespace Reports.Mailings.Trackedlinks.Summary
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// Summary report on tracked links.
            /// </summary>
            public GetByMailingId.Response.TrackedLinks Get(
                    Guid mailingId
                )
            {
                return (GetByMailingId.Response.TrackedLinks)_client.Execute("/reports/mailings/trackedlinks/summary/{AccountId}/{MailingId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "mailingId", mailingId }
                         }, 
                    null,
                    typeof(GetByMailingId.Response.TrackedLinks));
            }
            
            /// <summary>
            /// Summary report on tracked links by list.
            /// </summary>
            public GetByMailingIdAndListId.Response.TrackedLinks Get(
                    Guid mailingId
                ,                     Guid listId
                )
            {
                return (GetByMailingIdAndListId.Response.TrackedLinks)_client.Execute("/reports/mailings/trackedlinks/summary/{AccountId}/{MailingId}/{ListId}", 
                    Verb.Get, 
                    new Dictionary<string, object> { 
                            { "mailingId", mailingId }
                        ,                             { "listId", listId }
                         }, 
                    null,
                    typeof(GetByMailingIdAndListId.Response.TrackedLinks));
            }
        }

        namespace GetByMailingId
        {

            namespace Response
            {
                public class TrackedLinks : 
                    List<TrackedLink> { }

                /// <summary>
                /// 
                /// </summary>
                public class TrackedLink
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Url { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public TrackedLinkMailingFormat LinkMailingFormat { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime FirstClick { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime LastClick { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 UniqueClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalMobileClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 UniqueMobileClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalDesktopClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 UniqueDesktopClicks { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum TrackedLinkMailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                };

            }
        }

        namespace GetByMailingIdAndListId
        {

            namespace Response
            {
                public class TrackedLinks : 
                    List<TrackedLink> { }

                /// <summary>
                /// 
                /// </summary>
                public class TrackedLink
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Id { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Name { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Url { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public TrackedLinkMailingFormat LinkMailingFormat { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime FirstClick { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime LastClick { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 UniqueClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalMobileClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 UniqueMobileClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 TotalDesktopClicks { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 UniqueDesktopClicks { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum TrackedLinkMailingFormat 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Html,
                    /// <summary>
                    /// 
                    /// </summary>
                    Text,
                };

            }
        }

    }

    public partial class Api
    {
        public Security.Dsl Security
        {
            get
            {
                return new Security.Dsl(_client);
            }
        } 
    }

    namespace Security
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Authenticate.Dsl Authenticate
            {
                get
                {
                    return new Authenticate.Dsl(_client);
                }
            }
        }

    }

    namespace Security.Authenticate
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// 
            /// </summary>
            public Post.Response.Result Post(
                    Post.Request.Credentials request
                )
            {
                return (Post.Response.Result)_client.Execute("/security/authenticate", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Result));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class Credentials
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String AccountKey { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Username { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public String Password { get; set; }
                }

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Result
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public AuthStatus Status { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum AuthStatus 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    AccountDisabled,
                    /// <summary>
                    /// 
                    /// </summary>
                    InvalidUsernameOrPassword,
                    /// <summary>
                    /// 
                    /// </summary>
                    Success,
                    /// <summary>
                    /// 
                    /// </summary>
                    TooManyFailedLoginAttempts,
                    /// <summary>
                    /// 
                    /// </summary>
                    TrialExpired,
                };

            }
        }

    }

    public partial class Api
    {
        public Test.Dsl Test
        {
            get
            {
                return new Test.Dsl(_client);
            }
        } 
    }

    namespace Test
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }

            public Authorizationerror.Dsl Authorizationerror
            {
                get
                {
                    return new Authorizationerror.Dsl(_client);
                }
            }

            public Innertimeouterror.Dsl Innertimeouterror
            {
                get
                {
                    return new Innertimeouterror.Dsl(_client);
                }
            }

            public Notfounderror.Dsl Notfounderror
            {
                get
                {
                    return new Notfounderror.Dsl(_client);
                }
            }

            public Servererror.Dsl Servererror
            {
                get
                {
                    return new Servererror.Dsl(_client);
                }
            }

            public Sourceerror.Dsl Sourceerror
            {
                get
                {
                    return new Sourceerror.Dsl(_client);
                }
            }

            public Timeouterror.Dsl Timeouterror
            {
                get
                {
                    return new Timeouterror.Dsl(_client);
                }
            }

            public Validationerror.Dsl Validationerror
            {
                get
                {
                    return new Validationerror.Dsl(_client);
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            public Post.Response.Test Post(
                    Post.Request.Test request
                )
            {
                return (Post.Response.Test)_client.Execute("/test", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Test));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char? NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte? NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte? NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16? NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16? NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32? NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64? NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64? NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single? NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double? NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal? NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan? NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum? NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }
        }

    }

    namespace Test.Authorizationerror
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// 
            /// </summary>
            public Post.Response.Test Post(
                    Post.Request.Test request
                )
            {
                return (Post.Response.Test)_client.Execute("/test/authorizationerror", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Test));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char? NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte? NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte? NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16? NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16? NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32? NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64? NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64? NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single? NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double? NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal? NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan? NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum? NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }
        }

    }

    namespace Test.Innertimeouterror
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// 
            /// </summary>
            public Post.Response.Test Post(
                    Post.Request.Test request
                )
            {
                return (Post.Response.Test)_client.Execute("/test/innertimeouterror", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Test));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char? NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte? NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte? NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16? NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16? NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32? NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64? NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64? NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single? NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double? NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal? NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan? NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum? NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }
        }

    }

    namespace Test.Notfounderror
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// 
            /// </summary>
            public Post.Response.Test Post(
                    Post.Request.Test request
                )
            {
                return (Post.Response.Test)_client.Execute("/test/notfounderror", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Test));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char? NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte? NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte? NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16? NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16? NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32? NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64? NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64? NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single? NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double? NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal? NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan? NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum? NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }
        }

    }

    namespace Test.Servererror
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// 
            /// </summary>
            public Post.Response.Test Post(
                    Post.Request.Test request
                )
            {
                return (Post.Response.Test)_client.Execute("/test/servererror", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Test));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char? NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte? NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte? NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16? NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16? NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32? NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64? NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64? NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single? NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double? NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal? NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan? NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum? NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }
        }

    }

    namespace Test.Sourceerror
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// 
            /// </summary>
            public void Post(
                    Post.Request.Test request
                )
            {
                _client.Execute("/test/sourceerror", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    null);
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char? NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte? NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte? NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16? NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16? NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32? NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64? NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64? NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single? NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double? NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal? NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan? NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum? NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }

        }

    }

    namespace Test.Timeouterror
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// 
            /// </summary>
            public Post.Response.Test Post(
                    Post.Request.Test request
                )
            {
                return (Post.Response.Test)_client.Execute("/test/timeouterror", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Test));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char? NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte? NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte? NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16? NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16? NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32? NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64? NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64? NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single? NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double? NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal? NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan? NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum? NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }
        }

    }

    namespace Test.Validationerror
    {
        public class Dsl
        {
            private readonly IHttpClient _client;

            public Dsl(IHttpClient client)
            {
                _client = client;
            }
            
            /// <summary>
            /// 
            /// </summary>
            public Post.Response.Test Post(
                    Post.Request.Test request
                )
            {
                return (Post.Response.Test)_client.Execute("/test/validationerror", 
                    Verb.Post, 
                    new Dictionary<string, object> { 
 }, 
                    request,
                    typeof(Post.Response.Test));
            }
        }

        namespace Post
        {
            namespace Request
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char? NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean? NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte? NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte? NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16? NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16? NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32? NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32? NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64? NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64? NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single? NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double? NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal? NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime? NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan? NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid? NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum? NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }

            namespace Response
            {
                /// <summary>
                /// 
                /// </summary>
                public class Test
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    public String String { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char Char { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Char NullableChar { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean Boolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Boolean NullableBoolean { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte Byte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Byte NullableByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte SignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public SByte NullableSignedByte { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 Short { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int16 NullableShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 UnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt16 NullableUnsignedShort { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 Integer { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int32 NullableInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 UnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt32 NullableUnsignedInteger { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 Long { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Int64 NullableLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 UnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public UInt64 NullableUnsignedLong { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single Float { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Single NullableFloat { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double Double { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Double NullableDouble { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal Decimal { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Decimal NullableDecimal { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime DateTime { get; set; }
                    /// <summary>
                    /// Note: Datetime values are formatted as ISO 8601 [http://en.wikipedia.org/wiki/ISO_8601] and always expressed in UTC (yyyy-MM-ddThh:mm:ss.nZ).
                    /// </summary>
                    public DateTime NullableDateTime { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan TimeSpan { get; set; }
                    /// <summary>
                    /// Note: Time spans are formatted as d.hh:mm:ss.n.
                    /// </summary>
                    public TimeSpan NullableTimeSpan { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid Guid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Guid NullableGuid { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public Enum Enum { get; set; }
                    /// <summary>
                    /// 
                    /// </summary>
                    public NullableEnum NullableEnum { get; set; }
                }

                /// <summary>
                /// 
                /// </summary>
                public enum Enum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

                /// <summary>
                /// 
                /// </summary>
                public enum NullableEnum 
                {
                    /// <summary>
                    /// 
                    /// </summary>
                    Value1,
                    /// <summary>
                    /// 
                    /// </summary>
                    Value2,
                };

            }
        }

    }
}