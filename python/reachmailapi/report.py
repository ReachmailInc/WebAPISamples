# Functions for accessing the ReportService group
from reachmailapi import request

def enumerate_mailings(credentials, acct_id, request_body):
	service = request.ReportService.EnumerateMailings
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response
