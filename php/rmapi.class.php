<?php 
/*
Name: ReachMail API Library
Description: Provides a base file which you can use to interact with ReachMail.
Author: Dan Nielsen | support@reachmail.com 
Version: 1.0 
Requirements: PHP 5 or higher and the Curl Extension

This file holds all functions that we provide through our API. Usage is simple,
save this file to your site or application and include it whenever acces to
the ReachMail API is needed

include_once('rmapi.class.php'); 

This, of course, may vary depending on the actual filesystem location to
which you save rmapi.class.php

Please consult the notes above each function for more information on usage.
All functions return JSON data.
*/

/*
Ensure Curl is installed
*/

if(!extension_loaded("curl")) {
	throw(new Exception("The cURL extension for PHP is required."));
}

/**
RMAPI class is a required class that's needed to access all services.
Example usage:

$RMAPI = new RMAPI('ACME','admin','password');
Once this command has been added to your PHP script, 
you'll be able to use services like this:

$RMAPI->rm_functionName(requested parameters)	

* Note that any service requiring request parameters (any POST or PUT service)
will only accept those parameters in JSON format.

**/

class RMAPI{
	private $_account_key;
	private $_username;
	private $_password;
    private $_httpa;

	function __construct($_account_key = NULL, $_username = NULL, 
            $_password = NULL) {
		$this->_account_key = $_account_key;
		$this->_username = $_username;
		$this->_password = $_password;
        $this->_httpa = "$_account_key\\$_username:$_password";
        $this->svcbase = "https://services.reachmail.net";
	}

    function requestBase($uri, $request_body, $request_method) {
        
        $url = $this->svcbase . $uri;
        $curl_headers = array("Content-Type: application/json",
                "Accept: application/json");

        // Defaults
        $curl_defaults = array(
            CURLOPT_URL => $url,
            CURLOPT_HEADER => false,
            CURLOPT_USERPWD => "$this->_httpa",
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_HTTPHEADER => $curl_headers
        );
        $request = curl_init();

        switch($request_method) {
            
            case "GET":
                $curl_options = $curl_defaults;
                break;

            case "POST":
                $curl_options = $curl_defaults;
                $curl_options["CURLOPT_POSTFIELDS"] = $request_body;
                $curl_options["CURLOPT_POST"] = true;
                break;

            case "PUT":
                $curl_options = $curl_defaults;
                $curl_options["CURLOPT_POSTFIELDS"] = $request_body;
                $curl_options["CURLOPT_CUSTOMREQUEST"] = $method;
                break;

            case "DELETE":
                $curl_options = $curl_defaults;
                $curl_options["CURLOPT_POSTFIELDS"] = $request_body;
                $curl_options["CURLOPT_CUSTOMREQUEST"] = $method;
                break;
        }

        curl_setopt_array($request, $curl_options);
        $response = curl_exec($request);
        $http_status = curl_getinfo($request, CURLINFO_HTTP_CODE);
        curl_close($request);

        return array(
            "http_status" => $http_status,
            "service_response" => $response
        );
    }

/*Adminitration Service*/

// rm_administrationUsersCurrent()
function rm_administrationUsersCurrent() {
    
    // Required for nearly all other operations, this function 
    // returns the account id validated with your credentials
    $uri = "/administration/users/current";

    return $this->requestBase($uri, null, "GET"); 
}

/****** STOPPED working here - dn ******/

/**Enumerate Addresses Function

 * Enumerate Addresses will show all footer addresses that have been created in your account. 
 *
 * Sample: $RMAPI->rm_enumerateAddresses($account_id);	
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * 
**/
function rm_enumerateAddresses($account_id) {
	$enumerateAddy_url = 'https://services.reachmail.net/Rest/Administration/v1/addresses/';
	$api_url = $enumerateAddy_url . $account_id;
	$EnumerateAddresses = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		);
	curl_setopt_array($EnumerateAddresses, $curl_options);
	$response = curl_exec($EnumerateAddresses);
	$xml = simplexml_load_string($response);
	$Addresses = $xml->Address;

	curl_close ($EnumerateAddresses);
	print_r($Addresses);
	}

/*Campaign Services*/

/**Queue Mailing function
 	
 * Queue mailing can be used to schedule a mailing.
 *
 * Sample: $RMAPI->rm_queueMailing($account_id, $request_body);
 * @param string $request_body is an XML string that's required in order to set options.
 * Sample $request_body is as such:

 * <QueueParameters><ListIds><Id>list_id_from_api</Id></ListIds><MailingId>mailing_id_from_api</MailingId><Properties></Properties></QueueParameters>"
	

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser

**/

function rm_queueMailing($account_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$queue_mailing_url = 'https://services.reachmail.net/Rest/Campaigns/v1/';
	$api_url = $queue_mailing_url . $account_id . "/queue";
	$QueueMailing = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);

	curl_setopt_array($QueueMailing, $curl_options);
	$response = curl_exec($QueueMailing);
	curl_close($QueueMailing);

	$response_xml = simplexml_load_string($response);
	$QueueResponse = $response_xml;

	print_r($response_xml);

	}

/*Contact Service*/

/**Create List Function

 * rm_createList will create a list in your account with all the parameters requested by the $request_body param string 
 *
 * Sample: $RMAPI->rm_createList($account_id,$request_body);	
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $request_body sample:
  
 * <ListProperties><Fields><FieldNames><Field>Email</Field><Field>FullName</Field></FieldNames></Fields><Name>List_name</Name></ListProperties>

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * 
**/

function rm_createList($account_id, $request_body) {
	$create_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';
	$api_url = $create_list_url . $account_id;
	$header = array("Content-Type: application/xml");
	$createList = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);

	curl_setopt_array($createList, $curl_options);
	$response = curl_exec($createList);
	curl_close($createList);

	$response_xml = simplexml_load_string($response);
	$QueueResponse = $response_xml;
	$list_id = $response_xml ->Id;

	print_r($response_xml);
	
	}

/*Get List Function

 * rm_createList will create a list in your account with all the parameters requested by the $request_body param string 
 *
 * Sample: $RMAPI->rm_createList($account_id,$request_body);	
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $request_body sample:
  
 * <ListProperties><Fields><FieldNames><Field>Email</Field><Field>FullName</Field></FieldNames></Fields><Name>List_name</Name></ListProperties>

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * 
**/

