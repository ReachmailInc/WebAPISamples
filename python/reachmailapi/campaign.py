# Functions for accessing the CampaignService group
from reeachmailapi import request

def queue_mailing(credentials, account_id, request_body):
	service = request.CampaignService.QueueMailing
	response = request.call(service['uri'], service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response
