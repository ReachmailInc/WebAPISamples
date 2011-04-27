<?php
/*
Name: ReachMail API Library
Description: Provides a variety of functions to interact with and extend the ReachMail API.
Author: tsolyan@reachmail.com | http://www.reachmail.net/support
Version: 0.1
Requirements: PHP 5 or higher.
*/

  //Ensure Curl Installed
	if (!extension_loaded("curl")) {
		throw(new Exception("The cURL extension for PHP is required for ReachMail API to work."));
	}
/** 
 * RM_API class is required to access ALL of the ReachMail API services.
 *
 * This class holds the tokens for use in creating new objects.
 * $object = new RM_API('ACME','admin','1234ABC');
 *  
 * @param string $_account_key The ReachMail login account key.
 * @param string $_username  The accounts login username.
 * @param srring &_password  The accounts login password.
 *
 * Noting is actually returned to user, but, the results are used to login for each instance of objects made with individual services.
*/	
	class RM_API{
	       private $_account_key;
	       private $_username;
	       private $_password;	   
		
		function __construct($_account_key = NULL, $_username = NULL, $_password = NULL) {
					$this->_account_key = $_account_key;
					$this->_username = $_username;
					$this->_password = $_password;				
		}	
/**
 * Get User returns your accounts API ID which is a requirement for most other services.
 *
 * $getUser = new RM_API('ACME','admin','1234ABC'); 
 * $getUser->rm_getUser();
 * 
 * @return string The account_id in both the standard output and as user.xml. 
*/		
		function rm_getUser() {		
					$get_user_url =  'https://services.reachmail.net/Rest/Administration/v1/users/current';
					$account_id_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $get_user_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($account_id_request, $curl_options);
					$response = curl_exec($account_id_request);
					$xml = simplexml_load_string($response);
					$account_id = $xml->AccountId;   
					print "\n" . $account_id . "\n\n";					
					echo $account_id->saveXML("user.xml");
		}
/**
 * Queue Mailing to schedule and send a mailing. 
 *
 * $queueMail = new RM_API('ACME','admin','1234ABC');
 * $queueMail->rm_queueMail($account_id, $request_body);
 *
 * @param string $account_id  The account_id returned from the Get User service.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 * 
 * @return string The queue_id in both the standard output and as queueId.xml.
*/		
		function rm_queueMail($account_id, $request_body) {
					$queue_mailing_url = 'https://services.reachmail.net/Rest/Campaigns/v1/';		
					$api_service_url = $queue_mailing_url . $account_id . "/queue";
					$header = array("Content-Type: application/xml");		
					$queue_mailing_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($queue_mailing_request, $curl_options);
					$queue_mailing_response = curl_exec($queue_mailing_request);
					curl_close($queue_mailing_request);
					$mail_xml = simplexml_load_string($queue_mailing_response);
					print_r($mail_xml);
					echo $mail_xml->saveXML("queueId.xml");
		}
/**
 * Enumerate Fields returns a list of ALL available list fields for the account.
 * 
 * $enumerateFields = new RM_API('ACME','admin','1234ABC');
 * $enumerateFields->rm_enumerateFields($account_id);
 *
 * @param string $account_id The account_id returned from the Get User service.
 *
 * @return string A list of all of the specific accounts fields (different accounts can have different fields) in both the standard output and as fields.xml.
*/		
		function rm_enumerateFields($account_id) {		
					$enumerate_fields_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/fields/';
					$api_service_url = $enumerate_fields_url . $account_id;
					$header = array("Content-Type: application/xml");
					$enumerate_fields_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($enumerate_fields_request, $curl_options);
					$enumerate_fields_response = curl_exec($enumerate_fields_request);
					curl_close($enumerate_fields_request);
					$field_xml = simplexml_load_string($enumerate_fields_response);
					$field_names = array();
					$field_descriptions = array();
					foreach($field_xml->Field as $field){
						$field_names[] = $field->Name;
						$field_descriptions[] = $field->Description;
					}
					$field_count = count($field_names);
					print "\nFormat - Field Name : Field Description\n";
					for($i=0; $i<$field_count; $i++){
						print $field_names[$i]." : ".$field_descriptions[$i]."\n";
					}
					print "\n";
					echo $field_xml->saveXML("fields.xml");
		}
/**
 * Enumerate Lists gives the list_id and other requested list properties of list that meet the request requirements. 
 * 
 * $enumerateLists = new RM_API('ACME','admin','1234ABC');
 * $enumerateLists->rm_enumerateLists($account_id, $request_body);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 *
 * @return string A list of list-id's and other requested data about enumerated lists in both standard output and as lists.xml.
*/		
		function rm_enumerateLists($account_id, $request_body) {		
					$enumerate_lists_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/query/';
					$api_service_url = $enumerate_lists_url . $account_id;
					$header = array("Content-Type: application/xml");
					$enumerate_lists_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
							);
					curl_setopt_array($enumerate_lists_request, $curl_options);
					$enumerate_lists_response = curl_exec($enumerate_lists_request);
					curl_close($enumerate_lists_request);
					$list_xml = simplexml_load_string($enumerate_lists_response);
					$list_names = array();
					$list_api_ids = array();
					foreach($list_xml->List as $list){
							$list_names[] = $list->Name;
							$list_api_ids[] = $list->Id;
					}
					$list_count = count($list_api_ids);
					print "\nFormat - List API Id\t:\tList Name\n";
					for($i=0; $i<$list_count; $i++){
						print $list_api_ids[$i] . "\t:\t" . $list_names[$i] . "\n";
					}
					print "\n";
					echo $list_xml->saveXML("lists.xml");
		}
