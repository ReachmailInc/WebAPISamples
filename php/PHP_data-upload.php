<?php

//---- API Service: DataServices/Upload
//---- This API service uploads a file.

//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at
//---- services.reachmail.net
$account_key = 'account_key';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Data/';

//---- The header variable is used to set the content type of the request and
//---- will be used later in the cURL options
$header = array("Content-Type: application/xml");
$fp = fopen('path_to_file','r');
$request_body = $fp;

$upload_file_request = curl_init();
$curl_options = array(
        CURLOPT_URL => $api_service_url,
        CURLOPT_HEADER => false,
        CURLOPT_USERPWD => "$account_key\\$username:$password",
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
?>