function rm_getList($account_id, $list_id, $request_body) {
	$get_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';
	$api_url = $get_list_url . $account_id . "/" . $list_id;
	
	$header = array("Content-Type: application/xml");
	$GetList = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => false,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($GetList, $curl_options);
	$response = curl_exec($GetList);
	$xml = simplexml_load_string($response);
	curl_close ($GetList);
	print_r($xml);

	}

/* Modify List Function

 * rm_modifyList will modify list parameters (name, group id, etc)
 *
 * Sample: $RMAPI->rm_modifyList($account_id, $list_id, $request_body);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $list_id is the API list id returned from either Get List or Enumerate List functions
 * @param string $request_body sample:
  
 * <ListProperties><Name>New List Name</Name></ListProperties>

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * 
**/


function rm_modifyList($account_id, $list_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$modify_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';
	$api_url = $modify_list_url . $account_id . "/" . $list_id;
	$ModifyList = curl_init();
	$putString = $request_body;
	$putData = tmpfile();
	fwrite($putData, $putString);
	fseek($putData, 0);
	$length = strlen($putString);
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_INFILE => $putData,
		CURLOPT_INFILESIZE => $length,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_PUT => true
		);

	curl_setopt_array($ModifyList, $curl_options);
	$response = curl_exec($ModifyList);
	curl_close($ModifyList);

	$response_xml = simplexml_load_string($response);


	print_r($response);
	}

/* Delete List Function

 * rm_deleteList will delete a list according to the ID that you give it
 *
 * Sample: $RMAPI->rm_deleteList($account_id, $list_id);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $list_id is the API list id returned from either Get List or Enumerate List functions
 * 
**/

function rm_deleteList($account_id, $list_id) {
	$delete_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';
	$api_url = $delete_list_url . $account_id . "/" . $list_id;
	
	$header = array("Content-Type: application/xml");
	$DeleteList = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_CUSTOMREQUEST => "DELETE",
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($DeleteList, $curl_options);
	$response = curl_exec($DeleteList);
	$xml = simplexml_load_string($response);


	curl_close ($DeleteList);
	print_r($response);
	}

/* Export Recipients Function

 * rm_exportList will export the given list_id to your machine (name, group id, etc)
 *
 * Sample: $RMAPI->rm_modifyList($account_id, $list_id, $request_body);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $list_id is the API list id returned from either Get List or Enumerate List functions
 * @param string $request_body sample:
  
 * <<ExportParameters><ExportOptions><Format>CharacterSeperated</Format><HeaderRow>true</HeaderRow><CharacterSeperatedData><Delimiter>Comma</Delimiter></CharacterSeperatedData><FieldMapping><FieldMapping><DestinationFieldName>Email</DestinationFieldName><SourceFieldName>Email</SourceFieldName></FieldMapping></FieldMapping></ExportOptions></ExportParameters>

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * 
**/

function rm_exportRecipients($account_id, $list_id, $request_body) {
	$export_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/export/';
	$api_url = $export_recipients_url . $account_id . "/" . $list_id;
	$header = array("Content-Type: application/xml");
	$exportRecipients = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_POSTFIELDS => $request_body
		);
	curl_setopt_array($exportRecipients, $curl_options);
	$response = curl_exec($exportRecipients);
	$response_xml = simplexml_load_string($response);
	curl_close ($exportRecipients);
	print_r($response_xml);	
	
	}

/* Get Export Status Function

 * rm_getexportStatus needs the export ID returned from the previous function, this function will notify you if the export was successfull or not (name, group id, etc)
 *
 * Sample: $RMAPI->rm_getexportStatus($account_id,$export_id;
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $export_id is the API export ID given after you do the exportRecipients function
 * 
**/	

function rm_getexportStatus($account_id, $export_id) {
	$get_export_status_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/export/status/';
	$api_url = $get_export_status_url . $account_id . "/" . $export_id;
	$header = array("Content-Type: application/xml");
	$ExportStatus = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => false,
		);
	curl_setopt_array($ExportStatus, $curl_options);
	$response = curl_exec($ExportStatus);
	$xml = simplexml_load_string($response);
	curl_close ($ExportStatus);
	print_r($xml);
	}

/* Enumerate Fields Function

 * rm_enumerateFields  (name, group id, etc)
 *
 * Sample: $RMAPI->rm_enumerateFields($account_id);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 *
 *  Column fields that are currently active in your lists will be displayed. An example would be email, first_name, last_name etc. Basically any of the columns used to create your lists
 *
 * 
 * 
**/

function rm_enumerateFields($account_id) {
	$enumerate_fields_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/fields/';
	$api_url = $enumerate_fields_url . $account_id;
	$header = array("Content-Type: application/xml");
	$EnumerateFields = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => false,
		);
	curl_setopt_array($EnumerateFields, $curl_options);
	$response = curl_exec($EnumerateFields);
	$xml = simplexml_load_string($response);
	curl_close ($EnumerateFields);
	print_r($xml);

	}

/* Enumerate Groups Function

 * rm_enumerateGroups will show all List Groups by Group ID
 *
 * Sample: $RMAPI->rm_enumerateGroups($account_id);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * 
**/

function rm_enumerateGroups($account_id) {
	$enumerate_groups_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/groups/';
	$api_url = $enumerate_groups_url . $account_id;
	$EnumerateGroups = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => false,
		);
	curl_setopt_array($EnumerateGroups, $curl_options);
	$response = curl_exec($EnumerateGroups);
	$xml = simplexml_load_string($response);
	curl_close ($EnumerateGroups);
	print_r($xml);
	}

/* Create Group Function

 * rm_createGroup will create a group with the parameters givein in the $request_body variable
 *
 * Sample: $RMAPI->rm_createGroup($account_id, $request_body);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $request_body sample:
  
 * "<GroupProperties><Name>New Group Name</Name></GroupProperties>"

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * 
**/

function rm_createGroup($account_id, $request_body) {
	$create_group_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/groups/';
	$header = array("Content-Type: application/xml");
	$api_url = $create_group_url . $account_id;
	$createGroup = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);

	curl_setopt_array($createGroup, $curl_options);
	$response = curl_exec($createGroup);
	curl_close($createGroup);

	$response_xml = simplexml_load_string($response);
	$QueueResponse = $response_xml;
	$group_id = $response_xml ->Id;

	print_r($response_xml);
	}

/* Modify Group Function

 * rm_modifyGroup will modify a groups parameters specified in the request_body
 *
 * Sample: $RMAPI->rm_modifyGroup($account_id, $group_id, $request_body);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $group_id is the API group ID from enumerate Groups
 * @param string $request_body sample:
  
 * <ListProperties><Name>New List Name</Name></ListProperties>

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * 
**/