/**
 * Enumerate List Groups returns the group_id's, names, and other properties of all list groups in the account.
 * 
 * $listGroups = new RM_API('ACME','admin','1234ABC');
 * $listGroups->rm_listGroups($account_id);
 *
 * @param string $account_id The account_id returned from the Get User service.
 *
 * @return string returns the group_id's, names, and other properties of all list groups in the account.
*/
		function rm_enumerateListGroups($account_id) {
					$list_groups_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/groups/';    
					$api_service_url = $list_groups_url . $account_id;
					$header = array("Content-Type: application/xml");
					$list_groups_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_RETURNTRANSFER => true
							);
					curl_setopt_array($list_groups_request, $curl_options);
					$list_groups_response = curl_exec($list_groups_request);
					curl_close($list_groups_request);
					$list_groups_xml = simplexml_load_string($list_groups_response);
					print_r($list_groups_response);
					echo $list_groups_xml->saveXML("listGroups.xml");
		}
/**
 * Create List sets up an empty list with the fields formatted in the $request_body. 
 *
 * $createList = new RM_API('ACME','admin','1234ABC');
 * $createList->rm_createList($account_id, $request_body);
 * 
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 *
 * @return string The new list_id in both the standard output and as listId.xml.
*/		
		function rm_createList($account_id, $request_body){	
					$create_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';
					$api_service_url = $create_list_url . $account_id;
					$header = array("Content-Type: application/xml");					
					$create_list_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($create_list_request, $curl_options);
					$create_list_response = curl_exec($create_list_request);
					curl_close($create_list_request);
					$response_xml = simplexml_load_string($create_list_response);
					$list_api_id = $response_xml->Id;
					print "\nSuccessfully created list! (ID: $list_api_id)\n\n";
					echo $list_api_id->saveXML("listId.xml");
		}
/**
 * Get List is used to assure list is active and available for use.
 *
 * $getList = new RM_API('ACME','admin','1234ABC');
 * $getList->rm_getList($account_id, $list_id);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $list_id The list Id for the list you are retrieving.
 * 
 * @return string Returns list properties (e.g. ID, name, number of active users, etc.) in both the standard output and as getList.xml.
*/		
		function rm_getList($account_id, $list_id) {
					$get_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';
					$api_service_url = $get_list_url . $account_id . "/" . $list_id;
					$header = array("Content-Type: application/xml");
					$get_list_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_RETURNTRANSFER => true
							);
					curl_setopt_array($get_list_request, $curl_options);
					$response = curl_exec($get_list_request);
					curl_close($get_list_request);
					$xml = simplexml_load_string($response);
					print_r($response);
					echo $xml->saveXML("getList.xml");
	    }
