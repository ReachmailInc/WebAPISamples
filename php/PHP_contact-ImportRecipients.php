<?php
//---- API Service: ContactService\ImportRecipients
//---- This API service imports whole files into a specified list.
//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at
//---- services.reachmail.net
$account_key = 'account_id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/import/';

//---- The account id and list ids are API specific. For more information on
//---- getting these variables please refer to the code examples for
//---- AdministrationService/GetCurrentUser and ContactService/EnumerateLists.
$account_id = 'api-account-id';
//--- This is the list you want to import into
$list_id = 'llist-id';
$api_service_url = $api_service_url.$account_id.'/'.$list_id;

//---- The header variable is used to set the content type of the request and
//---- will be used later in the cURL options
$header = array("Content-Type: application/xml");

//--- The request body will vary depending on the fields in both your lists. The dataID is specific to the uploaded list you are importing. Please see https://services.reachmail.net/sdk/ for the necessary data you will need to include in your xml.
$request_body = "<Parameters><DataId>ID_recieved_from_data_upload</DataId><FieldMappings><FieldMapping><DestinationFieldName>Email</DestinationFieldName><SourceFieldPosition>1</SourceFieldPosition></FieldMapping><FieldMapping><DestinationFieldName>FullName</DestinationFieldName><SourceFieldPosition>2</SourceFieldPosition></FieldMapping></FieldMappings><ImportOptions><CharacterSeperatedOptions><Delimiter>Comma</Delimiter></CharacterSeperatedOptions><Format>CharacterSeperated</Format></ImportOptions></Parameters>";

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

//--- If your import was successful you will recieve an importID
if($create_recipients_response == "1"){
        print "\nSuccessfully added $email to $list_id\n\n";
} else {
        print_r($create_recipients_response);
}

?>