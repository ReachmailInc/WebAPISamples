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
information and service request details. 

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

### Function parameters:

- get (service_url, callback())
- post (service_url, post_body, callback())
- put (service_url, post_body, callback())
- delete (service_url, callback())

        api.get('/administration/users/current', 
                function (http_code, response) {
            console.log(http_code);
            console.log(response);
        });

Questions regarding use of this software should be refered to
support@reachmail.com
