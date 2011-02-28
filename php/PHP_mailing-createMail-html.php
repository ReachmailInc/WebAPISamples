<?php

//---- API Service: MailingService\CreateMailing
//---- This API service creates a mail.

//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at
//---- services.reachmail.net
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/REST/Content/Mailings/v1/';

//---- The account id is API specific. For more information on
//---- getting this variable please refer to the code example for
//---- AdministrationService/GetCurrentUser.
$account_id = 'api-account-id';
$api_service_url = $api_service_url.$account_id;

//---- The header variable is used to set the content type of the request and
//---- will be used later in the cURL options
$header = array("Content-Type: application/xml");

//---- Here the request body is set.
$request_body = "<MailingProperties><FromEmail>news@yourdomain.com</FromEmail><FromName>Company X News</FromName><HtmlContent><![CDATA[<html><head><title>my mail</title><body><h1>Welcome to my Newsletter</h1><p>I made this email with the ReachMail API</p><p>Here's a <a href='http://reachmail.net'>link</a></p></body></html>]]></HtmlContent><MailingFormat>Html</MailingFormat><Name>API Demo Mail</Name><Subject>Company X Newsletter</Subject><ReplyToEmail>news@yourdomain.com</ReplyToEmail><TrackedLink><Created>2011-01-23T12:00:00</Created><LinkMailingFormat>Html</LinkMailingFormat><Modified>2011-01-25T12:00:00</Modified><Url>http://reachmail.net</Url></TrackedLink></MailingProperties>";

$create_mail_request = curl_init();
$curl_options = array(
CURLOPT_URL => $api_service_url,
CURLOPT_HEADER => false,
CURLOPT_USERPWD => "$account_key\\$username:$password",
CURLOPT_HTTPHEADER => $header,
CURLOPT_POST => true,
CURLOPT_POSTFIELDS => $request_body,
CURLOPT_RETURNTRANSFER => true
);

curl_setopt_array($create_mail_request, $curl_options);

$create_mail_response = curl_exec($create_mail_request);

curl_close($create_mail_request);

$xml = simplexml_load_string($create_mail_response);

$mail_id = $xml->Id;
print "\nMail has been successfully created!\nYour mail id: $mail_id\n\n";

?>