/**
 * Modify List can add fields to an active list, as well as modify most other list properties.
 *
 * $modifyList = new RM_API('ACME','admin','1234ABC');
 * $modifyList->rm_modifyList($account_id, $list_id, $request_body);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $list_id The list Id for the list you are modifying.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 *
 * @return string Makes the requested changes in the specified list.
*/
		function rm_modifyList($account_id, $list_id, $request_body) {
					$modify_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';
					$api_service_url = $modify_list_url . $account_id . "/" . $list_id;
					$header = array("Content-Type: application/xml");
					$modify_list_request = curl_init();
					$putString = $request_body;
					$putData = tmpfile(); 
					fwrite($putData, $putString); 
					fseek($putData, 0);
					$length = strlen($putString);
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_PUT => true,
							CURLOPT_INFILE => $putData,
							CURLOPT_INFILESIZE => $length,
							CURLOPT_RETURNTRANSFER => true
							);
					curl_setopt_array($modify_list_request, $curl_options);
					$modify_list_response = curl_exec($modify_list_request);
					curl_close($modify_list_request);
					$xml = simplexml_load_string($modify_list_response);
					print_r($modify_list_response);			
	    }
/**
 * Upload Data uploads and prepares a file for Import Recipients into an active list.
 * 
 * $uploadData = new RM_API('ACME','admin','1234ABC');
 * $uploadData->rm_uploadData($file);
 *
 * @param string $file must be a path to a comma seperated list with open permissions.
 *
 * @return string The data_id to the standard output.
*/		
		function rm_uploadData($file) {	
					$upload_data_url = 'https://services.reachmail.net/Rest/Data/';
					$header = array("Content-Type: application/xml");
					$fp = file_get_contents($file);
					$request_body = $fp;
					$upload_file_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $upload_data_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_FOLLOWLOCATION => true,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
							);
					curl_setopt_array($upload_file_request, $curl_options);
					$upload_file_response = curl_exec($upload_file_request);
					curl_close($upload_file_request);
					$xml = simplexml_load_string($upload_file_response);
					$upload_id = $xml->Id;
					print "\nYour file has been successfully uploaded!\nYour upload id: $upload_id\n\n";
					echo $upload_id->saveXML("uploadId.xml");
		}
/**
 * Import Recipients places previously uploaded data into a specified active list.
 *
 * $importRecipients = new RM_API('ACME','admin','1234ABC');
 * $importRecipients->rm_importRecipients($account_id, $list_id, $request_body);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $list_id The list to which the uploaded data will be inported.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 *
 * @return string Adds Upload Data records into a specified list and returns the Import_id to the standard output.
*/		
		function rm_importRecipients($account_id, $list_id, $request_body) {	
					$import_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/import/';
					$api_service_url = $import_recipients_url . $account_id . '/' . $list_id;
					$header = array("Content-Type: application/xml");		
					$create_recipients_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($create_recipients_request, $curl_options);
					$create_recipients_response = curl_exec($create_recipients_request);
					curl_close($create_recipients_request);
					if($create_recipients_response == "1"){
							print "\nSuccessfully added $email to $list_id\n\n";
					} else {
							print_r($create_recipients_response);
					}
		}	
