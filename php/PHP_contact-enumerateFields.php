<?php

//---- API Service: ContactService\EnumerateFields
//---- This service can detail all the fields in an account that are available
//---- when creating subscriber lists. The 'Name' node is the result node to
//---- reference when creating lists.

//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at
//---- services.reachmail.net
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/fields/';

//---- For information on getting the API account id please refer to the
//---- AdministrationService/GetCurrentUser example
$account_id = 'api_account-id';
$api_service_url = $api_service_url.$account_id;

//---- The header variable will be used in the cURL options.
$header = array("Content-Type: application/xml");

//---- Intialize cURL, set options and make the request
$enumerate_fields_request = curl_init();
$curl_options = array(
CURLOPT_URL => $api_service_url,
CURLOPT_HEADER => false,
CURLOPT_USERPWD => "$account_key\\$username:$password",
CURLOPT_HTTPHEADER => $header,
CURLOPT_RETURNTRANSFER => true
);
curl_setopt_array($enumerate_fields_request, $curl_options);
$enumerate_fields_response = curl_exec($enumerate_fields_request);
curl_close($enumerate_fields_request);

//---- Load the XML from the response into a parser via simplexml
$field_xml = simplexml_load_string($enumerate_fields_response);

//---- In this example we've provided a very basic example of accessing XML
//---- nodes using simplexml objects.
$field_names = array();
$field_descriptions = array();

foreach($field_xml->Field as $field){
$field_names[] = $field->Name;
$field_descriptions[] = $field->Description;
}

$field_count = count($field_names);

print "\nFormat - Field Name : Field Description\n";
for($i=0; $i<$field_count; $i++){
print $field_names[$i]." : ".$field_descriptions[$i]."\n";
}
print "\n";
//--- save the response into an xml file in the current directory
echo $field_xml->saveXML("fields.xml");
?>