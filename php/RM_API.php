<?php
/*
Name: ReachMail API Library
Description: Provides a variety of functions to interact with and extend the ReachMail API.
Author: ReachMail Support
Version: 0.1.0
Author URI: http://www.reachmail.net/support
License: MIT License (see LICENSE) http://creativecommons.org/licenses/MIT/
Last Modified: April 5, 2011
Requirements: PHP 5 or higher.
*/

//Ensure Curl Installed
	if (!extension_loaded("curl")) {
		throw(new Exception("The Curl extension for PHP is required for ReachMail API to work."));
	}
/* The login class is a requirement to access ALL the ReachMail API services. This class holds the tokens for use in creating new instances and accessing the associated service methods. It is called thus:
$login = new login('ACME','admin','1234ABC'); */	
	class login{
	       private $account_key;
	       private $username;
	       private $password;		   
		
		function __construct($account_key = NULL, $username = NULL, $password = NULL) {
					$this->account_key = $account_key;
					$this->username = $username;
					$this->password = $password;		
		}	
		
		function getUser() {		
					$get_user_url =  'https://services.reachmail.net/Rest/Administration/v1/users/current';
					$account_id_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $get_user_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->account_key\\$this->username:$this->password",
							CURLOPT_RETURNTRANSFER => true
					);
					curl_setopt_array($account_id_request, $curl_options);
					$response = curl_exec($account_id_request);
					$xml = simplexml_load_string($response);
					$account_id = $xml->AccountId;   
					print "\n".$account_id."\n\n";					
					echo $account_id->saveXML("user.xml");
		}
		
		function queueMail($account_id, $request_body) {
					$queue_mailing_url = 'https://services.reachmail.net/Rest/Campaigns/v1/';		
					$api_service_url = $queue_mailing_url.$account_id."/queue";
					$header = array("Content-Type: application/xml");		
					$queue_mailing_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->account_key\\$this->username:$this->password",
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
		
		function enumerateLists($account_id, $request_body) {		
					$enumerate_lists_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/query/';
					$api_service_url = $enumerate_lists_url.$account_id;
					$header = array("Content-Type: application/xml");
					$enumerate_lists_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->account_key\\$this->username:$this->password",
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
						print $list_api_ids[$i]."\t:\t".$list_names[$i]."\n";
					}
					print "\n";
					echo $list_xml->saveXML("lists.xml");
		}
		
		function createList($account_id, $request_body){	
					$create_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';
					$api_service_url = $create_list_url.$account_id;
					$header = array("Content-Type: application/xml");					
					$create_list_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->account_key\\$this->username:$this->password",
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
		
		function enumerateMailings($account_id, $request_body) {			
					$enumerate_mailings_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/query/';
					$api_service_url = $enumerate_mailings_url.$account_id;										
					$enumerate_mailings_request = curl_init();
					$header = array("Content-Type: application/xml");
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->account_key\\$this->username:$this->password",
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
						print $mail_names[$i]." : ".$mail_ids[$i]." : ".$created[$i]."\n";
					}
					print "\n";
					echo $mail_xml ->saveXML("mailings.xml");
		}			
		
		function enumerateRecipients($account_id, $list_id, $request_body) {			
					$enumerate_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/query/';
					$api_service_url = $enumerate_recipients_url.$account_id.'/'.$list_id;
					$header = array("Content-Type: application/xml");		
					$enumerate_recipients_request = curl_init();
					$curl_options = array(
							CURLOPT_URL => $api_service_url,
							CURLOPT_HEADER => false,
							CURLOPT_USERPWD => "$this->account_key\\$this->username:$this->password",
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
						echo $email_addresses[$i]."\n";
						$i++;
					}
					print "\n";
					echo $response_xml->saveXML("recipients.xml");
		}
}	
?>