/**
 * List From File creates and populates a new list in your ReachMail account.
 *
 * Both the $request_body xml strings need to be set up as per your lists specific needs: https://services.reachmail.net/sdk/.
 * $listFromFile = RM_API('ACME','admin','1234ABC');
 * $listFromFile->rm_listFromFile($account_id, $file);
 *
 * @param string $account_id  The account_id returned from the Get User service.
 * @param string $file Must be a path to a comma seperated list with open permissions.
 *
 * @return string Sets up a new populated list in the account and returns an import_id to the standard output.
*/		
		function rm_listFromFile($account_id, $file) {
					$request_body = "<ListProperties><Fields><Field>Email</Field><Field>FullName</Field></Fields><Name>API-TestList</Name></ListProperties>";
					$create_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';
					$api_service_url = $create_list_url . $account_id;
					$header = array("Content-Type: application/xml");					
					$create_list_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($create_list_request, $curl_options);
					$create_list_response = curl_exec($create_list_request);
					curl_close($create_list_request);
					$response_xml = simplexml_load_string($create_list_response);
					$list_id  = $response_xml->Id;								
					$uploadFile = new RM_API($this->_account_key, $this->_username, $this->_password);
					$uploadFile->rm_uploadData($file);
					$data_id = simplexml_load_string(file_get_contents('uploadId.xml'));				
					$request_body = "<Parameters><DataId>$data_id</DataId><FieldMappings><FieldMapping><DestinationFieldName>Email</DestinationFieldName><SourceFieldPosition>1</SourceFieldPosition></FieldMapping><FieldMapping><DestinationFieldName>FullName</DestinationFieldName><SourceFieldPosition>2</SourceFieldPosition></FieldMapping></FieldMappings><ImportOptions><CharacterSeperatedOptions><Delimiter>Comma</Delimiter></CharacterSeperatedOptions><Format>CharacterSeperated</Format></ImportOptions></Parameters>";
					$importRecipients = new RM_API($this->_account_key, $this->_username, $this->_password);
					$importRecipients->rm_importRecipients($account_id, $list_id, $request_body);
	    }
/**	
 * Add Records Via Import, imports an uploaded files records into an active list in the account.
 *
 * The $request_body xml string need to be set up as per your lists specific needs: https://services.reachmail.net/sdk/.
 * $addViaImport = RM_API('ACME','admin','1234ABC');
 * $addViaImport->rm_addViaImport($account_id, $list_id, $file);
 *
 * @param string $account_id  The account_id returned from the Get User service.
 * @param string $list_id The list to which the uploaded data will be inported.
 * @param string $file Must be a path to a comma seperated list with open permissions.
 *
 * @return string Uploads and adds records into an active list and returns the import_Id.
*/	
		function rm_addViaImport($account_id, $list_id, $file) {	
					$upload_data_url = 'https://services.reachmail.net/Rest/Data/';
					$header = array("Content-Type: application/xml");
					$fp = file_get_contents($file);
					$request_body = $fp;
					$upload_file_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $upload_data_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_FOLLOWLOCATION => true,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
							);
					curl_setopt_array($upload_file_request, $curl_options);
					$upload_file_response = curl_exec($upload_file_request);
					curl_close($upload_file_request);
					$xml = simplexml_load_string($upload_file_response);
					$data_id = $xml->Id;
					$request_body = "<Parameters><DataId>$data_id</DataId><FieldMappings><FieldMapping><DestinationFieldName>Email</DestinationFieldName><SourceFieldPosition>1</SourceFieldPosition></FieldMapping><FieldMapping><DestinationFieldName>FullName</DestinationFieldName><SourceFieldPosition>2</SourceFieldPosition></FieldMapping></FieldMappings><ImportOptions><CharacterSeperatedOptions><Delimiter>Comma</Delimiter></CharacterSeperatedOptions><Format>CharacterSeperated</Format></ImportOptions></Parameters>";
					print "\nYour file has been successfully uploaded!\nYour upload id: $data_id\n\n";					
					$importRecipients = new RM_API($this->_account_key, $this->_username, $this->_password);
					$importRecipients->rm_importRecipients($account_id, $list_id, $request_body);
		}
/**
 * Enumerate Recipients returns all records in a list that meet the request parameters in the request_body.
 * 
 * $enumerateRecipients = new RM_API('ACME','admin','1234ABC');
 * $enumerateRecipients->rm_enumerateRecipients($account_id, $list_id, $request_body);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $list_id The list which is being enumerated.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 *
 * @return string Returns the requested records in the standard output and as recipients.xml.
*/		
		function rm_enumerateRecipients($account_id, $list_id, $request_body) {			
					$enumerate_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/query/';
					$api_service_url = $enumerate_recipients_url . $account_id . '/' . $list_id;
					$header = array("Content-Type: application/xml");		
					$enumerate_recipients_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($enumerate_recipients_request, $curl_options);
					$enumerate_recipients_response = curl_exec($enumerate_recipients_request);
					curl_close($enumerate_recipients_request);
					$response_xml = simplexml_load_string($enumerate_recipients_response);
					$i = 0;
					print "\n";
					foreach($response_xml->Recipient as $recipients){
						$email_addresses[] = $recipients->Email;
						echo $email_addresses[$i] . "\n";
						$i++;
					}
					print "\n";
					echo $response_xml->saveXML("recipients.xml");
		}
