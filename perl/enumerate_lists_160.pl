#!/usr/pkg/bin/perl
#---- include Perl Modules. note that HTTP::Request::Common qw(POST) is
#---- included as an example but is only necessary if the API service requires
#---- a HTTP POST
use XML::LibXML;
use LWP::UserAgent;
use HTTP::Request::Common qw(POST);

#---- set the account information variables. If you're unsure what these are
#---- contact your ReachMail account admin or email support@reachmail.com.
#---- the account_id variable needs to be retrieved using API
#---- services, please refer to service
#---- AdministrationService\GetCurrentUser
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$account_id = 'api-account-id';

#---- set the user agent and content type ----
$ua = LWP::UserAgent->new;
$ua->agent("$0/0.1 " . $ua->agent);
$ua->default_header('Content-Type' => "application/xml");

#---- set URL and authentication for lists, make the request ----
$list_url = HTTP::Request->new(POST => "https://services.reachmail.net/Rest/Contacts/v1/lists/query/api-account-id");
$list_url->content('<ListFilter></ListFilter>');
$list_url->authorization_basic('account-id\username', 'password');
$list_request = $ua->request($list_url);

#---- retrieve the content of the request and intialize a XML parser on it ----
$list_xml = $list_request->content;
$parser = XML::LibXML->new();
$list_dom = $parser->parse_string($list_xml);

#---- print the response ----
print $list_dom->toString(2);
