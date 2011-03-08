<?php

//---- API Service: ContactService\CreateList
//---- This API service creates a new list.

//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at services.reachmail.net
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';

//---- The account id is API specific. For more information on
//---- getting this variable please refer to the code example for
//---- AdministrationService/GetCurrentUser.
$account_id = 'api-account-id';
$api_service_url = $api_service_url.$account_id;

//---- The header variable is used to set the content type of the request and
//---- will be used later in the cURL options
$header = array("Content-Type: application/xml");

//---- While the request body can be set wiithin the cURL options array it's
//---- easier to define long request bodies outside of the array.
$request_body = "<ListProperties><Fields><FieldNames><Field>Email</Field><Field>FullName</Field></FieldNames></Fields><Name>API TEST LIST 20110101A</Name></ListProperties>";

$create_list_request = curl_init();
$curl_options = array(
CURLOPT_URL => $api_service_url,
CURLOPT_HEADER => false,
CURLOPT_USERPWD => "$account_key\\$username:$password",
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
//--- save the response into an xml file in the current directory
echo $list_api_id->saveXML("listID.xml");
?>