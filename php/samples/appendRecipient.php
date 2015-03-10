<?php
#include the api wrapper
include_once('/path_to_wrapper/rmapi.class.php');

#initialize the rmapi class
$rmapi = new RMAPI('YoUrSeCr3tTokenG03sH3rE');


#retrieve account information array 
$account_info = $rmapi->rm_administrationUsersCurrent();

#parse array and access the account id stdClass object value.  returns just the the account GUID as a string
$AccountId = $account_info['service_response']->AccountId;

// retrieve list UUID by usin the post to /lists/filtered/{AccountId} or by using the rm_listsFiltered function from the php wrapper
$listId = 'LIST UUID GOES HERE';

$r = array(
	"Email" => "test@reachmail.com",
        "Properties" => [array(
                "Name" => "FullName",
                "Value" => "API Test Append", 
        )]
);

$response = $rmapi->rm_listsRecipientsCreate($AccountId, $listId, $r);

print_r($response);
?>

