ReachMail API client library for php 
====================================

rmapi.class.php aims to provide a simple client library for interacing 
with the [ReachMail API](https://services.reachmail.net/documentation).

## Requirements

- [php](http://php.net/) v4+

## Getting started

This php module is a simple client library with convenience functions 
for all of the ReachMail API services.

Similarly to the API itself it is geared toward those with at least 
intermediate programming ability.

Note that at this time the client library supports only token authentication.
For more information, refer to these instructions on [creating a token](http://reachmail.zendesk.com/entries/26267216-Setting-authorization-tokens).

## Installation

There's no specific installation process for this library. Simply place it
in a directory of your choosing, making sure that the directory is 
accessible to all who need to access the library.

        install -m 0750 rmapi.class.php /usr/share/php5/rmapi.class.php

### Usage 

To use this library, use the PHP function include_once() to add the library
functions to your script. Then create a new library object with your 
API token.

        include_once('/usr/share/php5/rmapi.class.php');
        $rmapi = new RMAPI('your-api-token');

Once your library object has been created, individual services can be 
called using the various convenience functions.

        $account_info = $rmapi->rm_administrationUsersCurrent();

### Function parameters and return values:

Some functions will require that you submit parameters. These parameters may
be URI parameters or a request body for POST and PUT services. In the case
of POST and PUT services, the request body should always be submitted as
RequestBody. URI parameter names match the names detailed in the service 
documentation.

For example, the [campaigns service](http://services.reachmail.net/documentation#Campaigns@/campaigns) used to schedule mailings requires the URI
parameter AccountId and a RequestBody. The service would be called as follows.

        $campaign = $rmapi->rm_campaigns(AccountId='id',
                RequestBody='request_json');

All functions return the HTTP status code and the service response in an array.
The service response will be a JSON encoded string. Note that some services
will not return a response, success or failure is indicated by the status
code alone.

        Array
        (
            [http_status] => 200
            [service_response] => "{}"
        )

Questions regarding use of this software should be refered to
support@reachmail.com
