#!/usr/bin/perl

use RMAPI;

sub get_info {
	my @vs = @_;
        my $service_uri = "/Runtime";
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}


1;
