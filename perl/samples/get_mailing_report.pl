#!/usr/pkg/bin/perl
#---- include Perl Modules. note that HTTP::Request::Common qw(GET) is
#---- included as an example but is only necessary if the API service requires
#---- a HTTP GET
use XML::LibXML;
use LWP::UserAgent;
use HTTP::Request::Common qw(GET);

#---- set the account information variables. If you're unsure what these are
#---- contact your ReachMail account admin or email support@reachmail.com.
#---- the account_id and mailing_id variables need to be retrieved using API
#---- services, please refer to services
#---- AdministrationService\GetCurrentUser and EnumerateMailingReports respectively
$account_key = 'ACCOUNTID';
$username = 'user';
$password = 'SuperSecretPassword';
$api_service_url = 'https://services.reachmail.net/Rest/Reports/v1/mailings/';
$account_id = 'API-acct-id';
$mailing_id = 'API-mailing-id';

#---- set the user agent and content type ----
$ua = LWP::UserAgent->new;
$ua->agent("$0/0.1 " . $ua->agent);
$ua->default_header('Content-Type' => "application/xml");

#---- set URL and authentication for lists, make the request ----
$report_url = HTTP::Request->new(GET => "$api_service_url$account_id/$mailing_id");
$report_url->authorization_basic("$account_key\\$username", "$password");
$report_request = $ua->request($report_url);

#---- retrive the content and initialize a XML parser on them
$xml = $report_request->content;
$parser = XML::LibXML->new();
$dom = $parser->parse_string($xml);

#---- scan to the 'AccountId' and 'Mailing ID' nodes (and so on) and retrieve their content ----

$acct_id = $dom->findnodes('MailingReport/AccountId');
$mail_id = $dom->findnodes('MailingReport/MailingId');
$msg_content = $dom->findnodes('MailingReport/Message/ContentHtml');
$name = $dom->findnodes('MailingReport/Message/Name');
$create_date = $dom->findnodes('MailingReport/Created');


#--- output ----

print "\n\nYour API account id is: $acct_id\n\n";
print "Mailing ID is: $mail_id\n\n";
print "Mailing Name is: $name\n\n";
print "Date Created: $create_date\n\n";
print "Message Content: \n $msg_content \n\n";