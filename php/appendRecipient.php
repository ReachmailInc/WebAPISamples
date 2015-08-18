<?php
#include the api wrapper
require_once('rmapi.class.php');

#initialize the rmapi class
$rmapi = new RMAPI('vCbs427M88GhSt1iSHL5bO-uDuM3c5-OU6MmK6UP0RWgE1YuIvEaPJqTdAPUJIQ2');


#retrieve account information array 
$account_info = $rmapi->rm_administrationUsersCurrent();
print_r($account_info);
#parse array and access the account id stdClass object value.  returns just the the account GUID as a string
$AccountId = $account_info['service_response']->AccountId;
print_r($AccountId);die;

$r=$rmapi->rm_listsFiltered($AccountId);
//echo $r;die;
// retrieve list UUID by usin the post to /lists/filtered/{AccountId} or by using the rm_listsFiltered function from the php wrapper
$listId = '697254';

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