function rm_modifyGroup($account_id, $group_id, $request_body) {
	$modify_group_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/groups/';
	$header = array("Content-Type: application/xml");
	$api_url = $modify_group_url . $account_id . "/" . $group_id;
	$ModifyGroup = curl_init();
	$putString = $request_body;
	$putData = tmpfile();
	fwrite($putData, $putString);
	fseek($putData, 0);
	$length = strlen($putString);
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_INFILE => $putData,
		CURLOPT_INFILESIZE => $length,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_PUT => true
		);

	curl_setopt_array($ModifyGroup, $curl_options);
	$response = curl_exec($ModifyGroup);
	curl_close($ModifyGroup);
	$response_xml = simplexml_load_string($response);
	$return = print_r($response);
	
	}

/* Enumerate Lists Function

 * rm_enumerateLists will show all lists in your account according to what the request_body is. (name, group id, etc)
 *
 * Sample: $RMAPI->rm_enumerateLists($account_id, $request_body);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $list_id is the API list id returned from either Get List or Enumerate List functions
 * @param string $request_body sample:
  
 * <ListProperties><Name>New List Name</Name></ListProperties>

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * 
**/

function rm_enumerateLists($account_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$enumerate_lists = 'https://services.reachmail.net/Rest/Contacts/v1/lists/query/';
	$api_url = $enumerate_lists . $account_id;
	$EnumerateLists = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_POST => true
		);

	curl_setopt_array($EnumerateLists, $curl_options);
	$response = curl_exec($EnumerateLists);
	curl_close($EnumerateLists);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	}

/* Delete Group Function

 * rm_deleteGroup will delete the given group_id from your account
 *
 * Sample: $RMAPI->rm_deleteList($account_id, $group_id);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $group_id is the API group id returned from either get group or enumerate groups function
 * @param string $request_body sample:
  
 * <ListProperties><Name>New List Name</Name></ListProperties>

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * 
**/

function rm_deleteGroup($account_id, $group_id) {
	$delete_group_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/groups/';
	$api_url = $delete_group_url . $account_id . "/" . $group_id;
	$header = array("Content-Type: application/xml");
	$DeleteGroup = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_CUSTOMREQUEST => "DELETE",
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($DeleteGroup, $curl_options);
	$response = curl_exec($DeleteGroup);
	$xml = simplexml_load_string($response);
	curl_close ($DeleteGroup);
	print_r($response);

	}

/* Import Recipient Function

 * rm_importrecipients will upload a list to a given list with the request body. You must use the upload service and use it's returned import ID to use this function.
 *
 * Sample: $RMAPI->rm_importRecipient($account_id, $list_id, $request_body);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $list_id is the API list id returned from either Get List or Enumerate List functions
 * @param string $request_body sample:
  
 * <ListProperties><Name>New List Name</Name></ListProperties>

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * 
**/

function rm_importRecipients($account_id,$list_id,$request_body) {
	$header = array("Content-Type: application/xml");
	$import_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/import/';
	$api_url = $import_recipients_url . $account_id . "/" . $list_id;
	$importRecipients = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);

	curl_setopt_array($importRecipients, $curl_options);
	$response = curl_exec($importRecipients);
	curl_close($importRecipients);
	
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	}

/* Get Import Status Function

 * rm_modifyList will modify list parameters (name, group id, etc)
 *
 * Sample: $RMAPI->rm_modifyList($account_id, $list_id, $request_body);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $list_id is the API list id returned from either Get List or Enumerate List functions
 * @param string $request_body sample:
  
 * <ListProperties><Name>New List Name</Name></ListProperties>

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * 
**/

function rm_getimportStatus($account_id, $import_id) {
	$header = array("Content-Type: application/xml");
	$import_status_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/import/status/';
	$api_url = $import_status_url . $account_id . "/" . $import_id;
	$getImportStatus = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		);

	curl_setopt_array($getImportStatus, $curl_options);
	$response = curl_exec($getImportStatus);
	curl_close($getImportStatus);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);

	}

/* Opt In Recipient From List Function

 * rm_modifyList will modify list parameters (name, group id, etc)
 *
 * Sample: $RMAPI->rm_modifyList($account_id, $list_id, $request_body);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $list_id is the API list id returned from either Get List or Enumerate List functions
 * @param string $request_body sample:
  
 * <ListProperties><Name>New List Name</Name></ListProperties>

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * 
**/

function rm_optinrecipientFromList($account_id, $list_id, $request_body) {
	$opt_in_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/optin/';
	$api_url = $opt_in_list_url . $account_id . "/" . $list_id;
	$header = array("Content-Type: application/xml");
	$optinRecipientFromList = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_FOLLOWLOCATION => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($optinRecipientFromList, $curl_options);
	$response = curl_exec($optinRecipientFromList);
	curl_close($optinRecipientFromList);
	$response_xml = simplexml_load_string($response);
	$optinResponse = $response_xml;
	print_r($response_xml);
	
	}

/* Opt Out Recipient From List

 * rm_modifyList will modify list parameters (name, group id, etc)
 *
 * Sample: $RMAPI->rm_modifyList($account_id, $list_id, $request_body);
 * @param string $account_id is the API account ID that you get as a result from running rm_getUser
 * @param string $list_id is the API list id returned from either Get List or Enumerate List functions
 * @param string $request_body sample:
  
 * <ListProperties><Name>New List Name</Name></ListProperties>

 * The request body options are all listed here. https://services.reachmail.net/sdk/
 * 
**/

function rm_optoutrecipientFromList($account_id, $list_id, $request_body) {
	$header = array("Content-Type: application/xml");	
	$opt_out_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/optout/';
	$api_url = $opt_out_list_url . $account_id . "/" . $list_id;
	$optinRecipientFromList = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_FOLLOWLOCATION => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);

	curl_setopt_array($optinRecipientFromList, $curl_options);
	$response = curl_exec($optinRecipientFromList);
	curl_close($optinRecipientFromList);

	$response_xml = simplexml_load_string($response);
	$optinResponse = $response_xml;

	print_r($response_xml);
	
	}

function rm_getRecipient($account_id, $list_id, $get_email) {
	$get_recipient_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/';
	$api_url = $get_recipient_url . $account_id . "/" . $list_id . "/" . $get_email;
	$header = array("Content-Type: application/xml");
	$getRecipient = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => false,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($getRecipient, $curl_options);
	$response = curl_exec($getRecipient);
	$xml = simplexml_load_string($response);
	curl_close ($getRecipient);
	print_r($xml);
	}

