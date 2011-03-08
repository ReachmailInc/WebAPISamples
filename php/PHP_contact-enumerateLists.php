<?php

//---- API Service: ContactService\EnumerateLists
//---- This service details all the lists in an account. Optionally a filter
//---- can be added to refine the results. In this example we'll perfrom just
//---- a basic query to return all the lists available in the account.

//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at services.reachmail.net
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/query/';

//---- For information on getting the API account id please refer to the
//---- AdministrationService/GetCurrentUser example
$account_id = 'api-account-id';
$api_service_url = $api_service_url.$account_id;

//---- The header variable will be used in the cURL options, the request body
//---- is the body of the post that will be made to the API service URL
$header = array("Content-Type: application/xml");
$request_body = '<ListFilter><GreaterThan>1</GreaterThan><SmallerThan>5</SmallerThan></ListFilter>';

//---- Intialize cURL, set options and make the request
$enumerate_lists_request = curl_init();
$curl_options = array(
CURLOPT_URL => $api_service_url,
CURLOPT_HEADER => false,
CURLOPT_USERPWD => "$account_key\\$username:$password",
CURLOPT_HTTPHEADER => $header,
CURLOPT_POST => true,
CURLOPT_POSTFIELDS => $request_body,
CURLOPT_RETURNTRANSFER => true
);
curl_setopt_array($enumerate_lists_request, $curl_options);
$enumerate_lists_response = curl_exec($enumerate_lists_request);
curl_close($enumerate_lists_request);

//---- Load the XML from the response into a parser via simplexml
$list_xml = simplexml_load_string($enumerate_lists_response);

//---- In this example we've provided a very basic example of accessing XML
//---- nodes using simplexml objects.
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
//--- saves response a seperate xml file to the current directory
echo $list_xml->saveXML("lists.xml");
?>

