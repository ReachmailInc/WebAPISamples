#!/usr/pkg/bin/perl
#---- include Perl Modules. note that HTTP::Request::Common qw(POST) is
#---- included as an example but is only necessary if the API service requires
#---- a HTTP POST
use XML::LibXML;
use LWP::UserAgent;

#---- set the account information variables. If you're unsure what these are
#---- contact your ReachMail account admin or email support@reachmail.com.
#---- the account_id variable needs to be retrieved using API
#---- services, please refer to service
#---- AdministrationService\GetCurrentUser
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$account_id = 'api-account-id';
$list_id = 'api-list-id';

#---- set the user agent and content type ----
$ua = LWP::UserAgent->new;
$ua->agent("$0/0.1 " . $ua->agent);
$ua->default_header('Content-Type' => "application/xml");

#---- set URL and authentication for lists, make the request ----
$delete_list_url = HTTP::Request->new(DELETE => "https://services.reachmail.net/Rest/Contacts/v1/lists/$account_id/$list_id");
$delete_list_url->authorization_basic('account-id\admin', 'password');
$delete_list_request = $ua->request($delete_list_url);