function rm_modifyRecipient($account_id, $list_id, $email, $request_body) {
	$modify_recipient_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/';
	$api_url = $modify_recipient_url . $account_id . "/" . $list_id . "/" . $email;
	$header = array("Content-Type: application/xml");
	$ModifyList = curl_init();
	$putString = $request_body;
	$putData = tmpfile();
	fwrite($putData, $putString);
	fseek($putData, 0);
	$length = strlen($putString);
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_INFILE => $putData,
		CURLOPT_INFILESIZE => $length,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_PUT => true
		);

	curl_setopt_array($ModifyList, $curl_options);
	$response = curl_exec($ModifyList);
	curl_close($ModifyList);
	$response_xml = simplexml_load_string($response);
	print_r($response);
	}

function rm_deleteRecipient($account_id, $list_id, $email) {
	$delete_recipient_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/';
	$api_url = $delete_recipient_url . $account_id . "/" . $list_id . "/" . $email;
	$header = array("Content-Type: application/xml");
	$deleteRecipient = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_CUSTOMREQUEST => "DELETE",
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($deleteRecipient, $curl_options);
	$response = curl_exec($deleteRecipient);
	$xml = simplexml_load_string($response);
	curl_close ($deleteRecipient);
	print_r($xml);
	
	}

function rm_createRecipient($account_id, $list_id, $request_body) {
	$create_recipient_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/';
	$api_url = $create_recipient_url . $account_id . "/" . $list_id;
	$header = array("Content-Type: application/xml");
	$createRecipient = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($createRecipient, $curl_options);
	$response = curl_exec($createRecipient);
	curl_close($createRecipient);
	$response_xml = simplexml_load_string($response);
	$QueueResponse = $response_xml;
	print_r($response_xml);
	}

function rm_getindividualrecipientByQuery($account_id, $list_id, $request_body) {
	$get_recipient_query_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/find/';
	$api_url = $get_recipient_query_url . $account_id . "/" . $list_id;
	$header = array("Content-Type: application/xml");
	$modifyIndRecipient = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($modifyIndRecipient, $curl_options);
	$response = curl_exec($modifyIndRecipient);
	curl_close($modifyIndRecipient);
	$response_xml = simplexml_load_string($response);
	$queryResponse = $response_xml;
	print_r($response);

	}

function rm_deleteindividualrecipientByQuery($account_id, $list_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$delete_recipient_query_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/find/delete/';	
	$api_url = $delete_recipient_query_url . $account_id . "/" . $list_id;
	$deleteQuery = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);

	curl_setopt_array($deleteQuery, $curl_options);
	$response = curl_exec($deleteQuery);
	curl_close($deleteQuery);
	$response_xml = simplexml_load_string($response);
	$queryResponse = $response_xml;
	print_r($response_xml);
	}


function rm_modifyindividualrecipientByQuery($account_id, $list_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$modify_recipient_query_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/find/modify/';
	$api_url = $modify_recipient_query_url . $account_id . "/" . $list_id ;
	$modifyIndRecipient = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);

	curl_setopt_array($modifyIndRecipient, $curl_options);
	$response = curl_exec($modifyIndRecipient);
	curl_close($modifyIndRecipient);
	$response_xml = simplexml_load_string($response);
	$queryResponse = $response_xml;
	print_r($response);

	}

function rm_enumerateRecipients($account_id, $list_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$enumerate_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/query/';
	$api_url = $enumerate_recipients_url . $account_id . "/" . $list_id;
	$EnumerateRecipients = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_POST => true
		);
	curl_setopt_array($EnumerateRecipients, $curl_options);
	$response = curl_exec($EnumerateRecipients);
	curl_close($EnumerateRecipients);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	}

function rm_deletebatchrecipientsByQuery($account_id, $list_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$delete_recipient_query_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/query/delete/';
	$api_url = $delete_recipient_query_url . $account_id . "/" . $list_id ;
	$deleteBatchQuery = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($deleteBatchQuery, $curl_options);
	$response = curl_exec($deleteBatchQuery);
	curl_close($deleteBatchQuery);
	$response_xml = simplexml_load_string($response);
	$queryResponse = $response_xml;
	print_r($response_xml);
	}
function rm_modifybatchrecipientsByQuery($account_id, $list_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$modify_recipients_query_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/find/modify/';
	$api_url = $modify_recipients_query_url . $account_id . "/" . $list_id;
	$modifybatchRecipient = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($modifybatchRecipient, $curl_options);
	$response = curl_exec($modifybatchRecipient);
	curl_close($modifybatchRecipient);
	$response_xml = simplexml_load_string($response);
	$queryResponse = $response_xml;
	print_r($response);
	}

function rm_modifycreaterecipientByQuery($account_id, $list_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$modify_create_query_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/subscribe/';
	$api_url = $modify_create_query_url . $account_id . "/" . $list_id;
	$modifycreateRecipient = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($modifycreateRecipient, $curl_options);
	$response = curl_exec($modifycreateRecipient);
	curl_close($modifycreateRecipient);
	$response_xml = simplexml_load_string($response);
	$queryResponse = $response_xml;
	print_r($response);
	}

function rm_optinFromAccount($account_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$optin_from_account = 'https://services.reachmail.net/Rest/Contacts/v1/optin/';
	$api_url = $optin_from_account . $account_id;
	$optIn = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($optIn, $curl_options);
	$response = curl_exec($optIn);
	curl_close($optIn);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	}

function rm_optoutFromAccount($account_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$opt_out_account_url = 'https://services.reachmail.net/Rest/Contacts/v1/optout/';
	$api_url = $opt_out_account_url . $account_id ;
	$optOut = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($optOut, $curl_options);
	$response = curl_exec($optOut);
	curl_close($optOut);
	$response_xml = simplexml_load_string($response);
	$scriptResponse = $response_xml;
	print_r($response_xml);

	
	}

function rm_addFile($account_id, $data_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$add_file_url = 'https://services.reachmail.net/Rest/Content/Library/files/';
	$api_url = $add_file_url . $account_id . "/" . $data_id;
	$addFile = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);

	curl_setopt_array($addFile, $curl_options);
	$response = curl_exec($addFile);
	curl_close($addFile);

	$response_xml = simplexml_load_string($response);
	$import_id = $response_xml ->Id;

	print_r($response_xml);
	}

