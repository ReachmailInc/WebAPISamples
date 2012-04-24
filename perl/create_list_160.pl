#!/usr/pkg/bin/perl
#---- The API CreateList service makes use of special field names. Please
#---- review the EnumerateFields service for an explanation of how to
#---- retrieve the fields that are available for list use. ----

#---- include Perl Modules ----
use XML::LibXML;
use LWP::UserAgent;
use HTTP::Request::Common qw(POST);

#---- set the account information variables. If you're unsure what these are
#---- contact your ReachMail account admin or email support@reachmail.com 
$account_key = 'account-id';
$username = 'username';
$password = 'password';

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

#---- at this point we'll set the XML for the entity body that will be sent
#---- to the CreateLists url. this XML is what defines the properties of the
#---- list. in other tutorials we've set the entity body XML directly in the
#---- $url->content('SOMEXML'); statement but for longer entities such as 
#---- this it can be helpful to create the XML elsewhere ----
$create_list_xml = '<ListProperties><Fields><FieldNames><Field>Email</Field><Field>FullName</Field><Field>Zip</Field></FieldNames></Fields><Name>20100820mso_bisk_suppression</Name></ListProperties>';

#---- set URL and authentication for CreateList, make the request ----
$create_list_url = HTTP::Request->new(POST => "https://services.reachmail.net/REST/Contacts/v1/lists/$account_id");
$create_list_url->content("$create_list_xml");
$create_list_url->authorization_basic("$account_key\\$username", "$password");
$create_list_request = $ua->request($create_list_url);

#---- retrieve the request response and intitialize a parser on it. in this
#---- case the response will be the unique API id of the list that was 
#---- just created. ----
$new_list_xml = $create_list_request->content;
$parser = XML::LibXML->new();
$new_list_dom = $parser->parse_string($new_list_xml);

#---- print the response, for this example we'll print out the API id of the 
#---- list that was just created. You'll need this unique id to add list
#---- members via the API. ----
$create_list_results = $new_list_dom->findnodes('List/Id');
print "\nNew list id: ".$create_list_results->to_literal()."\n\n";
