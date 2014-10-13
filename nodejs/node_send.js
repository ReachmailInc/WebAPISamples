//Include the wrapper
var reachmail = require('./reachmailapi.js');

//inialize the wrapper to the variable api using a token generated from the User Interface at https://go.reachmail.net
var api = new reachmail({token: 'YoUrSeCr3tTokenG03sH3rE'});

//The following builds the content of the message
var body={
	FromAddress: 'billing@reachmail.com',
	Recipients: [
	{
		Address: 'mmarshall@reachmail.com'
        },
	{
		Address: 'nmitchell@reachmail.com'
	}
	],
  	Headers: { 
		Subject: 'Test Subject Goes Here' , 
		From: 'ReachMail Billing <billing@reachmail.com>', 
		'X-Company': 'Company Name', 
		'X-Location': 'Chicago' 
	}, 
	BodyText: 'this is the text version of the ES API test',
	BodyHtml: 'this is the <a href=\"http://www.google.com\">HTML</a> version of the ES API test', 
	Tracking: true
};
//JSON encode the message body for transmission
jsonBody = JSON.stringify(body);

/* 
The function below retreieves the account GUID. Only when succefful will the 
function proceed to them schedule the message for delivery.
Information is printed to screen through the use of console.log(...)
*/
api.AdministrationUsersCurrent(function (http_code, response) {
	if (http_code===200) {
		AccountId=response.AccountId; //extracts account GUID from response obj
		console.log("Success!  Account GUID: " + AccountId); //prints out the Account GUID
		//Next Function sends the message
		api.easySmtpDelivery(AccountId, jsonBody, function (http_code, response) {
			if (http_code===200) {
				console.log("successful connection to EasySMTP API");
				console.log(response);
			}else { 
				console.log("Oops, looks like an error on send. Status Code: " + http_code);
				console.log("Details: " + response);
			}
		});
	} else {
		console.log("Oops, there was an error when trying to get the account GUID. Status Code: " + http_code);
		console.log("Details: " + response);
	}
});