function rm_getFile($account_id, $data_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$get_file_url = 'https://services.reachmail.net/Rest/Content/Library/files/';
	$api_url = $get_file_url . $account_id . "/" . $data_id;
	$getFile = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true
		);

	curl_setopt_array($getFile, $curl_options);
	$response = curl_exec($getFile);
	curl_close($getFile);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	}

function rm_modifyFile($account_id, $data_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$modify_file_url = 'https://services.reachmail.net/Rest/Content/Library/files/';
	$api_url = $modify_file_url . $account_id . "/" . $data_id;
	$modifyFile = curl_init();
	$putString = $request_body;
	$putData = tmpfile();
	fwrite($putData, $putString);
	fseek($putData, 0);
	$length = strlen($putString);
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_INFILE => $putData,
		CURLOPT_INFILESIZE => $length,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_PUT => true
		);

	curl_setopt_array($modifyFile, $curl_options);
	$response = curl_exec($modifyFile);
	curl_close($modifyFile);
	$response_xml = simplexml_load_string($response);
	print_r($response);
	}

function rm_deleteFile ($account_id, $data_id) {
	$delete_file_url = 'https://services.reachmail.net/Rest/Content/Library/files/';
	$api_url = $delete_file_url . $account_id . "/" . $data_id;
	$header = array("Content-Type: application/xml");
	$deleteFile = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_CUSTOMREQUEST => "DELETE",
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($deleteFile, $curl_options);
	$response = curl_exec($deleteFile);
	$xml = simplexml_load_string($response);
	curl_close ($deleteFile);
	print_r($response);
	}

function rm_enumerateFiles ($account_id, $request_body) {
	$enumerate_files_url = 'https://services.reachmail.net/Rest/Content/Library/files/query/';
	$api_url = $enumerate_files_url . $account_id;
	$header = array("Content-Type: application/xml");
	$enumerateFiles = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_POST => true
		);

	curl_setopt_array($enumerateFiles, $curl_options);
	$response = curl_exec($enumerateFiles);
	curl_close($enumerateFiles);
	
	$response_xml = simplexml_load_string($response);

	print_r($response_xml);

	}

function rm_createfileFolder($account_id, $request_body) {
	$create_file_folder_url = 'https://services.reachmail.net/Rest/Content/Library/folders/';
	$api_url = $create_file_folder_url . $account_id ;
	$header = array("Content-Type: application/xml");
	$createFolder = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);

	curl_setopt_array($createFolder, $curl_options);
	$response = curl_exec($createFolder);
	curl_close($createFolder);	
	$response_xml = simplexml_load_string($response);
	$QueueResponse = $response_xml;
	$group_id = $response_xml ->Id;

	print_r($response_xml);

	}

function rm_getfileFolder($account_id, $folder_id, $request_body){
	$get_file_folder_url = 'https://services.reachmail.net/Rest/Content/Library/folders/';
	$api_url = $get_file_folder_url . $account_id . "/" . $folder_id;
	$header = array("Content-Type: application/xml");
	$getfileFolder = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true
		);

	curl_setopt_array($getfileFolder, $curl_options);
	$response = curl_exec($getfileFolder);
	curl_close($getfileFolder);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);

	}

function rm_modifyfileFolder($account_id, $folder_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$modify_file_folder_url = 'https://services.reachmail.net/Rest/Content/Library/folders/';
	$api_url = $modify_file_folder_url . $account_id . "/" . $folder_id;
	$ModifyGroup = curl_init();
	$putString = $request_body;
	$putData = tmpfile();
	fwrite($putData, $putString);
	fseek($putData, 0);
	$length = strlen($putString);
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_INFILE => $putData,
		CURLOPT_INFILESIZE => $length,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_PUT => true
		);

	curl_setopt_array($ModifyGroup, $curl_options);
	$response = curl_exec($ModifyGroup);
	curl_close($ModifyGroup);
	$response_xml = simplexml_load_string($response);
	$return = print_r($response);

	}

function rm_deletefileFolder($account_id, $folder_id) {
	$header = array("Content-Type: application/xml");
	$delete_folder_url = 'https://services.reachmail.net/Rest/Content/Library/folders/';
	$api_url = $delete_folder_url . $account_id . "/" . $folder_id;
	$deleteFile = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_CUSTOMREQUEST => "DELETE",
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($deleteFile, $curl_options);
	$response = curl_exec($deleteFile);
	$xml = simplexml_load_string($response);
	curl_close ($deleteFile);
	print_r($response);
	}

function rm_enumeratefileFolder($account_id, $request_body) {
	$enumerate_file_folders_url = 'https://services.reachmail.net/Rest/Content/Library/folders/query/';
	$api_url = $enumerate_file_folders_url . $account_id ;
	$header = array("Content-Type: application/xml");
	$enumerateFiles = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_POST => true
		);

	curl_setopt_array($enumerateFiles, $curl_options);
	$response = curl_exec($enumerateFiles);
	curl_close($enumerateFiles);
	$response_xml = simplexml_load_string($response);	
	print_r($response_xml);

	}

/*MAILING SERVICE*/

function rm_createMailing($account_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$create_mailing_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/';	
	$api_url = $create_mailing_url . $account_id;
	$createMailing = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);

	curl_setopt_array($createMailing, $curl_options);
	$response = curl_exec($createMailing);
	curl_close($createMailing);
	$response_xml = simplexml_load_string($response);
	$createResponse = $response_xml;
	print_r($response_xml);
	}

function rm_getMailing($account_id, $mailing_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$get_mailing_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/';
	$api_url = $get_mailing_url . $account_id . "/" . $mailing_id;
	$createMailing = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => false,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($createMailing, $curl_options);
	$response = curl_exec($createMailing);
	curl_close($createMailing);
	$response_xml = simplexml_load_string($response);
	$createResponse = $response_xml;
	print_r($response_xml);
	}

function rm_modifyMailing($account_id, $mailing_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$modify_mailing_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/';
	$api_url = $modify_mailing_url . $account_id . "/" . $mailing_id ; 
	$ModifyGroup = curl_init();
	$putString = $request_body;
	$putData = tmpfile();
	fwrite($putData, $putString);
	fseek($putData, 0);
	$length = strlen($putString);
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_INFILE => $putData,
		CURLOPT_INFILESIZE => $length,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_PUT => true
		);
	curl_setopt_array($ModifyGroup, $curl_options);
	$response = curl_exec($ModifyGroup);
	curl_close($ModifyGroup);
	$response_xml = simplexml_load_string($response);	
	$return = print_r($response);
	}

