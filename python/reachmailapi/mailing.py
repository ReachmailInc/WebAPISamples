# Functions for accessing the MailingService group
from reachmailapi import request

def create_mailing(credentials, acct_id, request_body):
	service = request.MailingService.CreateMailing
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_mailing(credentials, acct_id, mailing_id):
	service = request.MailingService.GetMailing
	service_uri = service['uri'] % (str(acct_id), str(mailing_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def modify_mailing(credentials, acct_id, mailing_id, request_body):
	service = request.MailingService.ModifyMailing
	service_uri = service['uri'] % (str(acct_id), str(mailing_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def delete_mailing(credentials, acct_id, mailing_id):
	service = request.MailingService.DeleteMailing
	service_uri = service['uri'] % (str(acct_id), str(mailing_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def enumerate_groups(credentials, acct_id, request_body):
	service = request.MailingService.EnumerateGroups
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def create_group(credentials, acct_id, request_body):
	service = request.MailingService.CreateGroup
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_group(credentials, acct_id, group_id):
	service = request.MailingService.GetGroup
	service_uri = service['uri'] % (str(acct_id), str(group_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def modify_group(credentials, acct_id, group_id, request_body):
	service = request.MailingService.ModifyGroup
	service_uri = service['uri'] % (str(acct_id), str(group_id))
	response = request.call(service_url, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def delete_group(credentials, acct_id, group_id):
	service = request.MailingService.DeleteGroup
	service_uri = service['uri'] % (str(acct_id), str(group_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def enumerate_mailings(credentials, acct_id, request_body):
	service = request.MailingService.EnumerateMailings
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def create_mailing_template(credentials, acct_id, request_body):
	service = request.MailingService.CreateMailingTemplate
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_mailing_template(credentials, acct_id, template_id):
	service = request.MailingService.GetMailingTemplate
	service_uri = service['uri'] % (str(acct_id), str(template_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def modify_mailing_template(credentials, acct_id, template_id, request_body):
	service = request.MailingService.ModifyMailingTemplate
	service_uri = service['uri'] % (str(acct_id), str(template_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def delete_mailing_template(credentials, acct_id, template_id):
	service = request.MailingService.DeleteMailingTemplate
	service_uri = service['uri'] % (str(acct_id), str(template_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def enumerate_template_groups(credentials, acct_id):
	service = request.MailingService.EnumerateTemplateGroups
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def create_template_group(credentials, acct_id, request_body):
	service = request.MailingService.CreateTemplateGroup
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_template_group(credentials, acct_id, group_id):
	service = request.MailingService.GetTemplateGroup
	service_uri = service['uri'] % (str(acct_id), str(template_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def modify_template_group(credentials, acct_id, group_id, request_body):
	service = request.MailingService.ModifyTemplateGroup
	service_uri = service['uri'] % (str(acct_id), str(group_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def delete_template_group(credentials, acct_id, group_id):
	service = request.MailingService.DeleteTemplateGroup
	service_uri = service['uri'] % (str(acct_id), str(template_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def enumerate_mailing_templates(credentials, acct_id):
	service = request.MailingService.EnumerateMailingTemplates
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

