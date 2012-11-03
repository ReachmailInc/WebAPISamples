Reachmail API Wrapper
=============

This library allows you to interact with the Reachmail API in .NET.

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

The library structure closely follows url structure of the API. To learn more about the API checkout the online documentation [here](http://services.reachmail.net/documentation).