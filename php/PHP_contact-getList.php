<?php

//---- API Service URL: ContactServices\GetList
//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at
//---- services.reachmail.net
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';

//---- For information on getting the API account id please refer to the
//---- AdministrationService/GetCurrentUser example. For information on getting
//---- the listid please refer to the example for
//---- ContactServices\EnumerateLists

$account_id = 'api-account-id';
$list_id = 'list_id';
$api_service_url = $api_service_url.$account_id."/".$list_id;

//---- The header variable will be used in the cURL options.
$header = array("Content-Type: application/xml");

//---- Intialize cURL and set the options.
$get_list_request = curl_init();
$curl_options = array(
        CURLOPT_URL => $api_service_url,
        CURLOPT_HEADER => false,
        CURLOPT_USERPWD => "$account_key\\$username:$password",
CURLOPT_HTTPHEADER => $header,
        CURLOPT_RETURNTRANSFER => true
        );
curl_setopt_array($get_list_request, $curl_options);

$response = curl_exec($get_list_request);
curl_close($get_list_request);

//---- Load the XML from the response into the simplexml parser and get the list itself

$xml = simplexml_load_string($response);

print_r($response);
?>