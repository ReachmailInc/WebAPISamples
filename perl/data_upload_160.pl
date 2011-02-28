#!/usr/pkg/bin/perl
use strict;
use warnings;
#---- include Perl Modules ----
use XML::LibXML;
use LWP::UserAgent;
use HTTP::Request::Common qw(POST);

#---- set the account information variables. If you're unsure what these are
#---- contact your ReachMail account admin or email support@reachmail.com 
my $account_key = 'account-id';
my $username = 'username';
my $password = 'password';

#---- set the user agent and content type ----
my $ua = LWP::UserAgent->new;
$ua->default_header('Content-Type' => "application/xml");
my $import_url = HTTP::Request->new(POST => "https://services.reachmail.net/Rest/Data/");
open FILE, "<list.txt";
my $DATA = do { local $/; <FILE>};
$import_url->content($DATA);
$import_url->authorization_basic("$account_key\\$username", "$password");
my $import_request = $ua->request($import_url);

print "\n".$import_request->content."\n\n";
