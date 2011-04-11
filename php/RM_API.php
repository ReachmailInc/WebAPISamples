<?php
/*
Name: ReachMail API Library
Description: Provides a variety of functions to interact with and extend the ReachMail API.
Author URI: http://www.reachmail.net/support
Version: 0.1
Requirements: PHP 5 or higher.
*/

  //Ensure Curl Installed
	if (!extension_loaded("curl")) {
		throw(new Exception("The cURL extension for PHP is required for ReachMail API to work."));
	}
/* 
RM_API class is required to access ALL of the ReachMail API services. 
This class holds the tokens for use in creating new objects.
$object = new RM_API('ACME','admin','1234ABC'); 
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
/*
Get User returns your accounts API ID. This ID is a requirement for 
most other services. Response is the account_id in both the standard 
output and as user.xml.
$getUser = new RM_API('ACME','admin','1234ABC');
$getUser->rm_getUser();
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
/*
Queue Mailing to schedule and send a mailing. Both the mail_id 
and list_id, as well as all other mailing properties are required 
to be formatted in xml as the $request_body as deliniated here, 
https://services.reachmail.net/sdk/. Response is the queue_id 
in both the standard output and as queueId.xml.
$queueMail = new RM_API('ACME','admin','1234ABC');
$queueMail->rm_queueMail($account_id, $request_body);
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
/*
Enumerate Lists gives the list_id and other requested list properties 
of all list that meet the $request_body request requirements. The 
$request_body is submitted in xml format as deliniated here, 
https://services.reachmail.net/sdk/. Response is in both the standard 
output and as lists.xml.
$enumerateLists = new RM_API('ACME','admin','1234ABC');
$enumerateLists->rm_enumerateLists($account_id, $request_body);
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
/*
Create List sets up an empty list with the fields formatted in 
the $request_body. The $request_body is submitted in xml format 
as deliniated here, https://services.reachmail.net/sdk/. Response 
is the new list_id in both the standard output and as listId.xml.
$createList = new RM_API('ACME','admin','1234ABC');
$createList->rm_createList($account_id, $request_body);
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
/*
Upload Data prepares a file for import into a list and is used with 
Import Recipients. The $file must be a comma seperated list. Returns 
the data_id to the standard output which is required for importing 
into a list.
$uploadData = new RM_API('ACME','admin','1234ABC');
$uploadData->rm_uploadData($file);
*/		
		function rm_uploadData($file) {	
					$upload_data_url = 'https://services.reachmail.net/Rest/Data/';
					$header = array("Content-Type: application/xml");
					$fp = fopen($file,'r');
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
		}
/*
Import Recipients places the uploaded data into the list itself.
The list_id to import into is required. As well, thedata_id from 
the Upload Data is required in xml format as deliniated here, https://services.reachmail.net/sdk/. Returns an import_id to the 
standard output.
$importRecipients = new RM_API('ACME','admin','1234ABC');
$importRecipients->rm_importRecipients($account_id, $list_id, $request_body);
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
/*
Enumerate Recipients returns all records in a list as requested 
in the $request_body. The $request_body must be formatted in xml
as delineated here, https://services.reachmail.net/sdk/. Returned
both in the standard output and as recipients.xml.
$enumerateRecipients = new RM_API('ACME','admin','1234ABC');
$enumerateRecipients->rm_enumerateRecipients($account_id, $list_id, $request_body);
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
/*
Create Recipient adds a record to a list. The fields and their 
values are set up in the $request_body xml, which is delineated
here, https://services.reachmail.net/sdk/.
$createRecipients = new RM_API('ACME','admin','1234ABC');
$createRecipients->rm_createRecipients($account_id, $list_id, $request_body); 
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
/*
Export Recipients will export a specified list for subsequent download.
The $request_body will vary depending on the fields in your lists, the 
required xml is deliniated here,  https://services.reachmail.net/sdk/.
Response is the export_id required for download in the standard output.
$exportRecipients = new RM_API('ACME','admin','1234ABC');
$exportRecipients->rm_exportRecipients($account_id, $list_id, $request_body);
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
/*
Download Data is used with Export Recipients and downloads a file you have 
exported. It requires the export_id and returns the records in the standard
output and as "list.xml".
$downloadData = new RM_API('ACME','admin','1234ABC');
$downloadData->rm_downloadData($export_id);
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
/*
Enumerate Mailings gives the mail_id and other requested mail properties 
of all mailings that meet the $request_body request requirements. The 
$request_body is submitted in xml format as deliniated here, 
https://services.reachmail.net/sdk/. Response is in both the standard 
output and as mailings.xml.
$enumerateMailings = new RM_API('ACME','admin','1234ABC');
$enumerateMailings->rm_enumerateMailings($account_id, $request_body);
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
/*
Create Mail (HTML/Text) creates a multi-part mailing in the account. The 
$request_body is submitted in xml format as deliniated here, 
https://services.reachmail.net/sdk/. Be sure to use "<![CDATA[HTML]]>". 
Response in the standard output returns the new mailings Id.
$createMail = new RM_API('ACME','admin','1234ABC');
$createMail->rm_createMail($account_id, $request_body);
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
	}	
?>