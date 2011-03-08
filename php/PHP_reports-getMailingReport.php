<?php

//---- API Service: ReportService\GetMailingReport
//---- This service retrieves the campaign statistics for a specific mail campaign.

//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at
//---- services.reachmail.net
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/Rest/Reports/v1/mailings/';

//---- For information on getting the API account id please refer to the
//---- AdministrationService/GetCurrentUser example. For information on getting
//---- the mailign id please refer to the example for
//---- ReportService_EnumerateMailingReports 
$account_id = 'api-account-id';
$mailing_id = 'mailing-id';
$api_service_url = $api_service_url.$account_id."/".$mailing_id;

//---- The header variable will be used in the cURL options.
$header = array("Content-Type: application/xml");

//---- Intialize cURL, set options and make the request
$mail_report_request = curl_init();
$curl_options = array(
CURLOPT_URL => $api_service_url,
CURLOPT_HEADER => false,
CURLOPT_USERPWD => "$account_key\\$username:$password",
CURLOPT_HTTPHEADER => $header,
CURLOPT_RETURNTRANSFER => true
);
curl_setopt_array($mail_report_request, $curl_options);
$mail_report_response = curl_exec($mail_report_request);
curl_close($mail_report_request);

//---- Load the XML from the response into a parser via simplexml
$mail_report_xml = simplexml_load_string($mail_report_response);

//---- In this example we've provided a very basic example of accessing XML
//---- nodes using simplexml objects.
print_r($mail_report_xml);
//--- This will save the response as a seperate xml file in the current directory
echo $mail_report_xml->saveXML("report.xml");
?>