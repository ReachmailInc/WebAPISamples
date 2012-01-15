# Functions for accessing the ContactService group
from reachmailapi import request

def create_list(credentials, acct_id, request_body):
	service = request.ContactService.CreateList
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_list(credentials, acct_id, list_id):
	service = request.ContactService.GetList
	service_uri = service['uri'] % (str(acct_id), str(list_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def modify_list(credentials, acct_id, list_id, request_body):
	service = request.ContactService.ModifyList
	service_uri = service['uri'] % (str(acct_id), str(list_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def delete_list(credentials, acct_id, list_id):
	service = request.ContactService.DeleteList
	service_uri = service['uri'] % (str(acct_id), str(list_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def export_recipients(credentials, acct_id, list_id, request_body):
	service = request.ContactService.ExportRecipients
	service_uri = service['uri'] % (str(acct_id), str(list_id))
	reposnse = request.call(service_uri, service['method'], 
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_export_status(credentials, acct_id, export_id):
	service = request.ContactService.GetExportStatus
	service_uri = service['uri'] % (str(acct_id), str(export_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def enumerate_fields(credentials, acct_id):
	service = request.ContactService.EnumerateFields
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def enumerate_groups(credentials, acct_id):
	service = request.ContactService.EnumerateGroups
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def create_group(credentials, acct_id, request_body):
	service = request.ContactService.CreateGroup
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		crdentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_group(credentials, acct_id, group_id):
	service = request.ContactService.GetGroup
	service_uri = service['uri'] % (str(acct_id), str(group_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response
	
def enumerate_lists(credentials, acct_id, request_body):
	service = request.ContactService.EnumerateLists
	service_uri = service['uri'] % str(acct_id)
	response = reqeust.call(service_uri, service['method'], 
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response
