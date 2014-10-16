#!/usr/bin/python27

import reachmail
import json

def getAccountGuid(api):
	res = api.adminsitration.users_current()
	if res[0] == 200 :
		data=json.loads(res[1])
		return data['AccountId']
	else:
		print "Oops. Could not find your Account Guid.  \nStatus Code: %s \nResponse: %s" % (res[0], res[1])
		exit(1)

def sendEmail(api, AccountId):
	body={
	'FromAddress': 'billing@reachmail.com',
	'Recipients': [
	{
		'Address': 'mmarshall@reachmail.com'
        },
	{
		'Address': 'nmitchell@reachmail.com'
	}
	],
  	'Headers': { 
		'Subject': 'Test Subject Goes Here' , 
		'From': 'From Name', 
		'X-Company': 'Company Name', 
		'X-Location': 'Your Location Header' 
	}, 
	'BodyText': 'this is the text version of the ES API test',
	'BodyHtml': 'this is the HTML version of the ES API test', 
	'Tracking': 'true'
	}
	jbody=json.dumps(body)
	print jbody
	send = api.easysmtp.delivery(AccountId=AccountId, Data=jbody)
	print send
	

def run():

	api = reachmail.ReachMail("WeTzk0oPRtsdEFNvlo4FyDeo4VS34KSMqVGqJDrA0opU_CGM6fECMS6OMAMGedQ2")
	AccountId=getAccountGuid(api)
	send=sendEmail(api, AccountId)
	

if __name__ == '__main__':
        run()
