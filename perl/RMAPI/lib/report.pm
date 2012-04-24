#!/usr/bin/perl

use RMAPI;

sub get_bounce_detail_report {
	my @vs = @_;
        my $acct_id = $vs[3];
	my $mailing_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Reports/v1/details/mailings/bounces/" . $acct_id . "/" . $mailing_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_opt_out_detail_report {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $mailing_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Reports/v1/details/mailings/optouts/" . $acct_id . "/" . $mailing_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_read_detail_report {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $mailing_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Reports/v1/details/mailings/reads/" . $acct_id . "/" . $mailing_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_tracked_link_detail_report {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $mailing_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Reports/v1/details/mailings/trackedLinks/" . $acct_id . "/" . $mailing_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_mailing_report {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $mailing_id = $vs[4];
        my $service_uri = "/Reports/v1/mailings/" . $acct_id . "/" . $mailing_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub enumerate_mailing_reports {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Reports/v1/mailings/query/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_tracked_link_report_by_mailing_id {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $mailing_id = $vs[4];
        my $service_uri = "/Reports/v1/mailings/trackedLinks/" . $acct_id . "/" . $mailing_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub get_tracked_link_report_by_mailing_list_id {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $mailing_id = $vs[4];
	my $mailing_list_is = $vs[5];
        my $service_uri = "/Reports/v1/mailings/trackedLinks/" . $acct_id . "/" . $mailing_id . "/" . $mailing_list_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

1;
