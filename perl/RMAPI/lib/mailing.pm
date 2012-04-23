#!/usr/bin/perl

use RMAPI;

sub create_mailing {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Content/Mailings/v1/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_mailing {
	my @vs = @_;
        my $acct_id = $vs[3];
	my $mailing_id = $vs[4];
        my $service_uri = "/Content/Mailings/v1/" . $acct_id . "/" . $mailing_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub modify_mailing {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $mailing_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Content/Mailings/v1/" . $acct_id . "/" . $mailing_id;
        my $method = 'PUT';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub delete_mailing {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $mailing_id = $vs[4];
        my $service_uri = "/Content/Mailings/v1/" . $acct_id . "/" . $mailing_id;
        my $method = 'DELETE';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub enumerate_mailing_groups {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $service_uri = "/Content/Mailings/v1/groups/" . $acct_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub create_mailing_group {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Content/Mailings/v1/groups/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_mailing_group {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $group_id = $vs[4];
        my $service_uri = "/Content/Mailings/v1/groups/" . $acct_id . "/" . $group_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub modify_mailing_group {
	my @vs = @_;
        my $acct_id = $vs[3];
	my $group_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Content/Mailings/v1/groups/" . $acct_id . "/" . $group_id;
        my $method = 'PUT';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub delete_mailing_group {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $group_id = $vs[4];
        my $service_uri = "/Content/Mailings/v1/groups/" . $acct_id . "/" . $group_id;
        my $method = 'DELETE';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub enumerate_mailings {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Content/Mailings/v1/query/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub create_mailing_template {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Content/Mailings/v1/templates/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_mailing_template {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $template_id = $vs[4];
        my $service_uri = "/Content/Mailings/v1/templates/" . $acct_id . "/" . $template_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub modify_mailing_template {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $template_id = $vs[4];
	my $entity_body = $vs[5];
        my $service_uri = "/Content/Mailings/v1/templates/" . $acct_id . "/" . $template_id;
        my $method = 'PUT';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);
}

sub delete_mailing_template {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $template_id = $vs[4];
        my $service_uri = "/Content/Mailings/v1/templates/" . $acct_id . "/" . $template_id;
        my $method = 'DELETE';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub enumerate_template_groups {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $service_uri = "/Content/Mailings/v1/templates/groups/" . $acct_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub create_template_group {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Content/Mailings/v1/templates/groups/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

	return $xml;
}

sub get_template_group {
	my @vs = @_;
        my $acct_id = $vs[3];
	my $group_id = $vs[4];
        my $service_uri = "/Content/Mailings/v1/templates/groups/" . $acct_id . "/" . $group_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub modify_template_group {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $group_id = $vs[4];
	my $entity_body = $vs[5];
        my $service_uri = "/Content/Mailings/v1/templates/groups/" . $acct_id . "/" . $group_id;
        my $method = 'PUT';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub delete_template_group {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $group_id = $vs[4];
        my $service_uri = "/Content/Mailings/v1/templates/groups/" . $acct_id . "/" . $group_id;
        my $method = 'DELETE';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub enumerate_mailing_templates {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Content/Mailings/v1/templates/query/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

1;
