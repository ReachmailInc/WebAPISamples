<?php 
include_once('../rmapi.class.php');

# initilize class
$rmapi = new RMAPI('YoUrSeCr3tTokenG03sH3rE'); 
# request AccountID
$account_info = $rmapi->rm_administrationUsersCurrent(); 

# parse array and access the account id stdClass object value.  
# returns just the the account GUID as a string
$AccountId = $account_info['service_response']->AccountId;

# create List

$list_request = array (
    "Name" => "Name of List here",
    "Type" => "Recipient",
    "Fields" => [
        "Email", "FirstName"
    ]
);

$list_response = $rmapi->rm_listsCreate($AccountId, $list_request);
$list_id = $list_response['service_response']->Id;


# read file in as string
$file_str = file_get_contents("/path/to/file/filename.csv");

# upload data
$data_response = $rmapi->rm_dataUpload($file_str);
$data_id = $data_response['service_response']->Id;

# build import request body
# If using "SourceFieldPosition" instead of "SourceFieldName" bear in mind that
# "SourceFieldPostion is a 1-indexed array
$import_request = array (
    "DataId" => $data_id,
    "FieldMappings" => [
        array (
            "DestinationFieldName" => "Email",
            "SourceFieldName" => "Email"
        ),
        array(
            "DestinationFieldName" => "FirstName",
            "SourceFieldName" => "FirstName"
        )
    ],
    "ImportOptions" => array(
        "Format" => "CharacterSeperated",
        "HeaderRow" => True,
        "SkipRecordsWithErrors" => True,
        "AllowStringTruncation" => True,
        "AbortImportOnError" => False,
        "CharacterSeperatedOptions" => array (
            "Delimiter" => "Comma"
        )
    )
);

# submit import
$import_response = $rmapi->rm_listsImport($AccountId, $list_id, $import_request);

# show import response
print_r($import_response);

?>