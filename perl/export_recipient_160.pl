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
$listid = 'api-list-id'; 

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
$export_recipient_xml = "
<ExportParameters>
<ExportOptions>
<Format>CharacterSeperated</Format>
<HeaderRow>true</HeaderRow>
<CharacterSeperatedData>
 <Delimiter>Comma</Delimiter>
</CharacterSeperatedData>
<FieldMapping>
<FieldMapping>
<DestinationFieldName>Email</DestinationFieldName>
<SourceFieldName>Email</SourceFieldName>
</FieldMapping>
<FieldMapping>
<DestinationFieldName>Name</DestinationFieldName>
<SourceFieldName>FullName</SourceFieldName>
</FieldMapping>
</FieldMapping>
</ExportOptions>
</ExportParameters>
";

#---- set URL and authentication for CreateRecipient, make the request ----
$export_recip_url = HTTP::Request->new(POST => "https://services.reachmail.net/Rest/Contacts/v1/lists/export/$account_id/$listid");
$export_recip_url->content("$export_recipient_xml");
$export_recip_url->authorization_basic("$account_key\\$username", "$password");
$export_recip_request = $ua->request($export_recip_url);

#---- print the request response
#print "\n".$export_recip_request->content."\n\n";

$xml = $export_recip_request->content;
$parser = XML::LibXML->new();
$dom = $parser->parse_string($xml);

$results = $dom->findnodes('Export');
foreach $context($results->get_nodelist){
	$file_id = $context->findnodes('Id');

}

#print "\n".$file_id."\n\n";

$download_url = HTTP::Request->new(GET => "https://services.reachmail.net/Rest/Data/$file_id");
$download_url->authorization_basic("$account_key\\$username", "$password");
$download_url_request = $ua->request($download_url);

print $download_url_request->content;
