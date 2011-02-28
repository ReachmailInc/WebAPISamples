#!/usr/pkg/bin/perl
#---- include Perl Modules. note that HTTP::Request::Common qw(POST) is
#---- included as an example but is only necessary if the API service requires
#---- a HTTP POST
use XML::LibXML;
use LWP::UserAgent;
use HTTP::Request::Common qw(POST);

#---- set the account information variables. If you're unsure what these are
#---- contact your ReachMail account admin or email support@reachmail.com.
#---- the account_id and list_id variables need to be retrieved using API
#---- services, please refer to services
#---- AdministrationService\GetCurrentUser and EnumerateLists respectively
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$account_id = 'api-account-id';
$listid = 'api-list-id';

#---- set the user agent and content type ----
$ua = LWP::UserAgent->new;
$ua->agent("$0/0.1 " . $ua->agent);
$ua->default_header('Content-Type' => "application/xml");

#---- set the entity body for the mailing properties.
$mail_properties_xml = "<MailingProperties><FromEmail>news@companyx.com</FromEmail><FromName>Company X News</FromName><HtmlContent><![CDATA[<html><head><title>my mail</title></head><body><h1>Welcome to my newsletter</h1><p>I made this email with the ReachMail API</p></body></html>]]></HtmlContent><MailingFormat>Html</MailingFormat><Name>Demo API Mail</Name><Subject>Company X Newsletter</Subject></MailingProperties>";

#---- set the URL and make the request
$create_mail_url = HTTP::Request->new(POST => "https://services.reachmail.net/REST/Content/Mailings/v1/$account_id");
$create_mail_url->content("$mail_properties_xml");
$create_mail_url->authorization_basic("$account_key\\$username", "$password");
$create_mail_request = $ua->request($create_mail_url);

print "\n".$create_mail_request->content."\n\n"; 
