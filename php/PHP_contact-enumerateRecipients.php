<?php

//---- API Service: ContactService\EnumerateRecipients
//---- This service can be used to get list subscribers who match specific
//---- criteria. In this example we'll serch for users created after
//---- 15 May, 2010.

//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at
//---- services.reachmail.net
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/query/';

//---- The following account id and list id are API specific. Please refer to
//---- the code samples for AdministrationService\GetCurrentUser and
//---- ContactService\EnumerateLists for more information on getting these values
$account_id = 'api-account-id';
$list_id = 'list-id';
$api_service_url = $api_service_url.$account_id.'/'.$list_id;

//---- This header is required in $curl_options when POSTing XML content
$header = array("Content-Type: application/xml");

//---- Setting the date threshold
$date = '2011-01-25T12:00:00';

//---- The request body contains the XML that will be posted to the API
//---- service URL.
$request_body = "<RecipientFilter><NewerThan>$date</NewerThan></RecipientFilter>";

//---- Initialize cURL and set all the options
$enumerate_recipients_request = curl_init();
$curl_options = array(
CURLOPT_URL => $api_service_url,
CURLOPT_HEADER => false,
CURLOPT_USERPWD => "$account_key\\$username:$password",
CURLOPT_HTTPHEADER => $header,
CURLOPT_POST => true,
CURLOPT_POSTFIELDS => $request_body,
CURLOPT_RETURNTRANSFER => true
);

curl_setopt_array($enumerate_recipients_request, $curl_options);

$enumerate_recipients_response = curl_exec($enumerate_recipients_request);

curl_close($enumerate_recipients_request);

//---- Use simplexml to parse the response from the request and print the
//---- results
$response_xml = simplexml_load_string($enumerate_recipients_response);

$i = 0;
print "\n";

foreach($response_xml->Recipient as $recipients){
$email_addresses[] = $recipients->Email;
echo $email_addresses[$i]."\n";
$i++;
}

print "\n";
//--- save the response into an xml file in the current directory
echo $response_xml->saveXML("records.xml");
?>