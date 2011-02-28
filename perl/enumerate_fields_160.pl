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
$ua = LWP::UserAgent->new();
#---- set the request URL and authentication, run the request using user agent
$fields_url = HTTP::Request->new(GET => "https://services.reachmail.net/REST/Contacts/v1/lists/fields/$account_id");
$fields_url->authorization_basic("$account_key\\$username", "$password");
$fields_request = $ua->request($fields_url);

#---- retrieve the content and intialize a XML parser on it ----
$fields_xml = $fields_request->content;
$parser = XML::LibXML->new();
$fields_dom = $parser->parse_string($fields_xml);

#---- for the purposes of this example we'll print the name and description of
#---- all possible fields ----
$fields_results = $fields_dom->findnodes('Fields');
foreach $fields_context ($fields_results->get_nodelist){
	@field_names = $fields_context->findnodes('Field/Name');
	@field_descriptions = $fields_context->findnodes('Field/Description');
}

$field_count = @field_names;

#---- print results ----
print "\nFormat: API name / Description\n";
for ($i=0; $i<$field_count; $i++){
	print @field_names[$i]->to_literal()." / ".@field_descriptions[$i]->to_literal()."\n";
}
print "\n";
