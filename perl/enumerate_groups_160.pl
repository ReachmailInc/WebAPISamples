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
$ua->default_header('Content-Type', "application/xml");

#---- set the URL request and authentication, request the content ----
$group_url = HTTP::Request->new(GET => "https://services.reachmail.net/Rest/Contacts/v1/lists/groups/$account_id");
$group_url->authorization_basic("$account_key\\$username", "$password");
$group_request = $ua->request($group_url);

#---- retrieve the request content and intializae a parser on it ----
$group_xml = $group_request->content;
$parser = XML::LibXML->new();
$group_dom = $parser->parse_string($group_xml);

#---- the following is an example of how to process the XML content that will
#---- returned from a successful request. in the example we take the group
#---- names and ids, send the to arrays and print them out

#---- scan the XML for group information and set the contents to arrays
$group_results = $group_dom->findnodes('Groups');
foreach $group_context ($group_results->get_nodelist){
	@group_names = $group_context->findnodes('Group/Name');
	@group_ids = $group_context->findnodes('Group/Id');
}

$group_count = @group_names;

#---- print out the group names and ids ----
print "\nFormat: group_name / group_id\n";
for ($i=0; $i<$group_count; $i++){
	print @group_names[$i]->to_literal()." / ".@group_ids[$i]->to_literal()."\n";
}
print "\n";