/**
 * Create Recipient adds a record to an active list.
 *
 * $createRecipients = new RM_API('ACME','admin','1234ABC');
 * $createRecipients->rm_createRecipients($account_id, $list_id, $request_body);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $list_id The list to which the new record is being added.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 *
 * @return string Returns the added records values and the list_id to the standard output.
*/		
		function rm_createRecipient($account_id, $list_id, $request_body) {	
					$create_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/';
					$api_service_url = $create_recipients_url . $account_id . '/' . $list_id;
					$header = array("Content-Type: application/xml");
					$create_recipients_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($create_recipients_request, $curl_options);
					$create_recipients_response = curl_exec($create_recipients_request);
					curl_close($create_recipients_request);
					if($create_recipients_response == "1"){
						print "\nSuccessfully added $email to $list_id\n\n";
					} else {
						print_r($create_recipients_response);
					}
		}	
/**
 * Modify Recipients changes properties of a record in an active list.
 *
 * $modifyRecipients = new RM_API('ACME','admin','1234ABC');
 * $modifyRecipients->rm_modifyRecipients($account_id, $list_id, $email, $request_body);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $list_id The list which is having a recipient modified.
 * @param string $email The email address of the record being modified.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 *
 * @return string The selected record in an active list will be modified.
 */
		function rm_modifyRecipient($account_id, $list_id, $email, $request_body) {
					$modify_recipient_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/';
					$api_service_url = $modify_recipient_url . $account_id . "/" . $list_id . "/" . $email;
					$header = array("Content-Type: application/xml");
					$modify_recipient_request = curl_init();
					$putString = $request_body;
					$putData = tmpfile(); 
					fwrite($putData, $putString); 
					fseek($putData, 0);
					$length = strlen($putString);
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_PUT => true,
							CURLOPT_INFILE => $putData,
							CURLOPT_INFILESIZE => $length,
							CURLOPT_RETURNTRANSFER => true
							);
					curl_setopt_array($modify_recipient_request, $curl_options);
					$modify_recipient_response = curl_exec($modify_recipient_request);
					curl_close($modify_recipient_request);
					$xml = simplexml_load_string($modify_recipient_response);
					$return = print_r($modify_recipient_response);				
		}	
/**
 * Export Recipients will export a specified list for subsequent download.
 *
 * $exportRecipients = new RM_API('ACME','admin','1234ABC');
 * $exportRecipients->rm_exportRecipients($account_id, $list_id, $request_body);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $list_id The list which is to be exported.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 *
 * @return string The export_id required for download in the standard output.
*/	
		function rm_exportRecipients($account_id, $list_id, $request_body) {	
					$export_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/export/' ;
					$api_service_url = $export_recipients_url . $account_id . '/' . $list_id;
					$header = array("Content-Type: application/xml");	
					$export_recipients_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
							);
					curl_setopt_array($export_recipients_request, $curl_options);
					$export_recipients_response = curl_exec($export_recipients_request);
					curl_close($export_recipients_request);
					if($export_recipients_response == "1"){
							print "\nSuccessfully exported listID" . $list_id . "\n\n";
					} else {
							print_r($export_recipients_response);
					}
		}
/**
 * Download Data is used with Export Recipients and downloads a file you have exported.
 *
 * $downloadData = new RM_API('ACME','admin','1234ABC');
 * $downloadData->rm_downloadData($export_id);
 *
 * @param string $export_id From Export Recipients service.
 *
 * @return string The exported records in the standard output and as list.xml.
*/
		function rm_downloadData($export_id) {
					$download_data_url = 'https://services.reachmail.net/Rest/Data/';
					$api_service_url = $download_data_url . $export_id;
					$header = array("Content-Type: application/xml");
					$download_data_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_RETURNTRANSFER => true
							);
					curl_setopt_array($download_data_request, $curl_options);
					$response = curl_exec($download_data_request);
					curl_close($download_data_request);
					$list_xml = simplexml_load_string($response);
					print_r($response);
					echo $list_xml->saveXML("list.xml");
		}
