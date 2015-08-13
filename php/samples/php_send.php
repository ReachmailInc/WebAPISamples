<?php

// Include the api wrapper
include_once('./rmapi.class.php');

// Initialize the rmapi class
// your token can be generated from the User Interface at https://go.reachmail.net
// log in, got to the Account Tab -> Tokens to generate your access token
$rmapi = new RMAPI('YoUrSeCr3tTokenG03sH3rE');


// Retrieve account information array 
$account_info = $rmapi->rm_administrationUsersCurrent();

// Parse array and access the account id stdClass object value.
// Returns just the the account GUID as a string
$AccountId = $account_info['service_response']->AccountId;

// Due to PHP's lack of dictionary objects, each recipient must be placed in 
// their own single record array
$request = array (
	"FromAddress"   => "sender@domain.tld",
	"Recipients"    =>  array(
                            array("Address" => "email@domain.tld"), 
                            array("Address" => "email2@domain.tld")
                        ),

    // Use the `Headers` parameter to set the message headers
    // Note that here, the `From` header is used to create a more friendly
    // sender alias. 
	"Headers"       => array( 
                        "Subject" => "Test Subject",
		                "From" => "FromName <sender@domain.tld>", 
                        "X-Company" => "ReachMail"
                        ),

    // Plain text MIME part
	"BodyText"      => "This is the TEXT version of the Easy-SMTP API test",

    // HTML MIME part. Be sure to correctly escape quotes and meta-characters
	"BodyHtml"      => "This is the HTML version of the 
                        <a href=\"http://www.easy-smtp.com\">Easy-SMTP</a> 
                        API test",

    // Enable link tracking with a boolean value
	"Tracking"      => true 
);

// Send the message and store the response in a variable for easy access.
$es_campaign=$rmapi->rm_easySmtpDelivery($AccountId, $request);
?>
