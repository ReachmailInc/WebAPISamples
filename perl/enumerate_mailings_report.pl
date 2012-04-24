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
$account_key = 'account_id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Reports/v1/mailings/query/';
$account_id = 'api_account_id';

#---- set the user agent and content type ----
$ua = LWP::UserAgent->new;
$ua->agent("$0/0.1 " . $ua->agent);
$ua->default_header('Content-Type' => "application/xml");

#---- set URL and authentication for lists, make the request ----
$report_url = HTTP::Request->new(POST => "$api_service_url$account_id");
$report_url->content('<MailingReportFilter><ScheduledDeliveryOnOrAfter>2011-10-01</ScheduledDeliveryOnOrAfter></MailingReportFilter>');
$report_url->authorization_basic("$account_key\\$username", "$password");
$report_request = $ua->request($report_url);

#---- retrieve the content of the request and intialize a XML parser on it ----
$report_xml = $report_request->content;
$parser = XML::LibXML->new();
$report_dom = $parser->parse_string($report_xml);

$mailings = $report_dom->findnodes('MailingReports');
foreach $mailing ($mailings->get_nodelist){
	@mail_id = $mailing->findnodes('MailingReport/MailingId');
}

$mail_count = @mail_id;

for ($i=0; $i<$mail_count; $i++){
	print @mail_id[$i]->to_literal()."\n";
}
