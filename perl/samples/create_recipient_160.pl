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
$account_id = 'api-account-id';
$listid = 'list-id';

#---- set some special variables for this example only
$email = 'faker@reachmail.com';
$name = 'Frank Aker';

#---- set the user agent and content type ----
$ua = LWP::UserAgent->new;
$ua->agent("$0/0.1 " . $ua->agent);
$ua->default_header('Content-Type' => "application/xml");

#---- at this point we'll create the XML that will be sent as the entity body
#---- to the CreateRecipients url. With longer bodies such as this it can be
#---- helpful to define them outside of the $url->content('SOMEXML'); line.
#---- at minimum you'll need to send an email within the RecipientProperties
#---- noed. this example shows how to use the Properties node to include 
#---- additional subscriber information.
$create_recip_xml = "<RecipientProperties><Email>$email</Email><Properties><Property><Name>FullName</Name><Value>$name</Value></Property></Properties></RecipientProperties>";

#---- set URL and authentication for CreateRecipient, make the request ----
$create_recip_url = HTTP::Request->new(POST => "https://services.reachmail.net/REST/Contacts/v1/lists/recipients/$account_id/$listid");
$create_recip_url->content("$create_recip_xml");
$create_recip_url->authorization_basic("$account_key\\$username", "$password");
$create_recip_request = $ua->request($create_recip_url);

#---- print the request response
if ($create_recip_request->content == ""){
	print "\nSuccessfully added $email to $listid\n\n";
} else {
	print "\n".$create_recip_request->content."\n\n";
}
