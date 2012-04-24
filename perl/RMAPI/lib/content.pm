#!/usr/bin/perl

use RMAPI;

sub add_file {
	my @vs = @_;
        my $acct_id = $vs[3];
	my $data_id = $vs[4];
        my $entity_body = $vs[5];
        my $service_uri = "/Content/Library/files/" . $acct_id . "/" . $data_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_file {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $file_id = $vs[4];
        my $service_uri = "/Content/Library/files/" . $acct_id . "/" . $file_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub modify_file {
	my @vs = @_;
        my $acct_id = $vs[3];
        my $file_id = $vs[4];
	my $entity_body = $vs[5];
        my $service_uri = "/Content/Library/files/" . $acct_id . "/" . $file_id;
        my $method = 'PUT';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub delete_file {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $file_id = $vs[4];
        my $service_uri = "/Content/Library/files/" . $acct_id . "/" . $file_id;
        my $method = 'DELETE';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub enumerate_files {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Content/Library/files/query/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub create_folder {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Content/Library/folders/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

sub get_folder {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $folder_id = $vs[4];
        my $service_uri = "/Content/Library/folders/" . $acct_id . "/" . $folder_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub modify_folder {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $folder_id = $vs[4];
	my $entity_body = $vs[5];
        my $service_uri = "/Content/Library/folders/" . $acct_id . "/" . $folder_id;
        my $method = 'PUT';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}


sub delete_folder {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $folder_id = $vs[4];
        my $service_uri = "/Content/Library/folders/" . $acct_id . "/" . $folder_id;
        my $method = 'DELETE';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml;
}

sub enumerate_folders {
        my @vs = @_;
        my $acct_id = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Content/Library/folders/query/" . $acct_id;
        my $method = 'POST';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $entity_body);

        return $xml;
}

1;
