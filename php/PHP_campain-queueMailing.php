<?php
//---- API Service: CampaingService/QueueMailing
//---- This service will let you queue a mailing for send
//---- By setting the options in the XML you can control 
//---- when and to what list your mailing is sent to

//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at
//---- services.reachmail.net
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Campaigns/v1/';

//---- For information on getting the API account id please refer to the
//---- AdministrationService/GetCurrentUser example
$account_id = 'api-account-id';
$api_service_url = $api_service_url.$account_id."/queue";

//---- The header variable will be used in the cURL options, the request body
//---- is the body of the post that will be made to the API service URL
$header = array("Content-Type: application/xml");
$request_body = '<QueueParameters><ListIds><Id>list-id</Id></ListIds><MailingId>mail-id</MailingId><Properties><DeliveryTime>2011-03-01T12:00:00</DeliveryTime><IsTest>0</IsTest></Properties></QueueParameters>';

//---- Intialize cURL, set options and make the request
$queue_mailing_request = curl_init();
$curl_options = array(
CURLOPT_URL => $api_service_url,
CURLOPT_HEADER => false,
CURLOPT_USERPWD => "$account_key\\$username:$password",
CURLOPT_HTTPHEADER => $header,
CURLOPT_POST => true,
CURLOPT_POSTFIELDS => $request_body,
CURLOPT_RETURNTRANSFER => true
);
curl_setopt_array($queue_mailing_request, $curl_options);
$queue_mailing_response = curl_exec($queue_mailing_request);
curl_close($queue_mailing_request);

//---- Load the XML from the response into a parser via simplexml
$mail_xml = simplexml_load_string($queue_mailing_response);
print_r($mail_xml);
?>

