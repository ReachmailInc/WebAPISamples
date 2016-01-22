ReachMail API client library for Java
=====================================

ReachMail Java aims to provide a simple client library for interacing with the
[ReachMail API](https://services.reachmail.net/documentation).

## Requirements

- [Java](http://java.com/) v6+

## Getting started

This wrapper provides a number of convenience functions for working with
various API services. Each convenience function is named after the section and
service it accesses, e.g. administrationAddresses() accesses 
/administration/addresses. Convenience functions will require in most cases 
parameters representing your account information and service request details. 
A complete list of the convenience functions is included below. All functions
return an instance of the APIReponse class.

Alternatively, the basic functions for 'GET', 'POST', 'PUT', and 'DELETE' have
been provided if you would like to access the services directly. Refer to the
"Function parameters" section below for an example. Documentation on the
required parameters for each service can be found in the [ReachMail API documentation](https://services.reachmail.net).

Note that at this time the client library supports only token authentication.
For more information, refer to these instructions on [creating a token](http://reachmail.zendesk.com/entries/26267216-Setting-authorization-tokens).

### Installing the wrapper

Clone the full repository and copy the Java wrapper to your application 
directory:

        $git clone git@github.com:ReachmailInc/WebAPISamples.git
        $mkdir MyApplication
        $mkdir MyApplication/reachmail
        $cp WebAPISamples/java/* MyApplication/reachmail

### Compiling with the wrapper

Compiling your application with the Java wrapper should be as simple as adding
the wrappper's directory to your compilation definitions:

        $javac -cp ./MyApplication/reachmail MyApp.java

### Configuring the API

The Java client library currently only supports token authentication. Your 
API token can be supplied when creating an instance of the API class.

        ReachMailAPI api = new ReachMailAPI(<your-token>);

### The APIResponse class

All convenience functions and their base functions return an instance of the
APIResponse class. APIResponses include the following methods:

- code() The HTTP status code of the request
- response() The service response body (always JSON).

Keep in mind that some services will not return a response, the code() method
should always be used to verify that the service was executed successfully.

### Example

The following simple application can be used to test your installation. It
is available in the samples/ directory of the repository.

        import java.io.IOException;
        public class ReachMail
        {
          public static void main (String[] args) throws IOException
          {
            ReachMailAPI api = new ReachMailAPI(args[0]);
            APIResponse resp = api.administrationUsersCurrent();
            System.out.println(resp.code());
            System.out.println(resp.response());
          }
        }

### Base Functions:

- get(serviceUrl)

        APIResponse resp = api.get('/administration/users/current'); 

- post(serviceUrl, payload)
        
        APIResponse resp = api.post(
          '/lists/filtered/00000000-0000-0000-0000-000000000000',
          {'LargerThan': 500});

- put(serviceUrl, payload)

        APIResponse resp = api.put(
          '/lists/00000000-0000-0000-0000-000000000000/00000000-0000-0000-0000-000000000000',
          {'Name': 'New list name'});

- delete(serviceUrl)

        APIResponse = api.delete(
          'lists/00000000-0000-0000-0000-000000000000/00000000-0000-0000-0000-000000000000');

### Convenience Functions

All convenience functions are listed here along with their required
parameters, each one is linked to it's corresponding service documentation.

- [`administrationUsersCurrent()`](http://services.reachmail.net/#Administration@/administration/addresses)
- [`administrationAddresses(AccountId)`](http://services.reachmail.net/#Administration@/administration/users/current)
- [`campaigns(AccountId)`](http://services.reachmail.net/#Campaigns@/campaigns)
- [`campaignsMessageTesting(AccountId)`](http://services.reachmail.net/#Campaigns@/campaigns)
- [`dataUpload(RequestBody)`](http://services.reachmail.net/#Data@/data)
- [`dataDownload(dataid)`](http://services.reachmail.net/#Data@/data)
- [`easySmtpDelivery(AccountId, RequestBody)`](http://services.reachmail.net/#EasySMTP@/easysmtp)
- [`listsCreate(AccountId, RequestBody)`](http://services.reachmail.net/#Lists@/lists)
- [`listsInformation(AccountId, ListId)`](http://services.reachmail.net/#Lists@/lists)
- [`listsModify(AccountId, ListId, RequestBody)`](http://services.reachmail.net/#Lists@/lists)
- [`listsDelete(AccountId, ListId)`](http://services.reachmail.net/#Lists@/lists)
- [`listsExport(AccountId, ListId, RequestBody)`](http://services.reachmail.net/#Lists@/lists/export)
- [`listsExportStatus(AccountId, exportid)`](http://services.reachmail.net/#Lists@/lists/export/status)
- [`listsFields(accoutid)`](http://services.reachmail.net/#Lists@/lists/fields)
- [`listsFiltered(AccountId, RequestBody)`](http://services.reachmail.net/#Lists@/lists/filtered)
- [`listsGroupCreate(AccountId, RequestBody)`](http://services.reachmail.net/#Lists@/lists/groups)
- [`listsGroupInformation(AccountId, GroupId)`](http://services.reachmail.net/#Lists@/lists/groups)
- [`listsGroupModify(AccountId, GroupId, RequestBody)`](http://services.reachmail.net/#Lists@/lists/groups)
- [`listsGroupDelete(AccountId, GroupId)`](http://services.reachmail.net/#Lists@/lists/groups)
- [`listsImport(AccountId, ListId, RequestBody)`](http://services.reachmail.net/#Lists@/lists/import)
- [`listsImportStatus(AccountId, importid)`](http://services.reachmail.net/#Lists@/lists/import/status)
- [`listsOptOut(AccountId, ListId, RequestBody)`](http://services.reachmail.net/#Lists@/lists/optout)
- [`listsRecipientsCreate(AccountId, ListId, RequestBody)`](http://services.reachmail.net/#Lists@/lists/recipients)
- [`listsRecipientsInformation(AccountId, ListId, email)`](http://services.reachmail.net/#Lists@/lists/recipients)
- [`listsRecipientsDelete(AccountId, ListId, email)`](http://services.reachmail.net/#Lists@/lists/recipients)
- [`listsRecipientsModify(AccountId, ListId, email)`](http://services.reachmail.net/#Lists@/lists/recipients)
- [`listsRecipientsFiltered(AccountId, ListId, RequestBody)`](http://services.reachmail.net/#Lists@/lists/recipients/filtered)
- [`listsRecipientsFilteredDelete(AccountId, ListId)`](http://services.reachmail.net/#Lists@/lists/recipients/filtered/delete)
- [`listsRecipientsFilteredModify(AccountId, ListId, RequestBody)`](http://services.reachmail.net/#Lists@/lists/recipients/filtered/modify)
- [`listsRecipientsFilteredSubscribe(AccountId, ListId, RequestBody)`](http://services.reachmail.net/#Lists@/lists/recipients/subscribe)
- [`listsSubscriptionForm(AccountId, formid)`](http://services.reachmail.net/#Lists@/lists/subscriptionform)
- [`listsSubscriptionFormFiltered(AccountId)`](http://services.reachmail.net/#Lists@/lists/subscriptionform/filtered)
- [`mailingsCreate(AccountId, RequestBody)`](http://services.reachmail.net/#Mailings@/mailings)
- [`mailingsInformation(AccountId, MailingId)`](http://services.reachmail.net/#Mailings@/mailings)
- [`mailingsModify(AccountId, MailingId, RequestBody)`](http://services.reachmail.net/#Mailings@/mailings)
- [`mailingsDelete(AccountId, MailingId)`](http://services.reachmail.net/#Mailings@/mailings)
- [`mailingsFiltered(AccountId, RequestBody)`](http://services.reachmail.net/#Mailings@/mailings/filtered)
- [`mailingsGroups(AccountId)`](http://services.reachmail.net/#Mailings@/mailings/groups)
- [`mailingsGroupsCreate(AccountId, RequestBody)`](http://services.reachmail.net/#Mailings@/mailings/groups)
- [`mailingsGroupsInformation(AccountId, GroupId)`](http://services.reachmail.net/#Mailings@/mailings/groups)
- [`mailingsGroupsModify(AccountId, GroupId, RequestBody)`](http://services.reachmail.net/#Mailings@/mailings/groups)
- [`mailingsGroupsDelete(AccountId, GroupId)`](http://services.reachmail.net/#Mailings@/mailings/groups)
- [`reportsMailingsBouncesDetail(AccountId, MailingId, RequestBody)`](http://services.reachmail.net/#Reports@/reports/mailings/bounces/detail)
- [`reportsMailingsDetail(AccountId, MailingId, RequestBody)`](http://services.reachmail.net/#Reports@/reports/mailings/detail)
- [`reportsMailingsDetailInformation(AccountId, MailingId)`](http://services.reachmail.net/#Reports@/reports/mailings/detail)
- [`reportsMailingsMessageTesting(AccountId, RequestBody)`](http://services.reachmail.net/#Reports@/reports/mailings/messagetesting)
- [`reportsMailingsOpensDetail(AccountId, MailingId, RequestBody)`](http://services.reachmail.net/#Reports@/reports/mailings/opens/detail)
- [`reportsMailingsOptOutsDetail(AccountId, MailingId, RequestBody)`](http://services.reachmail.net/#Reports@/reports/mailings/optouts/detail)
- [`reportsMailingsTrackedLinksDetail(AccountId, MailingId, RequestBody)`](http://services.reachmail.net/#Reports@/reports/mailings/trackedlinks/detail)
- [`reportsMailingsTrackedLinksSummary(AccountId, MailingId)`](http://services.reachmail.net/#Reports@/reports/mailings/trackedlinks/summary)
- [`reportsMailingsTrackedLinksSummaryList = function(AccountId, MailingId, ListId)`](http://services.reachmail.net/#Reports@/reports/mailings/trackedlinks/summary)
- [`reportsEasySmtp(AccountId, StartDate, EndDate)`](http://services.reachmail.net/#Reports@/reports/easysmtp)
- [`reportsEasySmtpBounces(AccountId, StartDate, EndDate)`](http://services.reachmail.net/#Reports@/reports/easysmtp/bounces)
- [`reportsEasySmtpOptouts(AccountId, StartDate, EndDate)`](http://services.reachmail.net/#Reports@/reports/easysmtp/optouts)
- [`reportsEasySmtpOpens(AccountId, StartDate, EndDate)`](http://services.reachmail.net/#Reports@/reports/easysmtp/opens)
- [`reportsEasySmtpClicks(AccountId, StartDate, EndDate)`](http://services.reachmail.net/#Reports@/reports/easysmtp/clicks)

Questions regarding use of this software should be referred to
support@reachmail.com
