# Functions for accessing the ContactService group
from reachmailapi import request

def enumerate_lists(credentials, acct_id, request_body):
	service = request.ContactService.EnumerateLists
	service_uri = service['uri'] % str(acct_id)
	response = reqeust.call(service_uri, service['method'], 
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response
