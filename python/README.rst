This software is a simple Python wrapper around the ReachMail API
(https://services.reachmail.net/documentation). This wrapper provides simple
convenience functions to access various API services.

INSTALLATION
------------
::
    $ python setup.py install

Note that httplib2 will be installed as a requirement to this software

USAGE
-----

This wrapper is very simple, requiring only the base module to be imported 
for use. It requires a valid API token for authentication and uses only the
JSON version of the API services. 

::
>>> import reachmail
>>> api = reachmail.ReachMail("my-token")

Service calls are grouped together under a method named after the first URI 
component as described in (https://services.reachmail.net/documentation). For
example, services.reachmail.net/adminsitration/users/current would be
accessed as follows.

::
>>> res = api.adminsitration.users_current()

Most service functions require keyword arguments representing components 
of the URL and or the HTTP message body. These keywords follow the naming 
conventions detailed in the service documentation, i.e. title case. For 
example, to correctly pass in an account id, use AccountId.

::
>>> res = api.administration.addresses(AccountId="my-account-id")

All services will return a named tuple which contains "status" and 
"content" parameters. Status will represent the HTTP status code of the 
service call. Note that some services will not return usable content,
success of failure is determined solely by the HTTP status code. Checking
status codes should therefore be an important part of any program 
incorporating this module.

Getting help
------------

Please refer all questions about this module or the ReachMail API to 
support@reachmail.com

