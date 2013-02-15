#!/usr/bin/perl

use RMAPI;

sub create_list {
	my @vs = @_;
	my $acct_id = $vs[3];
	my $entity_body = $vs[4];
        my $service_uri = "/Contacts/v1/lists/" . $acct_id;
        my $method = 'POST';
	
	my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml
}

sub get_list {
	my @vs = @_;
        my $acct_id = $vs[3];
	my $list_id = $vs[4];
        my $service_uri = "/Contacts/v1/lists/" . $acct_id . "/" . $list_id;
	my $method = 'GET';

	my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub modify_list {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
	my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/" . $acct_id . "/" . $list_id;
        my $method = 'PUT';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

	return $xml;
}

sub delete_list {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
        my $service_uri = "/Contacts/v1/lists/" . $acct_id . "/" . $list_id;
        my $method = 'DELETE';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

	return $xml;
}

sub export_recipients {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
	my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/export/" . $acct_id . "/" . $list_id;
	my $method = 'POST';

	my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_export_status {
	my @vs = @_;
        my $acct_id = $vs[3];
	my $export_id = $vs[4];
        my $service_uri = "/Contacts/v1/lists/export/status/" . $acct_id . "/" . $export_id;
        my $method = 'GET';

	my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

	return $xml;
}

sub enumerate_fields {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $service_uri = "/Contacts/v1/lists/fields/" . $acct_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub enumerate_groups {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $service_uri = "/Contacts/v1/lists/groups/" . $acct_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub create_group {
	my @vs = @_;
        my $acct_id = $vs[3];
	my $entity_body = $vs[4];
        my $service_uri = "/Contacts/v1/lists/groups/" . $acct_id;
        my $method = 'POST';

	my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_group {
	my @vs = @_;
        my $acct_id = $vs[3];
	my $group_id = $vs[4];
        my $service_uri = "/Contacts/v1/lists/groups/" . $acct_id . "/" . $group_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub modify_group {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $group_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/groups/" . $acct_id . "/" . $group_id;
        my $method = 'PUT';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub delete_group {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $group_id = $vs[4];
        my $service_uri = "/Contacts/v1/lists/groups/" . $acct_id . "/" . $group_id;
        my $method = 'DELETE';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub import_recipients {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
	my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/import/" . $acct_id . "/" . $list_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_import_status {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $import_id = $vs[4];
        my $service_uri = "/Contacts/v1/lists/import/status/" . $acct_id . "/" . $import_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub opt_in_recipient_from_list {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/optin/" . $acct_id . "/" . $list_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub opt_out_recipient_from_list {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/optout/" . $acct_id . "/" . $list_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub enumerate_lists {
	my @vs = @_;
        my $acct_id = $vs[3];
	my $entity_body = $vs[4];
        my $service_uri = "/Contacts/v1/lists/query/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_recipient {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
	my $email_addr = $vs[5];
        my $service_uri = "/Contacts/v1/lists/recipients/" . $acct_id . "/" . $list_id . "/" . $email_addr;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub modify_recipient {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
	my $email_addr = $vs[5];
        my $entity_body = $vs[6];
	my $service_uri = "/Contacts/v1/lists/recipients/" . $acct_id . "/" . $list_id . "/" . $email_addr;
        my $method = 'PUT';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub delete_recipient {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
	my $email_addr = $vs[5];
	my $service_uri = "/Contacts/v1/lists/recipients/" . $acct_id . "/" . $list_id . "/" . $email_addr;
        my $method = 'DELETE';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub create_recipient {
        my @vs = @_;
        my $acct_id = $vs[3];
	my $list_id = $vs[4];
        my $entity_body = $vs[5];
	my $service_uri = "/Contacts/v1/lists/recipients/" . $acct_id . "/" . $list_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_individual_recipient_by_query {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/recipients/find/" . $acct_id . "/" . $list_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub delete_individual_recipient_by_query {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/recipients/find/delete/" . $acct_id . "/" . $list_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub modify_individual_recipient_by_query {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/recipients/find/modify/" . $acct_id . "/" . $list_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub enumerate_recipients {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/recipients/query/" . $acct_id . "/" . $list_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub delete_batch_recipients_by_query {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/recipients/query/delete/" . $acct_id . "/" . $list_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub modify_batch_recipients_by_query {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/recipients/query/modify/" . $acct_id . "/" . $list_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub modify_or_create_individual_recipient_by_query {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $list_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Contacts/v1/lists/recipients/subscribe/" . $acct_id . "/" . $list_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub opt_in_recipient_from_account {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Contacts/v1/optin/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub opt_out_recipient_from_account {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Contacts/v1/optout/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}
1;

