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

def modify_group(credentials, acct_id, group_id):
	service = request.ContactService.GetGroup
	service_uri = service['uri'] % (str(acct_id), str(group_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def delete_group(credentials, acct_id, group_id):
	service = request.ContactService.GetGroup
	service_uri = service['uri'] % (str(acct_id), str(group_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def import_recipients(credentials, acct_id, list_id):
	service = request.ContactService.ImportRecipients
	service_uri = service['uri'] % (str(acct_id), str(list_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_import_status(credentials, acct_id, import_id):
	service = request.ContactService.GetImportStatus
	service_uri = service['uri'] % (str(acct_id), str(list_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def opt_in_recipient_from_list(credentials, acct_id, list_id,
	request_body, redirect=None):
	service = request.ContactService.OptInRecipientFromList
	if not redirect:
		service_uri = service['uri'] % (str(acct_id), str(list_id), '')
	else:
		service_uri = service['uri'] % (str(acct_id), str(list_id),
			str(redirect))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password, 
		request_body=request_body)
	return response

def opt_out_recipient_from_list(credentials, acct_id, list_id,
	request_body, redirect=None):
	service = request.ContactService.OptOutRecipientFromList
	if not redirect:
		service_uri = service['uri'] % (str(acct_id), str(list_id), '')
	else:
		service_uri = service['uri'] % (str(acct_id), str(list_id),
			str(redirect))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password, 
		request_body=request_body)
	return response

def enumerate_lists(credentials, acct_id, request_body):
	service = request.ContactService.EnumerateLists
	service_uri = service['uri'] % str(acct_id)
	response = reqeust.call(service_uri, service['method'], 
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_recipient(credentials, acct_id, list_id, email):
	service = request.ContactService.GetRecipient
	service_uri = service['uri'] % (str(acct_id), str(list_id), email)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def modify_recipient(credentials, acct_id, list_id, email):
	service = request.ContactService.ModifyRecipient
	service_uri = service['uri'] % (str(acct_id), str(list_id), email)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def delete_recipient(credenetials, acct_id, list_id, email):
	service = request.ContactService.DeleteRecipient
	service_uri = service['uri'] % (str(acct_id), str(list_id), email)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def create_recipient(credentials, acct_id, list_id, request_body,
	redirect=None):
	service = request.ContactService.CreateRecipient
	if not redirect:
		service_uri = service['uri'] % (str(acct_id), str(list_id), '')
	else:
		service_uri = service['uri'] % (str(acct_id), str(list_id),
			str(redirect))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password, 
		request_body=request_body)
	return response

def get_individual_recipient_by_query(credentials, acct_id, list_id,
	request_body):
	service = request.ContactService.GetIndividualRecipientByQuery
	service_uri = service['uri'] % (str(acct_id), str(list_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def delete_individual_recipient_by_query(credentials, acct_id, list_id,
	request_body):
	service = request.ContactService.DeleteIndividualRecipientByQuery
	service_uri = service['uri'] % (str(acct_id), str(list_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def modify_individual_recipient_by_query(credentials, acct_id, list_id,
	request_body, redirect_url=None):
	service = request.ContactService.ModifyIndividualRecipientByQuery
	if not redirect:
		service_uri = service['uri'] % (str(acct_id), str(list_id), '')
	else:
		service_uri = service['uri'] % (str(acct_id), str(list_id),
			str(redirect))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def enumerate_recipients(credentials, acct_id, list_id, request_body):
	service = request.ContactService.EnumerateRecipients
	serivce_uri = service['uri'] % (str(acct_id), str(list_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def delete_batch_recipients_by_query(credentials, acct_id, list_id,
	request_body):
	serivce = request.ContactService.DeleteBatchRecipientsByQuery
	service_uri = service['uri'] % (str(acct_id), str(list_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def modify_batch_recipients_by_query(credentials, acct_id, list_id,
	request_body):
	serivce = request.ContactService.ModifyBatchRecipientsByQuery
	service_uri = service['uri'] % (str(acct_id), str(list_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def modify_or_create_individual_recipient_by_query(credentials, acct_id,
	list_id, request_body, redirect=None):
	service = request.ContactService.ModifyOrCreateIndividualRecipientByQuery
	if not redirect:
		service_uri = service['uri'] % (str(acct_id), str(list_id), '')
	else:
		service_uri = service['uri'] % (str(acct_id), str(list_id),
			str(redirect))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def opt_in_recipient_from_account(credentials, acct_id, request_body,
	redirect=None):
	service = request.ContactService.OptInrecipientFromAccount
	if not redirect:
		service_uri = service['uri'] % (str(acct_id), str(list_id), '')
	else:
		service_uri = service['uri'] % (str(acct_id), str(list_id),
			str(redirect))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def opt_out_recipient_from_account(credentials, acct_id, request_body,
	redirect=None):
	service = request.ContactService.OptInrecipientFromAccount
	if not redirect:
		service_uri = service['uri'] % (str(acct_id), str(list_id), '')
	else:
		service_uri = service['uri'] % (str(acct_id), str(list_id),
			str(redirect))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response
