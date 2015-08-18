<?php 
/*
Name: ReachMail API Library
Description: ReachMail API client library 
Author: Dan Nielsen | support@reachmail.com 
Version: 1.2 
Requirements: PHP 5 or higher and the Curl Extension
Usage:
    include_once('rmapi.class.php'); 
    $rmapi = new RMAPI('your-api-token');
    $results = $rmapi->rm_functionName($parameter1='xx', $parameter2='yy');
All functions return an array including the HTTP status code of the request
and a JSON encoded string of the request response. Any service which requires
a request body will accept that body only in as a JSON encoded string.
Please consult the notes in each function for more information on usage and
their specific use.
Copyright (C) 2013 ReachMail, Inc. / Dan Nielsen
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.
This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with this program. If not, see <http://www.gnu.org/licenses/>.
*/
if(!extension_loaded("curl")) {
	throw(new Exception("The cURL extension for PHP is required."));
}
$header = "";
class RMAPI{
	private $_token;
    private $_httpa;
	function __construct($_token=null) {
		$this->_token = $_token;
        $this->svcbase = "https://services.reachmail.net";
	}
    function requestBase($uri, $RequestBody, $request_method) {
        global $header;
        $url = $this->svcbase . $uri;
        $curl_headers = array("Content-Type: application/json",
                "Accept: application/json",
                sprintf("Authorization: token %s", $this->_token));
        // Defaults
        $curl_defaults = array(
            CURLOPT_URL => $url,
            CURLOPT_HEADER => false,
            CURLOPT_RETURNTRANSFER => true,
            CURLOPT_HTTPHEADER => $curl_headers,
	    CURLOPT_HEADERFUNCTION => "HandleHeaderLine"
        );
	
        $request = curl_init();
        switch($request_method) {
            
            case "GET":
                $curl_options = $curl_defaults;
                break;
            case "POST":
                $curl_options = $curl_defaults;
                $curl_options[CURLOPT_POSTFIELDS] = json_encode($RequestBody);
                $curl_options[CURLOPT_POST] = true;
                break;
            case "PUT":
                $curl_options = $curl_defaults;
                $curl_options[CURLOPT_POSTFIELDS] = json_encode($RequestBody);
                $curl_options[CURLOPT_CUSTOMREQUEST] = $request_method;
                break;
            case "DELETE":
                $curl_options = $curl_defaults;
                $curl_options[CURLOPT_POSTFIELDS] = $RequestBody;
                $curl_options[CURLOPT_CUSTOMREQUEST] = $request_method;
                break;
        }
	
        curl_setopt_array($request, $curl_options);

	if (!function_exists('HandleHeaderLine')){	
	function HandleHeaderLine( $request, $header_line ) {
		global $header;
		if (preg_match('/status-text/', $header_line)) {
			$header .= substr($header_line, 13); // or do whatever
			$header = str_replace(array("\r", "\n"), "", $header);
		}
                return strlen($header_line);
        }}
	
        $response = json_decode(curl_exec($request));
        $http_status = curl_getinfo($request, CURLINFO_HTTP_CODE);
        curl_close($request);
        return array(
            "http_status" => $http_status,
	    "http_status_text" => $header,
            "service_response" => $response
        );
    }
    function rm_administrationUsersCurrent() {
    
        // Required for nearly all other operations, this function 
        // returns the account id validated with your credentials
        $uri = "/administration/users/current";
        return $this->requestBase($uri, null, "GET"); 
    }
    function rm_administrationAddresses($AccountId=null) {
        // Returns all CANSPAM compliance addresses saved in an account.
        $uri = sprintf("/administration/addresses/%s", $AccountId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_campaigns($AccountId=null, $RequestBody=null) {
        
        // Schedules a campaign
        $uri = sprintf("/campaigns/%s", $AccountId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_campaignsMessageTesting($AccountId=null, $RequestBody=null) {
        // Schedules a message testing campaign
        $uri = sprintf("/campaigns/messagetesting/%s", $AccountId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_campaignsMessageTestingComplete($AccountId=null,
            $CampaignId=null, $RequestBody=null) {
        // Completes an in-queue message testing
        $uri = sprintf("/campaigns/messagetesting/%s/%s", $AccountId,
                $CampaignId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_dataUpload($RequestBody=null) {
        
        // Uploads data, i.e. a list file, image, etc. 
        // In this function $RequestBody should be a byte-stream representation
        // of the data to be uploaded
        $uri = "/data";
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_dataDownload($DataId=null) {
        // Downloads the specified data id.
        $uri = sprintf("/data/%s", $DataId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_easySmtpDelivery($AccountId=null, $RequestBody=null) {
        $uri = sprintf("/easysmtp/%s", $AccountId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_listsCreate($AccountId=null, $RequestBody=null) {
        // Creates a new, empty list
        $uri = sprintf("/lists/%s", $AccountId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_listsInformation($AccountId=null, $ListId=null) {
        // Returns information about the specified list
        $uri = sprintf("/lists/%s/%s", $AccountId, $ListId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_listsModify($AccountId=null, $ListId=null,
            $RequestBody=null) {
        // Modifies the specified list
        $uri = sprintf("/lists/%s/%s", $AccountId, $ListId);
        return $this->requestBase($uri, $RequestBody, "PUT");
    }
    function rm_listsDelete($AccountId=null, $ListId=null) {
        // Deletes the specified list
        $uri = sprintf("/lists/%s/%s", $AccountId, $ListId);
        return $this->requestBase($uri, null, "DELETE");
    }
    function rm_listsExport($AccountId=null, $ListId=null,
            $RequestBody=null) {
        // Exports the specified list
        $uri = sprintf("/lists/export/%s/%s", $AccountId, $ListId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_listsExportStatus($AccountId=null, $ExportId=null) {
        
        // Returns the status for the specified export request
        $uri = sprintf("/lists/export/%s/%s", $AccountId, $ExportId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_listsFields($AccountId=null) {
        
        // Returns the fields available to the specified account
        $uri = sprintf("/lists/fields/%s", $AccountId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_listsFiltered($AccountId=null, $RequestBody=null) {
        
        // Returns a filtered set of all lists in the specified account
        $uri = sprintf("/lists/filtered/%s", $AccountId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_listsGroups($AccountId=null) {
        // Returns a set of all list groups in the specified account
        $uri = sprintf("/lists/groups/%s", $AccountId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_listsGroupsCreate($AccountId=null, $RequestBody=null) {
        // Creates a new list group
        $uri = sprintf("/lists/groups/%s", $AccountId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_listsGroupsInformation($AccountId=null, $GroupId=null) {
        // Returns information about the specified group
        $uri = sprintf("/lists/groups/%s/%s", $AccountId, $GroupId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_listsGroupsModify($AccountId=null, $GroupId=null,
            $RequestBody=null) {
        // Modifies the specified group
        $uri = sprintf("/lists/groups/%s/%s", $AccountId, $GroupId);
        return $this->requestBase($uri, $RequestBody, "PUT");
    }
    function rm_listsGroupsDelete($AccountId=null, $GroupId=null) {
        // Deletes the specified group
        $uri = sprintf("/lists/groups/%s/%s", $AccountId, $GroupId);
        return $this->requestBase($uri, null, "DELETE");
    }
    function rm_listsImport($AccountId=null, $ListId=null,
            $RequestBody=null) {
        // Imports data into a list
        $uri = sprintf("/lists/import/%s/%s", $AccountId, $ListId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_listsImportStatus($AccountId=null, $ImportId=null) {
        // Returns the status of the specified import
        $uri = sprintf("/lists/import/status/%s/%s", $AccountId, $ImportId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_listsOptout($AccountId=null, $ListId=null,
            $RequestBody=null) {
        // Opt-out a recipient from a list
        $uri = sprintf("/lists/optout/%s/%s", $AccountId, $ListId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_listsRecipientsCreate($AccountId=null, $ListId=null,
            $RequestBody=null) {
        // Add recipients to the specified list
        $uri = sprintf("/lists/recipients/%s/%s", $AccountId, $ListId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_listsRecipientsInformation($AccountId=null, $ListId=null,
            $Email=null) {
        // Returns information on the specified Email address, list specific
        $uri = sprintf("/lists/recipients/%s/%s/%s", $AccountId, $ListId,
                $Email);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_listsRecipientsDelete($AccountId=null, $ListId=null,
            $Email=null) {
        // Deletes specified recipient, list specific
        $uri = sprintf("/lists/recipients/%s/%s/%s", $AccountId, $ListId,
                $Email);
        return $this->requestBase($uri, null, "DELETE");
    }
    function rm_listsRecipientsModify($AccountId=null, $ListId=null,
            $Email=null, $RequestBody=null) {
        // Modifies the specified recipient, list specific
        $uri = sprintf("/lists/recipients/%s/%s/%s", $AccountId, $ListId,
                $Email);
        return $this->requestBase($uri, $RequestBody, "PUT");
    }
    function rm_listsRecipientsFiltered($AccountId=null, $ListId=null,
            $RequestBody=null) {
        // Returns a filtered set of recipients
        $uri = sprintf("/lists/recipients/filtered/%s/%s", $AccountId, 
                $ListId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_listsRecipientsFilteredDelete($AccountId=null, $ListId=null,
            $RequestBody=null) {
        // Deletes recipients by filter application
        $uri = sprintf("/lists/recipients/filtered/delete/%s/%s", $AccountId,
                $ListId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_listsRecipientsFilteredModify($AccountId=null, $ListId=null,
            $RequestBody=null) {
        // Modifies recipients by filter application
        $uri = sprintf("/lists/recipients/filtered/modify/%s/%s", $AccountId,
                $ListId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_listsRecipientsSubscribe($AccountId=null, $ListId=null,
            $RequestBody=null) {
        // Subscribes a recipient, list specific 
        $uri = sprintf("/lists/recipients/subscribe/%s/%s", $AccountId,
                $ListId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_listsSubscrptionForm($AccountId=null,
            $SubscriptionFormId=null) {
        // Returns subscription form HTML code
        $uri = sprintf("/lists/subscriptionform/%s/%s", $AccountId,
                $SubscriptionFormId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_listsSubscriptionFormFiltered($AccountId=null,
            $RequestBody=null) {
        // Returns filtered set of subscription forms in the specified account
        $uri = sprintf("/lists/subscriptionform/filtered/%s", $AccountId);
        return $this->requestBase($uri, $reuqest_body, "POST");
    }
    function rm_mailingsCreate($AccountId=null, $RequestBody=null) {
        // Creates a mailing
        $uri = sprintf("/mailings/%s", $AccountId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_mailingsInformation($AccountId=null, $MailingId=null) {
        // Returns information on the specified mailing
        $uri = sprintf("/mailings/%s/%s", $AccountId, $MailingId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_mailingsModify($AccountId=null, $MailingId=null, 
            $RequestBody=null) {
        // Modifies the specified mailing
        $uri = sprintf("/mailings/%s/%s", $AccountId, $MailingId);
        return $this->requestBase($uri, $RequestBody, "PUT");
    }
    function rm_mailingsDelete($AccountId=null, $MailingId=null) {
        //Deletes the specified mailing
        $uri = sprintf("/mailings/%s/%s", $AccountId, $MailingId);
        return $this->requestBase($uri, null, "DELETE");
    }
    function rm_mailingsFiltered($AccountId=null, $RequestBody=null) {
        // Returns a filtered set of mailings
        $uri = sprintf("/mailings/filtered/%s", $AccountId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_mailingsGroups($AccountId=null) {
        // Returns mailing groups in the specified account
        $uri = sprintf("/mailings/groups/%s", $AccountId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_mailingsGroupsCreate($AccountId=null, $RequestBody=null) {
        // Create a new mailing group
        $uri = sprintf("/mailings/groups/%s", $AccountId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_mailingsGroupsInformation($AccountId=null, $GroupId=null) {
        // Returns specified group information
        $uri = sprintf("/mailings/groups/%s/%s", $AccountId, $GroupId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_mailingsGroupsModify($AccountId=null, $GroupId=null,
            $request_id=null) {
        // Modifies the specified group
        $uri = sprintf("/mailings/groups/%s/%s", $AccountId, $GroupId);
        return $this->requestBase($uri, $RequestBody, "PUT");
    }
    function rm_mailingsGroupsDelete($AccountId=null, $GroupId=null) {
        // Returns specified group information
        $uri = sprintf("/mailings/groups/%s/%s", $AccountId, $GroupId);
        return $this->requestBase($uri, null, "DELETE");
    }
    function rm_reportsMailingsBouncesDetail($AccountId=null,
            $MailingId=null, $RequestBody=null) {
        // Returns bounce information from the specified mailing
        $uri = sprintf("/reports/mailings/bounces/detail/%s/%s", $AccountId,
                $MailingId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_reportsMailingsDetail($AccountId=null, $RequestBody=null) {
        // Returns a filtered set of mailing reports
        $uri = sprintf("/reports/mailings/detail/%s", $AccountId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_reportsMailingsDetailInformation($AccountId=null,
            $MailingId=null) {
        // Returns detailed information on the specified mailing
        $uri = sprintf("/reports/mailings/detail/%s/%s", $AccountId,
                $MailingId);
        return $this->requestBase($uri, null, "GET");
    }
    function reportsMailingsMessageTestingFiltered($AccountId=null,
            $RequestBody=null) {
        // Returns a filtered set of message tetsing mailing reports
        $uri = sprintf("/reports/mailings/messagetesting/%s", $AccountId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_reportsMailingsOpensDetail($AccountId=null,
            $MailingId=null, $RequestBody=null) {
        // Returns open information from the specified mailing
        $uri = sprintf("/reports/mailings/opens/detail/%s/%s", $AccountId,
                $MailingId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_reportsMailingsOptouts($AccountId=null,
            $MailingId=null, $RequestBody=null) {
        // Returns opt-out details from the specified mailing
        $uri = sprintf("/reports/mailings/optouts/detail/%s/%s", $AccountId,
                $MailingId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_reportsMailingsTrackedLinksDetail($AccountId=null,
            $MailingId=null, $RequestBody=null) {
        // Returns link tracking details from the specified mailing
        $uri = sprintf("/reports/mailings/trackedlinks/detail/%s/%s", 
                $AccountId, $MailingId);
        return $this->requestBase($uri, $RequestBody, "POST");
    }
    function rm_reportsMailingsTrackedLinksSummary($AccountId=null,
            $MailingId=null) {
        // Returns link tracking summaries from the specified mailing
        $uri = sprintf("/reports/mailings/trackedlinks/summary/%s/%s", 
                $AccountId, $MailingId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_reportsMailingsTrackedLinksSummaryByList($AccountId=null,
            $MailingId=null, $ListId=null) {
        // Returns link tracking summaries from the specified mailing,
        // list specific
        $uri = sprintf("/reports/mailings/trackedlinks/summary/%s/%s/%s", 
                $AccountId, $MailingId, $ListId);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_reportsEasySMTPMailings($AccountId=null, $startdate=null,
            $enddate=null) {
        // Returns details of easysmtp mailings
        $uri = sprintf("/reports/easysmtp/%s/?enddate=%s&startdate=%s",
                $AccountId, $enddate, $startdate);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_reportsEasySMTPBounces($AccountId=null, $startdate=null,
            $enddate=null) {
        $uri = sprintf("/reports/easysmtp/bounces/%s/?enddate=%s&startdate=%s",
                $AccountId, $enddate, $startdate);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_reportsEasySMTPOptouts($AccountId=null, $startdate=null,
            $enddate=null) {
        $uri = sprintf("/reports/easysmtp/optouts/%s/?enddate=%s&startdate=%s",
                $AccountId, $enddate, $startdate);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_reportsEasySMTPOpens($AccountId=null, $startdate=null,
            $enddate=null) {
        $uri = sprintf("/reports/easysmtp/opens/%s/?enddate=%s&startdate=%s",
                $AccountId, $enddate, $startdate);
        return $this->requestBase($uri, null, "GET");
    }
    function rm_reportsEasySMTPClicks($AccountId=null, $startdate=null,
            $enddate=null) {
        $uri = sprintf("/reports/easysmtp/clicks/%s/?enddate=%s&startdate=%s",
                $AccountId, $enddate, $startdate);
        return $this->requestBase($uri, null, "GET");
    }
}
?>
