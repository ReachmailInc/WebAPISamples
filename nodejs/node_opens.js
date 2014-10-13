//Include the wrapper
var reachmail = require('./reachmailapi.js');

//Initialize the wrapper to the variable api using a token generated from the User Interface at https://go.reachmail.net
var api = new reachmail({token: 'YoUrSeCr3tTokenG03sH3rE'});

//Sets the date range for the report data request. String format is needed here.
var startdate='2014-10-01T00:00:00.000Z';
var enddate='2014-10-14T00:00:00.000Z';

/*
Function will retreive the account GUID, and only upon success, will continue to request data from date range
specified just above.
*/
api.AdministrationUsersCurrent(function (http_code, response) {
	if (http_code===200) {
		AccountId=response.AccountId; //extracts account GUID from response obj
		console.log("Success!  Account GUID: " + AccountId); //prints out the Account GUID
		//Next function send off the request for report data
		api.reportsEasySmtpOpens(AccountId, startdate, enddate, function (http_code, response) {
			if (http_code===200) {
				console.log("Openers Log Retrieved:");
				console.log(response);
			}else { 
				console.log("Oops, looks like an error in the report request. Status Code:" + http_code);
				console.log("Details: " + response);
			}
		});
	} else {
		console.log("Ooops, there was an error on the attempt to retreive the account GUID.  Status Code: " + http_code);
		console.log("Details: " + response);
	}
});
