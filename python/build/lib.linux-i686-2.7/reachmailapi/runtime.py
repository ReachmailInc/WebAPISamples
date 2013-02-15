# Functions for accessing RuntimeService group
from reachmailapi import request

def get_info(credentials):
	service = request.RuntimeService.GetInfo
	response = request.call(service['uri'], service['method'],
		credentials.api_user, credentials.password)
	return response
