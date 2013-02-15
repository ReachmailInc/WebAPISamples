#!/usr/pkg/bin/perl
#---- include Perl Modules ----
use XML::LibXML;
use LWP::UserAgent;

#---- set the account information variables. If you're unsure what these are
#---- contact your ReachMail account admin or email support@reachmail.com 
$account_key = 'account-id';
$username = 'username';
$password = 'password';

#---- set the and content type ----
$ua = LWP::UserAgent->new;
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

#--- output ----
print "\nCongradulations! You have authenticated account $account_key\n";
print "Your API account id is: $account_id\n\n";
