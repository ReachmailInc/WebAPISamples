#!/usr/bin/python27

import reachmail
import json

#following function returns the account GUID after checking for a successful connection
def getAccountGuid(api):
	res = api.adminsitration.users_current()
	if res[0] == 200 :
		data=json.loads(res[1]) #parse json response
		return data['AccountId']
	else:
		print "Oops. Could not find your Account Guid. \nStatus Code: %s \nResponse: %s" % (res[0], res[1])
		exit(1)

#this functions builds the body of the message and returns the response after checking the connection
def sendEmail(api, AccountId):
	body={
	'FromAddress': 'from@domain.tld',
	'Recipients': [
	{
		'Address': 'rcpt@domain.tld'
        },
	{
		'Address': 'rcpt2@domain.tld'
	}
	],
  	'Headers': { 
		'Subject': 'Test Subject Goes Here' , 
		'From': 'From Name <from@domain.tld>', 
		'X-Company': 'Company Name', 
		'X-Location': 'Your Location Header' 
	}, 
	'BodyText': 'this is the text version of the ES API test',
	'BodyHtml': 'this is the <a href=\"http://www.reachmail.com\">HTML</a> version of the ES API test', 
	'Tracking': 'true'
	}
	send = api.easysmtp.delivery(AccountId=AccountId, Data=body)
	if send[0] == 200:
		return send[1] 
	else:
		print "Could not Deliver message.  \nStatus Code: %s \nResponse: %s" % (send[0], send[1])
		exit(1)
	
def run():

	api = reachmail.ReachMail("YoUrSeCr3tTokenG03sH3rE")
	AccountId=getAccountGuid(api)
	send=sendEmail(api, AccountId)
	print "Messgae Sent. \nResponse: %s" % send
	

if __name__ == '__main__':
        run()
