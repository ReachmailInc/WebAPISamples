# Functions for accessing the Administation service group
from reachmailapi import request 

def enumerate_addresses(credentials, account_id):
        service = request.Administration.GetCurrentUser
        response = request.call(service['uri'] % account_id, service['method'],
                credentials.api_user, credentials.password)
        return response

def get_current_user(credentials):
	service = request.Administration.GetCurrentUser 
	response = request.call(service['uri'], service['method'], 
		credentials.api_user, credentials.password)
	return response
