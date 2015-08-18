<?php
#include the api wrapper
include_once('/Users/mmarshall/GitHub/WebAPISamples/php/rmapi.class.php');

#initialize the rmapi class
$rmapi = new RMAPI('WeTzk0oPRtsdEFNvlo4FyDeo4VS34KSMqVGqJDrA0opU_CGM6fECMS6OMAMGedQ2');

#retrieve account information array 
$account_info = $rmapi->rm_administrationUsersCurrent();

#parse array and access the account id stdClass object value.  returns just the the account GUID as a string
$AccountId = $account_info['service_response']->AccountId;

$listId = '8107c1d2-1c04-e311-ab1a-d4ae5294c257';
$email = 'mmarshall@reachmail.com';

$r = array(
        "Properties" => [array(
                "Name" => "FullName",
                "Value" => "API Test 4", 
        )]
);

$change = $rmapi->rm_listsRecipientsModify($AccountId, $listId, $email, $r);

print_r($change);
?>

