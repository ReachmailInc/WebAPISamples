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

#this function gets the report and returns the response for correctly executed calls
def getDeliveryReport(api, AccountId):
	startdate = '2014-10-01T00:00:00.000Z'
	enddate = '2014-10-14T00:00:00.000Z'
	report = api.reports.easysmtp_mailings(AccountId=AccountId, startdate=startdate, enddate=enddate)
	if report[0] == 200:
		return report[1] 
	else:
		print "Could not retrieve report.  \nStatus Code: %s \nResponse: %s" % (report[0], report[1])
		exit(1)
	
def run():

	api = reachmail.ReachMail("WeTzk0oPRtsdEFNvlo4FyDeo4VS34KSMqVGqJDrA0opU_CGM6fECMS6OMAMGedQ2")
	AccountId=getAccountGuid(api)
	res=getDeliveryReport(api, AccountId)
	print "Report Retrieved. \nResponse: %s" % res 
	

if __name__ == '__main__':
        run()
