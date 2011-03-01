<?php
//---- API Service URL: AdministrationService\GetCurrentUser
//---- This service will let you retrieve your API account id. The API account
//---- is required for accessing other API services.

//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at
//---- services.reachmail.net
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Administration/v1/users/current';

//---- Intialize cURL and set the options.
$account_id_request = curl_init();
$curl_options = array(
        CURLOPT_URL => $api_service_url,
        CURLOPT_HEADER => false,
        CURLOPT_USERPWD => "$account_key\\$username:$password",
        CURLOPT_RETURNTRANSFER => true
        );
curl_setopt_array($account_id_request, $curl_options);

$response = curl_exec($account_id_request);

//---- Load the XML from the response into the simplexml parser and get the
//---- account id.
//$xml = simplexml_load_string($response);

$xml = simplexml_load_string($response);

$account_id = $xml->AccountId;

print "\n".$account_id."\n\n";
//--- save the response into an xml file in the current directory
echo $account_id->saveXML("write.xml");
?>