function rm_deleteMailing($account_id, $mailing_id) {
	$header = array("Content-Type: application/xml");
	$delete_mailing_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/';
	$api_url = $delete_mailing_url . $account_id . "/" . $mailing_id;
	$deleteMailing = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_CUSTOMREQUEST => "DELETE",
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($deleteMailing, $curl_options);
	$response = curl_exec($deleteMailing);
	$xml = simplexml_load_string($response);
	curl_close ($deleteMailing);	
	print_r($response);
	}

function rm_enumeratemailingGroups($account_id) {
	$header = array("Content-Type: application/xml");
	$enumerate_mailing_groups_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/groups/';
	$api_url = $enumerate_mailing_groups_url . $account_id;
	$EnumerateGroups = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => false,
		);
	curl_setopt_array($EnumerateGroups, $curl_options);
	$response = curl_exec($EnumerateGroups);
	$xml = simplexml_load_string($response);
	curl_close ($EnumerateGroups);
	print_r($xml);

	}


function rm_createmailingGroups($account_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$create_mailing_group_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/groups/';
	$api_url = $create_mailing_group_url . $account_id;
	$createGroup = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($createGroup, $curl_options);
	$response = curl_exec($createGroup);
	curl_close($createGroup);
	$response_xml = simplexml_load_string($response);
	$QueueResponse = $response_xml;
	$group_id = $response_xml ->Id;	
	print_r($response_xml);

	}

function rm_getmailingGroup($account_id, $folder_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$get_mailing_group = 'https://services.reachmail.net/Rest/Content/Mailings/v1/groups/';
	$api_url = $get_mailing_group . $account_id . "/" . $folder_id;
	$EnumerateRecipients = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true
		);

	curl_setopt_array($EnumerateRecipients, $curl_options);
	$response = curl_exec($EnumerateRecipients);
	curl_close($EnumerateRecipients);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	}

function rm_modifymailingGroup($account_id, $folder_id, $request_body) {
	$modify_mailing_group_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/groups/';
	$api_url = $modify_mailing_group_url . $account_id . "/" . $folder_id;
	$header = array("Content-Type: application/xml");
	$ModifyGroup = curl_init();
	$putString = $request_body;
	$putData = tmpfile();
	fwrite($putData, $putString);
	fseek($putData, 0);
	$length = strlen($putString);
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_INFILE => $putData,
		CURLOPT_INFILESIZE => $length,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_PUT => true
		);
	curl_setopt_array($ModifyGroup, $curl_options);
	$response = curl_exec($ModifyGroup);
	curl_close($ModifyGroup);	
	$response_xml = simplexml_load_string($response);
	$return = print_r($response);
	}


function rm_deletemailingGroup($account_id, $folder_id) {
	$delete_mailing_group_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/groups/';
	$api_url = $delete_mailing_group_url . $account_id . "/" . $folder_id;
	$header = array("Content-Type: application/xml");
	$deleteFile = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_CUSTOMREQUEST => "DELETE",
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($deleteFile, $curl_options);
	$response = curl_exec($deleteFile);
	$xml = simplexml_load_string($response);
	curl_close ($deleteFile);
	print_r($response);
	}

function rm_enumerateMailings($account_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$enumerate_mailings_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/query/';
	$api_url = $enumerate_mailings_url . $account_id;
	$EnumerateMailings = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_POST => true
		);

	curl_setopt_array($EnumerateMailings, $curl_options);
	$response = curl_exec($EnumerateMailings);
	curl_close($EnumerateMailings);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	
	}

function rm_createmailingTemplate($account_id, $request_body) {
	$create_mailing_template_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/templates/';
	$api_url = $create_mailing_template_url . $account_id;
	$header = array("Content-Type: application/xml");
	$createMailing = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($createMailing, $curl_options);
	$response = curl_exec($createMailing);
	curl_close($createMailing);	
	$response_xml = simplexml_load_string($response);
	$createResponse = $response_xml;
	print_r($response_xml);
	}


function rm_getmailingTemplate($account_id, $template_id, $request_body) {
	$get_mailing_template_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/templates/';
	$api_url = $get_mailing_template_url . $account_id . "/" . $template_id;
	$header = array("Content-Type: application/xml");
	$createMailing = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => false,
		CURLOPT_HTTPHEADER => $header
		);

	curl_setopt_array($createMailing, $curl_options);	
	$response = curl_exec($createMailing);
	curl_close($createMailing);
	$response_xml = simplexml_load_string($response);
	$createResponse = $response_xml;
	print_r($response_xml);
	}

function rm_modifymailingTemplate($account_id, $template_id, $request_body) {
	$modify_mailing_template_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/templates/';
	$api_url = $modify_mailing_template_url . $account_id . "/" . $template_id ; 
	$header = array("Content-Type: application/xml");
	$ModifyGroup = curl_init();
	$putString = $request_body;
	$putData = tmpfile();
	fwrite($putData, $putString);
	fseek($putData, 0);
	$length = strlen($putString);
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_INFILE => $putData,
		CURLOPT_INFILESIZE => $length,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_PUT => true
		);
	curl_setopt_array($ModifyGroup, $curl_options);
	$response = curl_exec($ModifyGroup);
	curl_close($ModifyGroup);
	$response_xml = simplexml_load_string($response);
	$return = print_r($response);
	}

function rm_deletemailingTemplate($account_id, $template_id) {
	$delete_mailing_template_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/templates/';
	$api_url = $delete_mailing_template_url . $account_id . "/" . $template_id;
	$header = array("Content-Type: application/xml");
	$deleteMailing = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_CUSTOMREQUEST => "DELETE",
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($deleteMailing, $curl_options);
	$response = curl_exec($deleteMailing);
	$xml = simplexml_load_string($response);
	curl_close ($deleteMailing);	
	print_r($response);
	}

function rm_enumeratetemplateGroup($account_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$enumerate_template_groups_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/templates/groups/';
	$api_url = $enumerate_template_groups_url . $account_id;
	$enumerateFiles = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		);

	curl_setopt_array($enumerateFiles, $curl_options);
	$response = curl_exec($enumerateFiles);
	curl_close($enumerateFiles);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	}

function rm_createtemplateGroup($account_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$create_template_groups_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/templates/groups/';
	$api_url = $create_template_groups_url . $account_id;
	$createMailing = curl_init();
	$createtemplateGroup = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_POST => true
		);

	curl_setopt_array($createtemplateGroup, $curl_options);
	$response = curl_exec($createtemplateGroup);
	curl_close($createtemplateGroup);
	
	$response_xml = simplexml_load_string($response);

	print_r($response_xml);
	}

