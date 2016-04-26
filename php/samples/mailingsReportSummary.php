<?php 
include_once('/path/To/Wrapper/rmapi.class.php'); 

# initilize class
$rmapi = new RMAPI('YourSecrEtTokenGo3sHeRe'); 

#request AccountID
$account_info = $rmapi->rm_administrationUsersCurrent();

#parse array and access the account id stdClass object value.  returns just the the account GUID as a string
$AccountId = $account_info['service_response']->AccountId;

#Build the request array For details on the potential filers, see here: https://services.reachmail.net/#Reports@/reports/mailings/summary
$request = array (
        "ScheduledDeliveryOnOrAfter" => "2016-03-20T05:21:00.0000000Z",
        "ScheduledDeliveryOnOrBefore" => "2016-03-30T05:21:00.0000000Z"
);

# Pull Report Summary
$reportSummary=$rmapi->rm_reportsMailingsSummary($AccountId, $request);
#print results to screen
print_r($reportSummary);
?>
