# Functions for accessing the Administation service group
from reachmailapi import request 

def get_current_user(credentials):
	service = request.Administration.GetCurrentUser 
	response = request.call(service['uri'], service['method'], 
		credentials.api_user, credentials.password)
	return response