function rm_gettemplateGroup($account_id, $folder_id) {
	$header = array("Content-Type: application/xml");
	$get_template_group_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/templates/groups/';
	$api_url = $get_template_group_url . $account_id;
	$gettemplateGroup = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		);
	curl_setopt_array($gettemplateGroup, $curl_options);
	$response = curl_exec($gettemplateGroup);
	curl_close($gettemplateGroup);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	}

function rm_modifytemplateGroup($account_id, $folder_id, $request_body) {
	$modify_template_group_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/templates/groups/';
	$api_url = $modify_template_group_url . $account_id . "/" . $folder_id ; 
	$header = array("Content-Type: application/xml");
	$ModifyGroup = curl_init();
	$putString = $request_body;
	$putData = tmpfile();
	fwrite($putData, $putString);
	fseek($putData, 0);
	$length = strlen($putString);
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_INFILE => $putData,
		CURLOPT_INFILESIZE => $length,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_PUT => true
		);

	curl_setopt_array($ModifyGroup, $curl_options);
	$response = curl_exec($ModifyGroup);
	curl_close($ModifyGroup);
	$response_xml = simplexml_load_string($response);
	$return = print_r($response);

	}

function rm_deletetemplateGroup($account_id, $folder_id) {
	$delete_template_group_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/templates/groups/';
	$api_url = $delete_template_group_url . $account_id . "/" . $folder_id;
	$header = array("Content-Type: application/xml");
	$deleteGroup = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_CUSTOMREQUEST => "DELETE",
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($deleteGroup, $curl_options);
	$response = curl_exec($deleteGroup);
	$xml = simplexml_load_string($response);
	curl_close ($deleteGroup);
	print_r($response);
	}

function rm_enumeratemailingTemplates($account_id, $request_body) {
	$enumerate_mailing_templates_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/templates/query/';
	$api_url = $enumerate_mailing_templates_url . $account_id;
	$header = array("Content-Type: application/xml");
	$enumerateTemplates = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($enumerateTemplates, $curl_options);
	$response = curl_exec($enumerateTemplates);
	curl_close($enumerateTemplates);
	$response_xml = simplexml_load_string($response);
	$createResponse = $response_xml;
	print_r($response_xml);
	}

/**DATA SERVICE**/

function rm_upload($account_id) {
	$api_url = 'https://services.reachmail.net/Rest/Data';
	$header = array("Content-Type: application/xml");

	$file = 'xab.csv';
	$fp = file_get_contents($file);
	$request_body = $fp;
	$uploadData = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_FOLLOWLOCATION => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($uploadData, $curl_options);
	$response = curl_exec($uploadData);
	curl_close($uploadData);
	$response_xml = simplexml_load_string($response);
	$QueueResponse = $response_xml;
	$upload_id = $response_xml ->Id;
	print_r($response_xml);
	}


function rm_download($data_id) {
	$header = array("Content-Type: application/xml");
	$download_url = 'https://services.reachmail.net/Rest/Data/';
	$api_url = $download_url . $data_id;
	$downloadFile = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		);
	curl_setopt_array($downloadFile, $curl_options);
	$response = curl_exec($downloadFile);
	curl_close($downloadFile);
	$file_xml = simplexml_load_string($response);
	$data_response = $file_xml ->Id;
	print_r($response);
	$my_file = 'list.xml';
	$handle = fopen($my_file, 'w');
	$data = '<?xml version="1.0"?><?xml-stylesheet type="text/xsl" href="styles.xsl"?>';
	fwrite($handle, $data);
	fclose($handle);
	$my_file = 'list.xml';
	$handle = fopen($my_file, 'w');
	$data = $response;
	fwrite($handle, $data);
	fclose($handle);
	}

function rm_downloadFile($data_id, $file_name) {
	$download_file_url = 'https://services.reachmail.net/Rest/Data/';
	$api_url = $download_file_url . $data_id . "/" . $file_name;
	$header = array("Content-type: application/pdf");
	$fp = fopen('AdobeTestAPIMODIFY.pdf', 'w+');
	$downloadFile = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_TIMEOUT => 50,
		CURLOPT_FILE => $fp,
		);
	curl_setopt_array($downloadFile, $curl_options);
	$response = curl_exec($downloadFile);
	curl_close($downloadFile);
	
	fwrite($fp, $response);
	fclose($fp);

	}

function rm_exists($data_id, $request_body) {
	$exists_url = 'https://services.reachmail.net/Rest/Data/exists/';
	$api_url = $exists_url . $data_id;
	$header = array("Content-Type: application/xml");
	$dataExists = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_service_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		);

	curl_setopt_array($dataExists, $curl_options);
	$response = curl_exec($dataExists);
	curl_close($dataExists);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	}
/*Report Service*/

function rm_getbounceDetailReport() {
	$header = array("Content-Type: application/xml");
	$get_bounce_detail_report = 'https://services.reachmail.net/Rest/Reports/v1/details/mailings/bounces/';
	$api_url = $get_bounce_detail_report . $account_id . "/" . $mailing_id;
	$getbounces = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_POST => true
		);

	curl_setopt_array($getbounces, $curl_options);
	$response = curl_exec($getbounces);
	curl_close($getbounces);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	$fp = 'styledReport.xml';
	$handle = fopen($fp, 'w') or die('Cannot open file: ' . $my_file);
	$data = '<?xml version="1.0"?><?xml-stylesheet type="text/xsl" href="styles.xsl"?>';
	fwrite($handle, $data);
	fclose($handle);
	$my_file = 'styledReport.xml';
	$handle = fopen($fp, 'a') or die('Cannot open file: ' . $fp);
	$data = $response_xml;
	fwrite($handle, $data);
	fclose($handle);
	}

