<?php
#include the api wrapper
include_once('/path_to_wrapper/rmapi.class.php');

#initialize the rmapi class
$rmapi = new RMAPI('YoUrSeCr3tTokenG03sH3rE');

#retrieve account information array 
$account_info = $rmapi->rm_administrationUsersCurrent();

#parse array and access the account id stdClass object value.  returns just the the account GUID as a string
$AccountId = $account_info['service_response']->AccountId;

$listId = 'YoUr-SeCr3t-L1sTGUID-G03s-H3rE'; # list GUID in question
# to retrieve your list GUID, see here: http://services.reachmail.net/#Lists@/lists/filtered

$email = 'email@domain.tld'; #email address to be modified


# request body below will change the name field 
# for a full list of fields see here:
# http://support.reachmail.net/hc/en-us/articles/201866473-List-Field-Information
$r = array(
        "Properties" => [array(
                "Name" => "FullName",
                "Value" => "API Test 4", 
        )]
);

$change = $rmapi->rm_listsRecipientsModify($AccountId, $listId, $email, $r);
?>

