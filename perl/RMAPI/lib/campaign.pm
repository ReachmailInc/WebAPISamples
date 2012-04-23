#!/usr/bin/perl

use RMAPI;

sub queue_mailing {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Campaigns/v1/" . $acct_id . "/queue";
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml
}

1;