function rm_getoptoutDetailReport($account_id, $mailing_id, $request_body) {
	$get_opt_out_detail_report_url = 'https://services.reachmail.net/Rest/Reports/v1/details/mailings/optouts/';
	$api_url = $get_opt_out_detail_report_url . $account_id . "/" . $mailing_id;
	$header = array("Content-Type: application/xml");
	$getbounces = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_POST => true
		);

	curl_setopt_array($getbounces, $curl_options);
	$response = curl_exec($getbounces);
	curl_close($getbounces);
	
	$response_xml = simplexml_load_string($response);
	
	print_r($response_xml);
	$fp = 'styledReport.xml';
	$handle = fopen($fp, 'w') or die('Cannot open file: ' . $my_file);
	$data = '<?xml version="1.0"?><?xml-stylesheet type="text/xsl" href="styles.xsl"?>';
	fwrite($handle, $data);
	fclose($handle);
	$my_file = 'styledReport.xml';
	$handle = fopen($fp, 'a') or die('Cannot open file: ' . $fp);
	$data = $response_xml;
	fwrite($handle, $data);
	fclose($handle);
	}

function rm_getreadDetailReport($account_id, $mailing_id, $request_body) {
	$header = array("Content-Type: application/xml");
	$get_read_detail_report = 'https://services.reachmail.net/Rest/Reports/v1/details/mailings/optouts/';
	$api_url = $get_read_detail_report . $account_id . "/" . $mailing_id;
	$getbounces = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_service_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_POST => true
		);
	curl_setopt_array($getbounces, $curl_options);
	$response = curl_exec($getbounces);
	curl_close($getbounces);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	$fp = 'styledReport.xml';
	$handle = fopen($fp, 'w') or die('Cannot open file: ' . $my_file);
	$data = '<?xml version="1.0"?><?xml-stylesheet type="text/xsl" href="styles.xsl"?>';
	fwrite($handle, $data);
	fclose($handle);
	$my_file = 'styledReport.xml';
	$handle = fopen($fp, 'a') or die('Cannot open file: ' . $fp);
	$data = $response_xml;
	fwrite($handle, $data);
	fclose($handle);
	}

function rm_gettrackedlinkDetailReport($account_id, $mailing_id, $request_body) {
	$get_tracked_link_url = 'https://services.reachmail.net/Rest/Reports/v1/details/mailings/trackedlinks/';
	$api_url = $get_tracked_link_url . $account_id . "/" . $mailing_id;
	$header = array("Content-Type: application/xml");
	$gettracked = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_POST => true
		);

	curl_setopt_array($gettracked, $curl_options);
	$response = curl_exec($gettracked);
	curl_close($gettracked);
		
	$response_xml = simplexml_load_string($response);
	
	print_r($response_xml);
	$fp = 'styledReport.xml';
	$handle = fopen($fp, 'w') or die('Cannot open file: ' . $my_file);
	$data = '<?xml version="1.0"?><?xml-stylesheet type="text/xsl" href="styles.xsl"?>';
	fwrite($handle, $data);
	fclose($handle);
	$my_file = 'styledReport.xml';
	$handle = fopen($fp, 'a') or die('Cannot open file: ' . $fp);
	$data = $response_xml;
	fwrite($handle, $data);
	fclose($handle);

	}

function rm_getmailingReport($account_id, $mailing_id) {
	$get_mailing_report_url = 'https://services.reachmail.net/Rest/Reports/v1/mailings/';
	$api_url = $get_mailing_report_url . $account_id . "/" . $mailing_id;
	$header = array("Content-Type: application/xml");
	$getreport = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		);

	curl_setopt_array($getreport, $curl_options);
	$response = curl_exec($getreport);
	curl_close($getreport);

	$response_xml = simplexml_load_string($response);

	print_r($response_xml);
	$fp = 'styledReport.xml';
	$handle = fopen($fp, 'w') or die('Cannot open file: ' . $my_file);
	$data = '<?xml version="1.0"?><?xml-stylesheet type="text/xsl" href="styles.xsl"?>';
	fwrite($handle, $data);
	fclose($handle);
	$my_file = 'styledReport.xml';
	$handle = fopen($fp, 'a') or die('Cannot open file: ' . $fp);
	$data = $response_xml;
	fwrite($handle, $data);
	fclose($handle);
	}

function rm_enumeratemailingReports($account_id, $request_body) {
	$enumerate_reports_url = 'https://services.reachmail.net/Rest/Reports/v1/mailings/query/';
	$api_url = $enumerate_reports_url . $account_id;
	$header = array("Content-Type: application/xml");
	$enumeratemReport = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_POSTFIELDS => $request_body,
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => true,
		CURLOPT_HTTPHEADER => $header
		);

	curl_setopt_array($enumeratemReport, $curl_options);
	$response = curl_exec($enumeratemReport);
	curl_close($enumeratemReport);	
	$response_xml = simplexml_load_string($response);
	$createResponse = $response_xml;
	print_r($response_xml);
	}

function rm_getTrackedLinkReportbyMailingID($account_id, $mailing_id) {
	$get_tracked_link_url = 'https://services.reachmail.net/Rest/Reports/v1/mailings/trackedLinks/';
	$api_url = $get_tracked_link_url . $account_id . "/" . $mailing_id;
	$header = array("Content-Type: application/xml");
	$gettracked = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_POST => false,
		CURLOPT_HTTPHEADER => $header
		);
	curl_setopt_array($gettracked, $curl_options);
	$response = curl_exec($gettracked);
	curl_close($gettracked);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	}

function rm_getTrackedLinkReportbyMailingListID($account_id, $mailing_id, $list_id) {
	$get_tracked_link_url = 'https://services.reachmail.net/Rest/Reports/v1/mailings/trackedLinks/';
	$api_url = $get_tracked_link_url . $account_id . "/" . $mailing_id . "/" . $list_id;
	$header = array("Content-Type: application/xml");
	$gettrackedbyList = curl_init();
	$curl_options = array(
		CURLOPT_URL => $api_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true,
		CURLOPT_HTTPHEADER => $header,
		CURLOPT_POST => false
		);
	curl_setopt_array($gettrackedbyList, $curl_options);
	$response = curl_exec($gettrackedbyList);
	curl_close($gettrackedbyList);
	$response_xml = simplexml_load_string($response);
	print_r($response_xml);
	}



/*Runtime Service*/
function rm_getInfo($account_id) {
	$get_info_url =  'https://services.reachmail.net/Rest/Runtime/';
	$info_request = curl_init();
	$curl_options = array(
		CURLOPT_URL => $get_info_url,
		CURLOPT_HEADER => false,
		CURLOPT_USERPWD => "$this->_account_key\\$this->_username:$this->_password",
		CURLOPT_RETURNTRANSFER => true
		);
	curl_setopt_array($info_request, $curl_options);
	$response = curl_exec($info_request);
	$xml = simplexml_load_string($response);
	$account_id = $xml->AccountId;

	curl_close ($id_request);
	print_r($xml);
	}

}
?>