/**
 * Export List will export and download a specified list from an account.
 *
 * $exportList = new RM_API('ACME','admin','1234ABC');
 * $exportList->rm_exportList($account_id, $list_id, $request_body);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $list_id The list which is to be exported.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 *
 * @return string The exported records in the standard output and as list.xml.
*/			
		function rm_exportList($account_id, $list_id, $request_body) {	
					$export_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/export/' ;
					$api_service_url = $export_recipients_url . $account_id . '/' . $list_id;
					$header = array("Content-Type: application/xml");	
					$export_recipients_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
							);
					curl_setopt_array($export_recipients_request, $curl_options);
					$export_recipients_response = curl_exec($export_recipients_request);
					curl_close($export_recipients_request);
					$xml = simplexml_load_string($export_recipients_response);
					$export_id = $xml->Id;
					$downloadData = new RM_API($this->_account_key, $this->_username,$this->_password);
					$downloadData->rm_downloadData($export_id);
					
		}		
/**
 * Enumerate Mailings gives the mail_id and other requested mail properties of all mailings that meet the request requirements in the request_body.
 *
 * $enumerateMailings = new RM_API('ACME','admin','1234ABC');
 * $enumerateMailings->rm_enumerateMailings($account_id, $request_body);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 *
 * @return string Lists the mail_id's and other requested data in both standard output and as mailings.xml.
*/		
		function rm_enumerateMailings($account_id, $request_body) {			
					$enumerate_mailings_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/query/';
					$api_service_url = $enumerate_mailings_url . $account_id;										
					$enumerate_mailings_request = curl_init();
					$header = array("Content-Type: application/xml");
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($enumerate_mailings_request, $curl_options);
					$enumerate_mailings_response = curl_exec($enumerate_mailings_request);
					curl_close($enumerate_mailings_request);
					$mail_xml = simplexml_load_string($enumerate_mailings_response);
					$created = array();
					$mail_names = array();
					$mail_ids = array();
					foreach($mail_xml->Mailing as $mailing){
						$created[] = $mailing->Created;
						$mail_names[] = $mailing->Name;
						$mail_ids[] = $mailing->Id;
					}
					$mail_count = count($mail_ids);
					print "\nFormat - Mail Name : Mail ID : Create Date\n";
					for($i=0; $i<$mail_count; $i++){
						print $mail_names[$i] . " : " . $mail_ids[$i] . " : " . $created[$i] . "\n";
					}
					print "\n";
					echo $mail_xml ->saveXML("mailings.xml");
		}
/**
 * Create Mail (HTML/Text) creates a multi-part mailing in the account.
 *
 * The mailings HTML and Text content as the $request_body, use <![CDATA[HTML Goes Here]]>.
 * $createMail = new RM_API('ACME','admin','1234ABC');
 * $createMail->rm_createMail($account_id, $request_body);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 *
 * @return string Creates a new mailing in the accou nt and returns it's mailing_id to the standard output.
*/
		function rm_createMail($account_id, $request_body) {
					$create_mail_url = 'https://services.reachmail.net/REST/Content/Mailings/v1/';
					$api_service_url = $create_mail_url . $account_id;
					$header = array("Content-Type: application/xml");
					$create_mail_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($create_mail_request, $curl_options);
					$create_mail_response = curl_exec($create_mail_request);
					curl_close($create_mail_request);
					$xml = simplexml_load_string($create_mail_response);
					$mail_id = $xml->Id;
					print_r($xml);
	        }		
