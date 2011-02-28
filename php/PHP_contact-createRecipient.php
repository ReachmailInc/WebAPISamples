<?php

//---- API Service: ContactService\CreateRecipient
//---- This API service creates a single subscriber in a list.

//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at
//---- services.reachmail.net
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/';

//---- The account id and list ids are API specific. For more information on
//---- getting these variables please refer to the code examples for
//---- AdministrationService/GetCurrentUser and ContactService/EnumerateLists.
$account_id = 'api-account-id';
$list_id = 'list-id';
$api_service_url = $api_service_url.$account_id.'/'.$list_id;

//---- The header variable is used to set the content type of the request and
//---- will be used later in the cURL options
$header = array("Content-Type: application/xml");

$email = 'faker@domain.com';
$name = 'Frank Aker';

$request_body = "<RecipientProperties><Email>$email</Email><Properties><Property><Name>FullName</Name><Value>$name</Value></Property></Properties></RecipientProperties>";

$create_recipients_request = curl_init();
$curl_options = array(
CURLOPT_URL => $api_service_url,
CURLOPT_HEADER => false,
CURLOPT_USERPWD => "$account_key\\$username:$password",
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

?>