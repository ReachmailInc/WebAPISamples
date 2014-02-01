ReachMail API client library for node.js
========================================

reachmailnj aims to provide a simple client library for interacing with the
[ReachMail API](https://services.reachmail.net/documentation).

## Requirements

- [node](http://nodejs.org/) v0.6+

## Getting started

This is the preliminary version of the client library for node.js and as such
has limited convenience functions and expects an at least intermediate 
level of familiarity with node.js

Note that at this time the client library supports only token authentication.
For more information, refer to these instructions on [creating a token](http://reachmail.zendesk.com/entries/26267216-Setting-authorization-tokens).

### Configure API

        var reachmail = require('/path/to/reachmailapi');

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

Questions regarding use of this software should be referr to
support@reachmail.com
