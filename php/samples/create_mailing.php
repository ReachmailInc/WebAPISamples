<?php 
include_once('/path/To/Wrapper/rmapi.class.php');
# initilize class
$rmapi = new RMAPI('YourSecrEtTokenGo3sHeRe'); 
#request AccountID
$account_info = $rmapi->rm_administrationUsersCurrent(); 

#parse array and access the account id stdClass object value.  returns just the the account GUID as a string
$AccountId = $account_info['service_response']->AccountId;

#Build the request array. Wrapper will convert to JSON
#Endpoint documentation can be found here: https://services.reachmail.net/#resources/Mailings under the POST request details
$request = array (
        "Name" => "Maling Name",
        "Subject" => "The Subject of your email",
        "FromName" => "From Name",
        "FromEmail" => "from@email.com",
        "ReplyToEmail" => "reply@email.com",
        "MailingFormat" => "Html",
        "HtmlContent" => "This is test content for the Html MIME part of the email."
);

# print JSON payload as it will be requested (for testing purposes)
print_r(json_encode($request));
# POST mailing request
$response=$rmapi->rm_mailingsCreate($AccountId, $request);

#print response to screen
print_r($response);
?>