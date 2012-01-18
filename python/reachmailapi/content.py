# Functions for accessing the ContentLibraryService group
from reachmailapi import request

def add_file(credentials, acct_id, data_id, request_body=None):
	service = request.ContentLibraryService.AddFile
	service_uri = service['uri'] % (str(acct_id), str(data_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_file(credentials, acct_id, file_id):
	service = request.ContentLibraryService.GetFile
	service_uri = service['uri'] % (str(acct_id), str(file_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def modify_file(credentials, acct_id, file_id, request_body=None):
	service = request.ContentLibraryService.ModifyFile
	service_uri = service['uri'] % (str(acct_id), str(file_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def delete_file(credentials, acct_id, file_id):
	service = request.ContentLibraryService.DeleteFile
	service_uri = service['uri'] % (str(acct_id), str(file_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def enumerate_files(credentials, acct_id, request_body=None):
	service = request.ContentLibraryService.EnumerateFiles
	service_uri = service['uri'] % (str(acct_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def create_folder(credentials, acct_id, request_body=None):
	service = request.ContentLibraryService.CreateFolder
	service_uri = service['uri'] % (str(acct_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_folder(credentials, acct_id, folder_id):
	service = request.ContentLibraryService.GetFolder
	service_uri = service['uri'] % (str(acct_id), str(folder_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def modify_folder(credentials, acct_id, folder_id, request_body=None):
	service = request.ContentLibraryService.ModifyFolder
	service_uri = service['uri'] % (str(acct_id), str(folder_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def delete_folder(credentials, acct_id, folder_id):
	service = request.ContentLibraryService.DeleteFolder
	service_uri = service['uri'] % (str(acct_id), str(folder_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def enumerate_folders(credentials, acct_id, request_body=None):
	service = request.ContentLibraryService.EnumerateFolders
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response
