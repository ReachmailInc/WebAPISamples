<?php

#include the api wrapper
include_once('rmapi.class.php');

#initialize the rmapi class
$rmapi = new RMAPI('YoUrSeCr3tTokenG03sH3rE');

#retrieve account information array 
$account_info = $rmapi->rm_administrationUsersCurrent();

#parse array and access the account id stdClass object value.  returns just the the account GUID as a string
$AccountId = $account_info['service_response']->AccountId;

#Dates are in UTC format and must be set as strings
$startdate='2014-09-20T00:00:00.000Z';
$enddate='2014-09-29T00:00:00.000Z';

$es_Opens=$rmapi->rm_reportsEasySMTPOpens($AccountId, $startdate, $enddate);
?>
