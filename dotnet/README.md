Reachmail API Wrapper
=============

This library allows you to easily interact with the Reachmail API in .NET.

Installation
------------

This library is distributed via [NuGet](http://nuget.org/packages/reachmail).

    PM> Install-Package reachmail

Usage
------------

Using the library is as easy as creating a `Reachmail` object:

```csharp
var reachmail = Reachmail.Create("accountkey", "username", "password");
```

From there you can navigate the hierarchy using Intellisense:

```csharp
var lists = reachmail.Contacts.Lists.Query.Post(new ListFilter { LargerThan = 500 });
lists.ForEach(x => Debug.WriteLine("{0}: {1}", x.Name, x.TotalActiveRecipients));
```

Most calls to the API accept an optional account id argument. The wrapper defaults to the account id of the authentication user so you do not need to set this.

The library structure closely follows url structure of the API so the [online documentation](http://services.reachmail.net/documentation) is in parity with the library. The properties in the library first match up to the url segments (Url parameters other than account id are prefixed with "By") and finally the http verb. For example the endpoint url  `GET:/reports/easysmtp/mailings/{AccountId}?enddate={enddate}&startdate={startdate}` would correspond to `reachmail.Reports.Easysmtp.Mailings.Get(startdate: ..., enddate: ...)`. Where possible comments in the online documentation are included in the library so you can see them in Intellisense.

If you are accessing the API behind a proxy server you can specify a proxy server as follows:

```csharp
var reachmail = Reachmail.Create("accountkey", "username", "password", proxy: new WebProxy("http://webproxy:80/"));
```

Examples
------------

The following examples illustrate how to work with the wrapper.

### Adding a recipient to a list:

```csharp
reachmail.Contacts.Lists.Recipients.ByListId.Post(listId, new RecipientProperties
    {
        Email = "user@domain.com",
        EmailFormatPreference = RecipientProperties.EmailFormatPreferenceOptions.Html,
        Properties = new List<Property>
            {
                new Property { Name = "FullName", Value = "Ed"},
                new Property { Name = "Zip", Value = "81504" }
            }
    });
```

### Delete all recipients from a list:

```csharp
reachmail.Contacts.Lists.Recipients.Query.Delete.ByListId.Post(listId, new RecipientFilter());
```
