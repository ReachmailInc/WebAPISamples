#!/usr/bin/perl

use RMAPI;

sub get_current_user {
        my @vs = @_;
        my $service_uri = "/Administration/v1/users/current";
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub enumerate_addresses {
        my @vs = @_;
        my $method = 'GET';
        my $acct_id = $vs[3];
        my $service_uri = '/Administration/v1/addresses/' . $acct_id;

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

1;