/**
 * Enumerate Mailing Reports returns all the mailing reports that meet the requested parameters in the request_body.
 *
 * $enumerateMailingReports = new RM_API('ACME','admin','1234ABC');
 * $enumerateMailingReports->rm_enumerateMailingReports($account_id, $request_body);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $request_body In xml and containg parameters delineated here https://services.reachmail.net/sdk/.
 *
 * @return string Returns the sumarized mailing reports meeting specified parameters.
*/		
		function rm_enumerateMailingReports($account_id, $request_body) {	
					$enumerate_mailings_report_url = 'https://services.reachmail.net/Rest/Reports/v1/mailings/query/';
					$api_service_url = $enumerate_mailings_report_url . $account_id;
					$header = array("Content-Type: application/xml");		
					$enumerate_mailings_report_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_POST => true,
							CURLOPT_POSTFIELDS => $request_body,
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($enumerate_mailings_report_request, $curl_options);
					$enumerate_mailings_report_response = curl_exec($enumerate_mailings_report_request);
					curl_close($enumerate_mailings_report_request);
					$mail_report_xml = simplexml_load_string($enumerate_mailings_report_response);
					$delivered = array();
					$mail_names = array();
					$mail_ids = array();
					foreach($mail_report_xml->Mailing as $mailing){
						$delivered[] = $mailing->DeliveredDate;
						$mail_names[] = $mailing->Name;
						$mail_ids[] = $mailing->Id;
					}
					$mail_count = count($mail_ids);
					print "\nFormat - Mail Name : Mail ID : Delivered Date\n";
					for($i=0; $i<$mail_count; $i++){
						print $mail_names[$i] . " : " . $mail_ids[$i] . " : " . $delivered[$i] . "\n";
					}
					print "\n";
					echo $mail_report_xml ->saveXML("reports.xml");
		}
/**
 * Get Mailing Report Summary returns a summary of a specific mailing report.
 *
 * $mailingReportSummary = new RM_API('ACME','admin','1234ABC');
 * $mailingReportSummary->rm_getMailingSummary($account_id, $mailing_id);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $mailing_id The id of the mailin summarized in the report summary.
 *
 * @return string Returns report summary in the standard output and as summary.xml.
*/		
		function rm_getMailingSummary($account_id, $mailing_id) {	
					$mailing_summary_url = 'https://services.reachmail.net/Rest/Reporting/Content/Mailings/v1/Summary/';		
					$api_service_url = $mailing_summary_url . $account_id . "/" . $mailing_id;
					$header = array("Content-Type: application/xml");
					$mail_summary_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($mail_summary_request, $curl_options);
					$mail_summary_response = curl_exec($mail_summary_request);
					curl_close($mail_summary_request);
					$mail_summary_xml = simplexml_load_string($mail_summary_response);
					print_r($mail_summary_xml);
					echo $mail_summary_xml->saveXML("summary.xml");
		}
/**
 * Get Mailing Report returns a detailed mailing report.
 *
 * $mailingReport= new RM_API('ACME','admin','1234ABC');
 * $mailingReport->rm_getMailingReport($account_id, $mailing_id);
 *
 * @param string $account_id The account_id returned from the Get User service.
 * @param string $mailing_id The id of the mailin to generate the report.
 *
 * @return string Returns report in the standard output and as an xslt styledReport.xml.
*/		
		function rm_getMailingReport($account_id, $mailing_id) {	
					$mailing_report_url = 'https://services.reachmail.net/Rest/Reports/v1/mailings/';
					$api_service_url = $mailing_report_url . $account_id . "/" . $mailing_id;
					$header = array("Content-Type: application/xml");
					$mail_report_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
							CURLOPT_HTTPHEADER => $header,
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($mail_report_request, $curl_options);
					$mail_report_response = curl_exec($mail_report_request);
					curl_close($mail_report_request);
					$mail_report_xml = simplexml_load_string($mail_report_response);
					print_r($mail_report_xml);
					$my_file = 'styledReport.xml';
					$handle = fopen($my_file, 'w') or die('Cannot open file:  ' . $my_file);
					$data = '<?xml version="1.0"?><?xml-stylesheet type="text/xsl" href="styles.xsl"?>';
					fwrite($handle, $data);
					fclose($handle);
					$my_file = 'styledReport.xml';
					$handle = fopen($my_file, 'a') or die('Cannot open file:  ' . $my_file);
					$data = $mail_report_response;
					fwrite($handle, $data);
					fclose($handle);
		}
	}	
?>