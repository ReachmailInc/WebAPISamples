<?php
/**
 * Add Records Via Import, creates and populates a list in your ReachMail account.
 *
 * Both the $request_body xml strings need to be set up as per your lists specific needs.
 *
 * @param string $account_key The ReachMail login account key.
 * @param string $username    The accounts login username.
 * @param srring &password    The accounts login password.
 * @param string $account_id  The account_id returned from the Get User service.
 * @param string $file Must be a path to a comma seperated list with open permissions.
 *
 * @return string Sets up a populated list in the account and returnd an importID.
*/
	$account_key = 'account_key';
	$username = 'user_name';
	$password = 'password';
	$account_id = 'account_id';
	$file = '/path_to_file.csv';
		
	function rm_addRecordsViaImport($account_key, $username, $password, $account_id, $file) {
				include('RM_API.php');
				$request_body = "<ListProperties><Fields><Field>Email</Field><Field>FullName</Field></Fields><Name>API-TestList</Name></ListProperties>";
				$createList = new RM_API($account_key, $username, $password);
				$createList->rm_createList($account_id, $request_body);
				$list_id = simplexml_load_string(file_get_contents('listId.xml'));								
				$uploadFile = new RM_API($account_key, $username, $password);
				$uploadFile->rm_uploadData($file);
				$data_id = simplexml_load_string(file_get_contents('uploadId.xml'));				
				$request_body = "<Parameters><DataId>$data_id</DataId><FieldMappings><FieldMapping><DestinationFieldName>Email</DestinationFieldName><SourceFieldPosition>1</SourceFieldPosition></FieldMapping><FieldMapping><DestinationFieldName>FullName</DestinationFieldName><SourceFieldPosition>2</SourceFieldPosition></FieldMapping></FieldMappings><ImportOptions><CharacterSeperatedOptions><Delimiter>Comma</Delimiter></CharacterSeperatedOptions><Format>CharacterSeperated</Format></ImportOptions></Parameters>";
				$importRecipients = new RM_API($account_key, $username, $password);
				$importRecipients->rm_importRecipients($account_id, $list_id, $request_body);
}		
	rm_addRecordsViaImport($account_key, $username, $password, $account_id, $file);
?>