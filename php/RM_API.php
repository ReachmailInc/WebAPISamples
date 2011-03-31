<?php
/*
Name: ReachMail API Library
Description: Provides a variety of functions to interact with and extend the ReachMail API.
Author: ReachMail Support
Version: 0.1.0
Author URI: http://www.reachmail.net/support
License: MIT License (see LICENSE) http://creativecommons.org/licenses/MIT/
Last Modified: March 31, 2011
Requirements: PHP 5 or higher.
*/

  // ensure Curl extension installed
	if (!extension_loaded("curl")) {
		throw(new Exception("The Curl extension for PHP is required for ReachMail API to work."));
}

/* Define Static Variables */
  // Administration Service
    var $base_url = 'https://services.reachmail.net/';
    var $get_user_url =  'https://services.reachmail.net/Rest/Administration/v1/users/current';
    var $queue_mailing_url = 'https://services.reachmail.net/Rest/Campaigns/v1/';
  // Contact Services
    var $enumerate_fields_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/query/';
    var $enumerate_lists_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/query/';
    var $create_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';
    var $get_list_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/';
    var $enumerate_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/query/';
    var $get_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients';
    var $create_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/recipients/';
    var $import_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/import/';
    var $export_recipients_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/export/';
    var $get_export_status_url = 'https://services.reachmail.net/Rest/Contacts/v1/lists/export/status/';
  //Mailing Services
    var $enumerate_mailings_url = 'https://services.reachmail.net/Rest/Content/Mailings/v1/query/';
    var $get_mailing_url = 'https://services.reachmail.net//Rest/Content/Mailings/v1/';
    var $delete_mailing_url = 'https://services.reachmail.net//Rest/Content/Mailings/v1/';
    var $create_mailing_url = 'https://services.reachmail.net/REST/Content/Mailings/v1/';
  //Report Services
    var $enumerate_mailing_reports_url = 'https://services.reachmail.net/Rest/Reports/v1/mailings/query/';
    var $get_mailing_report_url = 'https://services.reachmail.net/Rest/Reports/v1/mailings/';
    var $get_mailing_report_summary_url = 'https://services.reachmail.net/Rest/Reporting/Content/Mailings/v1/Summary/';
  //Data Services
    var $data_upload_url = 'https://services.reachmail.net/Rest/Data/';
    var $data_exist_url = 'https://services.reachmail.net/Rest/Data/exists/';
	
	class login{
	    private $account_key;
		private $username;
		private $password;
		
		function($account_key = NULL, $username = NULL, $password = NULL) {
					$this->account_key = $account_key;
					$this->username = $username;
					$this->password = $password;		
		}	
}
	
?>