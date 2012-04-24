#!/usr/bin/perl

use RMAPI;

sub upload {
	my @vs = @_;
	my $path2File = $vs[3];
        my $entity_body = $vs[4];
        my $service_uri = "/Data";
        my $method = 'POST';

	open FILE, "<$path2File";
	my $DATA = do { local $/; <FILE>};

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2], $DATA);

        return $xml
}

sub download {
        my @vs = @_;
	my $data_id = $vs[3];
        my $service_uri = "/Data/" . $data_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml
}

sub download_file {
        my @vs = @_;
        my $data_id = $vs[3];
	my $filename = $vs[4];
        my $service_uri = "/Data/" . $data_id . "/" . $filename;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml
}

sub exists {
        my @vs = @_;
        my $data_id = $vs[3];
        my $service_uri = "/Data/exists/" . $data_id;
        my $method = 'GET';

        my $xml = RMAPI::call($method, $service_uri, $vs[0], $vs[1], $vs[2]);

        return $xml
}

1;

