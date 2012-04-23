#!/usr/bin/perl
package RMAPI;

use 5.8.8;
use strict;
use warnings;
use administration;
use contact;
use campaign;
use content;
use data;
use mailing;
use report;
use runtime;


require Exporter;

our @ISA = qw(Exporter);

# Items to export into callers namespace by default. Note: do not export
# names by default without a very good reason. Use EXPORT_OK instead.
# Do not simply export all your public functions/methods/constants.

# This allows declaration	use RMAPI ':all';
# If you do not need this, moving things directly into @EXPORT or @EXPORT_OK
# will save memory.
our %EXPORT_TAGS = ( 'all' => [ qw(
	
) ] );

our @EXPORT_OK = ( @{ $EXPORT_TAGS{'all'} } );

our @EXPORT = qw(
	
);

our $VERSION = '0.01';


# Preloaded methods go here.

sub call
{
        use LWP::UserAgent;
	use HTTP::Request::Common;
        use Carp;
        my @vs = @_;
	my $base_uri = 'https://services.reachmail.net/Rest';

	my $method = $vs[0];
	my $uri = $base_uri . $vs[1];
	my $entity_body = $vs[5];

	my $ua = LWP::UserAgent->new;
        $ua->default_header("Content-Type" => "application/xml");
        my $url = HTTP::Request->new($method => "$uri");
        if (($method eq 'POST') || ($method eq 'PUT')){
		$url->content($entity_body);
	}
	$url->authorization_basic("$vs[2]\\$vs[3]", "$vs[4]");
	my $request = $ua->request($url);
        my $xml = $request->content;

	if ($xml !~ m@^HTTP/\d+\.\d+\s+200\s@) {return $xml;} else {return "call failed to return 200 OK response";}

}

# End modules
1;
__END__
# Below is stub documentation for your module. You'd better edit it!

=head1 NAME

RMAPI - Perl extension for blah blah blah

=head1 SYNOPSIS

  use RMAPI;

	$result = RMAPI::get_current_user('account_key', 'username', 'password');
	$result = RMAPI::enumerat_addresses('account_key', 'username', password', 'account id');
	$result = RMAPI::create_list('account_key', 'username', password', 'account id', 'entity body');

	
=head1 DESCRIPTION

A simple perl module that acts as a wrapper for Reachmail API services.
=head2 EXPORT

None by default.



=head1 SEE ALSO

None

=head1 AUTHOR

Mike Marshall 

=head1 COPYRIGHT AND LICENSE

Copyright (C) 2012 by Dan Nielsen

This library is free software; you can redistribute it and/or modify
it under the same terms as Perl itself, either Perl version 5.8.8 or,
at your option, any later version of Perl 5 you may have available.


=cut

