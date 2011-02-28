#!/usr/pkg/bin/perl
#---- The API service CreateRecipient requires that you have an API list id
#---- handy. Please refer to the CreateList or EnumerateLists demo for 
#---- information on creating new lists or accessing exisiting lists.

#---- include Perl Modules ----
use XML::LibXML;
use LWP::UserAgent;
use HTTP::Request::Common qw(POST);

#---- set the account information variables. If you're unsure what these are
#---- contact your ReachMail account admin or email support@reachmail.com 
$account_key = 'account-id';
$username = 'username';
$password = 'password';

#---- set some special variables for this example only
$listid = 'api-list-id';
$data_id = 'api-data-id';

#---- set the user agent and content type ----
$ua = LWP::UserAgent->new;
$ua->agent("$0/0.1 " . $ua->agent);
$ua->default_header('Content-Type' => "application/xml");

#---- set request URL and authentication, run the request using user agent ----
$url = HTTP::Request->new(GET => 'https://services.reachmail.net/Rest/Administration/v1/users/current');
$url->authorization_basic("$account_key\\$username", "$password");
$request = $ua->request($url);

#---- retrive the content and initialize a XML parser on them
$xml = $request->content;
$parser = XML::LibXML->new();
$dom = $parser->parse_string($xml);

#---- scan to the 'AccountId' node and retrieve the content ----
$results = $dom->findnodes('User');
foreach $context ($results->get_nodelist){
	$account_id = $context->findnodes('AccountId');
}

$import_data = "<Parameters><DataId>$data_id</DataId><FieldMappings><FieldMapping><DestinationFieldName>Email</DestinationFieldName><SourceFieldPosition>1</SourceFieldPosition></FieldMapping></FieldMappings><ImportOptions><CharacterSeperatedOptions><Delimiter>Comma</Delimiter></CharacterSeperatedOptions><Format>CharacterSeperated</Format></ImportOptions></Parameters>";
$import_url = HTTP::Request->new(POST => "https://services.reachmail.net/REST/Contacts/v1/lists/import/$account_id/$listid");
$import_url->content("$import_data");
$import_url->authorization_basic("$account_key\\$username", "$password");
$import_request = $ua->request($import_url);

print "\n".$import_request->content."\n\n";
