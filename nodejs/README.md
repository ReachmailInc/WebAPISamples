ReachMail API client library for node.js
========================================

reachmailnj aims to provide a simple client library for interacing with the
[ReachMail API](https://services.reachmail.net/documentation).

## Requirements

- [node](http://nodejs.org/) v0.6+

## Getting started

This wrapper provides a number of convenience functions for working with
various API services. Each convenience function is named after the section and
service it accesses, e.g. administrationAddresses() accesses 
/administration/addresses. Convenience functions will require at least a 
callback function and in most cases, parameters representing your account
information and service request details. A complete list of the convenience
functions is included below.

Alternatively, the basic functions for 'GET', 'POST', 'PUT', and 'DELETE' have
been provided if you would like to access the services directly. Refer to the
"Function parameters" section below for an example. Documentation on the
required parameters for each service can be found in the [ReachMail API documentation](https://services.reachmail.net).

Note that at this time the client library supports only token authentication.
For more information, refer to these instructions on [creating a token](http://reachmail.zendesk.com/entries/26267216-Setting-authorization-tokens).

### Installing the wrapper

To install the wrapper via npm:

        $mkdir MyApplication
        $cd MyApplication
        $npm install reachmailapi

Or clone the full repository and copy the NodeJS wrapper to your application 
directory:

        $git clone git@github.com:ReachmailInc/WebAPISamples.git
        $mkdir MyApplication
        $cp WebAPISamples/nodejs/reachmailapi.js MyApplication

### Configuring the API

        var reachmail = require('reachmailapi');

        var api = new reachmail({token: 'your_token_here'});

### Base Functions:

- get(serviceUrl, callback())

        api.get('/administration/users/current', 
                function (httpCode, response) {
          console.log(httpCode);
          console.log(response);
        });

- post(serviceUrl, postBody, callback())
        
        api.post('/lists/filtered/00000000-0000-0000-0000-000000000000',
                {'LargerThan': 500}, function (httpCode, response) {
          console.log(httpCode);
          console.log(response);
        });

- put(serviceUrl, postBody, callback())

        api.put('/lists/00000000-0000-0000-0000-000000000000/00000000-0000-0000-0000-000000000000',
                {'Name': 'New list name'}, function (httpCode, response) {
          console.log(httpCode);
          console.log(response);
        });

- delete(serviceUrl, callback())

        api.delete('lists/00000000-0000-0000-0000-000000000000/00000000-0000-0000-0000-000000000000',
                    function (httpCode, response) {
          console.log(httpCode);
          console.log(response);
        });

### Convenience Functions

All convenience functions are listed here along with their required
parameters, each one is linked to it's corresponding service documentation.

- [`administrationUsersCurrent(callback)`](http://services.reachmail.net/#Administration@/administration/addresses)
- [`administrationAddresses(AccountId, callback)`](http://services.reachmail.net/#Administration@/administration/users/current)
- [`campaigns(AccountId, callback)`](http://services.reachmail.net/#Campaigns@/campaigns)
- [`campaignsMessageTesting(AccountId, callback)`](http://services.reachmail.net/#Campaigns@/campaigns)
- [`dataUpload(Request, callback)`](http://services.reachmail.net/#Data@/data)
- [`dataDownload(dataid, callback)`](http://services.reachmail.net/#Data@/data)
- [`easySmtpDelivery(AccountId, Request, callback)`](http://services.reachmail.net/#EasySMTP@/easysmtp)
- [`listsCreate(AccountId, Request, callback)`](http://services.reachmail.net/#Lists@/lists)
- [`listsInformation(AccountId, ListId, callback)`](http://services.reachmail.net/#Lists@/lists)
- [`listsModify(AccountId, ListId, Request, callback)`](http://services.reachmail.net/#Lists@/lists)
- [`listsDelete(AccountId, ListId, callback)`](http://services.reachmail.net/#Lists@/lists)
- [`listsExport(AccountId, ListId, Request, callback)`](http://services.reachmail.net/#Lists@/lists/export)
- [`listsExportStatus(AccountId, exportid, callback)`](http://services.reachmail.net/#Lists@/lists/export/status)
- [`listsFields(accoutid, callback)`](http://services.reachmail.net/#Lists@/lists/fields)
- [`listsFiltered(AccountId, Request, callback)`](http://services.reachmail.net/#Lists@/lists/filtered)
- [`listsGroupCreate(AccountId, Request, callback)`](http://services.reachmail.net/#Lists@/lists/groups)
- [`listsGroupInformation(AccountId, GroupId, callback)`](http://services.reachmail.net/#Lists@/lists/groups)
- [`listsGroupModify(AccountId, GroupId, Request, callback)`](http://services.reachmail.net/#Lists@/lists/groups)
- [`listsGroupDelete(AccountId, GroupId, callback)`](http://services.reachmail.net/#Lists@/lists/groups)
- [`listsImport(AccountId, ListId, Request, callback)`](http://services.reachmail.net/#Lists@/lists/import)
- [`listsImportStatus(AccountId, importid, callback)`](http://services.reachmail.net/#Lists@/lists/import/status)
- [`listsOptOut(AccountId, ListId, Request, callback)`](http://services.reachmail.net/#Lists@/lists/optout)
- [`listsRecipientsCreate(AccountId, ListId, Request, callback)`](http://services.reachmail.net/#Lists@/lists/recipients)
- [`listsRecipientsInformation(AccountId, ListId, email, callback)`](http://services.reachmail.net/#Lists@/lists/recipients)
- [`listsRecipientsDelete(AccountId, ListId, email, callback)`](http://services.reachmail.net/#Lists@/lists/recipients)
- [`listsRecipientsModify(AccountId, ListId, email, callback)`](http://services.reachmail.net/#Lists@/lists/recipients)
- [`listsRecipientsFiltered(AccountId, ListId, Request, callback)`](http://services.reachmail.net/#Lists@/lists/recipients/filtered)
- [`listsRecipientsFilteredDelete(AccountId, ListId, callback)`](http://services.reachmail.net/#Lists@/lists/recipients/filtered/delete)
- [`listsRecipientsFilteredModify(AccountId, ListId, Request, callback)`](http://services.reachmail.net/#Lists@/lists/recipients/filtered/modify)
- [`listsRecipientsFilteredSubscribe(AccountId, ListId, Request, callback)`](http://services.reachmail.net/#Lists@/lists/recipients/subscribe)
- [`listsSubscriptionForm(AccountId, formid, callback)`](http://services.reachmail.net/#Lists@/lists/subscriptionform)
- [`listsSubscriptionFormFiltered(AccountId, callback)`](http://services.reachmail.net/#Lists@/lists/subscriptionform/filtered)
- [`mailingsCreate(AccountId, Request, callback)`](http://services.reachmail.net/#Mailings@/mailings)
- [`mailingsInformation(AccountId, MailingId, callback)`](http://services.reachmail.net/#Mailings@/mailings)
- [`mailingsModify(AccountId, MailingId, Request, callback)`](http://services.reachmail.net/#Mailings@/mailings)
- [`mailingsDelete(AccountId, MailingId, callback)`](http://services.reachmail.net/#Mailings@/mailings)
- [`mailingsFiltered(AccountId, Request, callback)`](http://services.reachmail.net/#Mailings@/mailings/filtered)
- [`mailingsGroups(AccountId, callback)`](http://services.reachmail.net/#Mailings@/mailings/groups)
- [`mailingsGroupsCreate(AccountId, Request, callback)`](http://services.reachmail.net/#Mailings@/mailings/groups)
- [`mailingsGroupsInformation(AccountId, GroupId, callback)`](http://services.reachmail.net/#Mailings@/mailings/groups)
- [`mailingsGroupsModify(AccountId, GroupId, Request, callback)`](http://services.reachmail.net/#Mailings@/mailings/groups)
- [`mailingsGroupsDelete(AccountId, GroupId, callback)`](http://services.reachmail.net/#Mailings@/mailings/groups)
- [`reportsMailingsBouncesDetail(AccountId, MailingId, Request, callback)`](http://services.reachmail.net/#Reports@/reports/mailings/bounces/detail)
- [`reportsMailingsDetail(AccountId, MailingId, Request, callback)`](http://services.reachmail.net/#Reports@/reports/mailings/detail)
- [`reportsMailingsDetailInformation(AccountId, MailingId, callback)`](http://services.reachmail.net/#Reports@/reports/mailings/detail)
- [`reportsMailingsMessageTesting(AccountId, Request, callback)`](http://services.reachmail.net/#Reports@/reports/mailings/messagetesting)
- [`reportsMailingsOpensDetail(AccountId, MailingId, Request, callback)`](http://services.reachmail.net/#Reports@/reports/mailings/opens/detail)
- [`reportsMailingsOptOutsDetail(AccountId, MailingId, Request, callback)`](http://services.reachmail.net/#Reports@/reports/mailings/optouts/detail)
- [`reportsMailingsTrackedLinksDetail(AccountId, MailingId, Request, callback)`](http://services.reachmail.net/#Reports@/reports/mailings/trackedlinks/detail)
- [`reportsMailingsTrackedLinksSummary(AccountId, MailingId, callback)`](http://services.reachmail.net/#Reports@/reports/mailings/trackedlinks/summary)
- [`reportsMailingsTrackedLinksSummaryList = function(AccountId, MailingId, ListId, callback)`](http://services.reachmail.net/#Reports@/reports/mailings/trackedlinks/summary)
- [`reportsEasySmtp(AccountId, StartDate, EndDate, callback)`](http://services.reachmail.net/#Reports@/reports/easysmtp)
- [`reportsEasySmtpBounces(AccountId, StartDate, EndDate, callback)`](http://services.reachmail.net/#Reports@/reports/easysmtp/bounces)
- [`reportsEasySmtpOptouts(AccountId, StartDate, EndDate, callback)`](http://services.reachmail.net/#Reports@/reports/easysmtp/optouts)
- [`reportsEasySmtpOpens(AccountId, StartDate, EndDate, callback)`](http://services.reachmail.net/#Reports@/reports/easysmtp/opens)
- [`reportsEasySmtpClicks(AccountId, StartDate, EndDate, callback)`](http://services.reachmail.net/#Reports@/reports/easysmtp/clicks)

Questions regarding use of this software should be referred to
support@reachmail.com
