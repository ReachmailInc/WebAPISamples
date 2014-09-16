<?php

#include the api wrapper
include_once('./rmapi.class.php');

#initialize the rmapi class
$rmapi = new RMAPI('WeTzk0oPRtsdEFNvlo4FyDeo4VS34KSMqVGqJDrA0opU_CGM6fECMS6OMAMGedQ2');


#retrieve account information array 
$account_info = $rmapi->rm_administrationUsersCurrent();

#parse array and access the account id stdClass object value.  returns just the the account GUID as a string
$AccountId = $account_info['service_response']->AccountId;

#create a list

$listRequest = array (
	"Name" => "MZM API LIST5",
	"Type" => "Recipient",
	"Fields" => array (
		"Email",
		"FirstName",
		"LastName",
		"Zip"
	)
);

$listResponse = $rmapi->rm_listsCreate($AccountId, $listRequest);
$listId=$listResponse['service_response']->Id;

print_r("list uploaded.  Id is: " . $listId . "\n");


#upload the file
$file=file_get_contents('test.csv'); 
$dataResponse = $rmapi->rm_dataUpload($file);
$dataId = $dataResponse['service_response']->Id;

print_r("File is uploaded.  Data Id is: " . $dataId . "\n");

#import the file from the Data resourse to the List we created above

$listRequest = array (
	"DataId" => $dataId,
	"FieldMappings:" => [array(
		"DesitinationFieldName" => "Email",
		"SourceFieldPosition" => 1	
	), array(
		"DesitinationFieldName" => "FirstName",
		"SourceFieldPosition" => 2
	), array(
		"DesitinationFieldName" => "LastName",
		"SourceFieldPosition" => 3
	), array(
		"DesitinationFieldName" => "Zip",
		"SourceFieldPosition" => 4
	)],
	"ImportOptions" => array(
		"Format" => "CharacterSeperated",
		"HeaderRow" => true,
		"SkipRecordsWithErrors" => true,
		"CharacterSeperatedOptions" => array(
			"Delimiter" => "Comma"
		)
	)	
);

$importResponse = $rmapi->rm_listsImport($AccountId, $listId, $listRequest);
$importId = $importResponse['service_response']->Id;
print_r($importResponse);
print_r($importId);

?>
