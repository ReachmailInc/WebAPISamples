<?php
//---- API Service URL: ContactService\ExportRecipients
//---- This service will let you export a lists recipients and will return an exportID
//---- The exportID will then be used to download the file

//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at services.reachmail.net
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/export/' ;
//---- For information on getting the API account id please refer to the
//---- AdministrationService/GetCurrentUser example
$account_id = 'api_account-id';
//---- The list id is for the list you are exporting
//---- For information on getting the list id please refer to the
//---- ContactService/EnumerateLists example.
$list_id = 'list_id';
$api_service_url = $api_service_url.$account_id.'/'.$list_id;
//---- The header variable is used to set the content type of the request and
//---- will be used later in the cURL options
$header = array("Content-Type: application/xml");
//---- The request body will vary depending on the fields in  your lists. 
//---- Please see https://services.reachmail.net/sdk/ for the necessary data you will need to include in your xml.
$request_body = '<ExportParameters><ExportOptions><Format>CharacterSeperated</Format><HeaderRow>true</HeaderRow><CharacterSeperatedData><Delimiter>Comma</Delimiter></CharacterSeperatedData><FieldMapping><FieldMapping><DestinationFieldName>Email</DestinationFieldName><SourceFieldName>Email</SourceFieldName></FieldMapping></FieldMapping></ExportOptions></ExportParameters>';

//---- Intialize cURL, set options and make the request
$export_recipients_request = curl_init();
$curl_options = array(
        CURLOPT_URL => $api_service_url,
        CURLOPT_HEADER => false,
        CURLOPT_USERPWD => "$account_key\\$username:$password",
        CURLOPT_HTTPHEADER => $header,
        CURLOPT_POST => true,
        CURLOPT_POSTFIELDS => $request_body,
        CURLOPT_RETURNTRANSFER => true
        );

curl_setopt_array($export_recipients_request, $curl_options);

$export_recipients_response = curl_exec($export_recipients_request);

curl_close($export_recipients_request);
	
//--- If your import was successful you will recieve an importID
if($export_recipients_response == "1"){
        print "\nSuccessfully exported listID".$list_id."\n\n";
} else {
        print_r($export_recipients_response);
}

?>