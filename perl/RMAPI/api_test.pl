#!usr/bin/perl

use XML::LibXML;
use RMAPI;

$acct_key = 'YOUR ACCOUNT KEY GOES HERE';
$user = 'USERNAME GOES HERE';
$pass = 'superSecretPASSWORD';

my $result = RMAPI::get_current_user ($acct_key, $user, $pass);

print $result . "\n\n" . 'next step...'. "\n\n";

$parser = XML::LibXML->new();
$dom = $parser->parse_string($result);

$results = $dom->findnodes('User');
foreach $context ($results->get_nodelist) {
	$acct_id = $context->findnodes('AccountId');
}

print 'account-id is: ' . $acct_id . "\n\n";

# this test function will use the RMAPI perl module.  An exaple of running one of the test functions is as follows:

#&test_create_foler;  

#which run the create_folder service call.  You will have to ensure the XML (entity body) to fit your needs but this should provide a usefule jumping off point for testing purposes.

#The code above these comment lines will query for an display your account id  while the code below are prebuilt functions for testing purposes.  




sub test_enumerate_mailing_templates {
	$x = RMAPI::enumerate_mailing_templates($acct_key, $user, $pass, $acct_id, '<MailingTemplateFilter><MaxResults>5</MaxResults></MailingTemplateFilter>');
	print $x;
}


sub test_create_folder {
	my $x = "<FolderProperties><Name>testFolder</Name></FolderProperties>";
	my $res = RMAPI::create_folder($acct_key, $user, $pass, $acct_id, $x);
	print $res;
}

sub test_modify_folder {
        my $x = "<FolderProperties><Name>NEW FOLDER NAME TEST</Name></FolderProperties>";
        my $res = RMAPI::modify_folder($acct_key, $user, $pass, $acct_id, $folder_id, $x);
        print $res;
}

sub test_enumerate_folders {
	my $x = "<FolderFilter><MaxResults>15</MaxResults></FolderFilter>";
	my $f_resp = RMAPI::enumerate_folders($acct_key, $user, $pass, $acct_id, $x);
	print $f_resp;
}


sub test_upload {
	my $data_id = RMAPI::upload($acct_key, $user, $pass, 'path to local file');
	print "\n\ndata id = :" .  $data_id;
}

sub test_enumerate_addresses {
	my $addresses = RMAPI::enumerate_addresses ($acct_key, $user, $pass, $acct_id);
	print $addresses;
}

sub test_create_list {
	my $list_xml = '<ListProperties><Fields><Field>email</Field><Field>FullName</Field></Fields><Name>New list name</Name></ListProperties>';

	my $list_result = RMAPI::create_list ($acct_key, $user, $pass, $acct_id, $list_xml);
	print $list_result . "\n\n";

	$list_id_result = $dom->findnodes('List');
	foreach $context ($results->get_nodelist) {
        	$acct_id = $context->findnodes('Id');
	}

	print "list id is: " . $list_id_result . "\n";
}

sub test_get_list {
	print "attempting get_list now...\n\n";
	$get_list_result = RMAPI::get_list ($acct_key, $user, $pass, $acct_id, $list_id);
	print "response is: \n" . $get_list_result . "\n\n";
}

sub test_delete_list {
	print "attempting delete list now...\n\n";
	$delete_list = RMAPI::delete_list ($acct_key, $user, $pass, $acct_id, $list_id);
	print "response is: \n" . $delete_list . "\n\n";
}

sub test_modify_list {
	my $mod_list_xml = "<ListProperties><Name>New Edited List Name</Name></ListProperties>";
	print "attempting modify list now...\n\n";
	$mod_list = RMAPI::modify_list ($acct_key, $user, $pass, $acct_id, $list_id, $mod_list_xml);
	print "response is: \n" . $mod_list . "\n\n";
}
