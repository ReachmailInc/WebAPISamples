<?php

#include the api wrapper
include_once('./rmapi.class.php');

#initialize the rmapi class
$rmapi = new RMAPI('YoUrSeCr3tTokenG03sH3rE');


#retrieve account information array 
$account_info = $rmapi->rm_administrationUsersCurrent();

#parse array and access the account id stdClass object value.  returns just the the account GUID as a string
$AccountId = $account_info['service_response']->AccountId;

#due to PHP's lack of dictionary objects, each recipient must be placed in their own single record array
$request = array (
	"FromAddress" => "sender@domain.tld",
	"Recipients" =>  [array(
		"Address" => "email@domain.tld"
	), array(
		"Address" => "email2@domain.tld"
	)],
	"Headers" => array( # use this array to attach any and all message headers
		"Subject" => "Test Subject",
		"From" => "FromName <sender@domain.tld>", # use this header to also specify a from name
		"X-Company" => "ReachMail"
	),
	"BodyText" => "This is the TEXT version of the Easy-SMTP API test", #plain text content
	"BodyHtml" => "This is the HTML version of the <a href=\"http://www.easy-smtp.com\">Easy-SMTP</a> API test",  #be sure to escape quote with the backslash in your HTML content
	"Tracking" => true #boolean statement for enabling link tracking
);

#send the message and store the response in a variable for easy access.
$es_campaign=$rmapi->rm_easySmtpDelivery($AccountId, $request);
?>
