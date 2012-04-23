# Functions to access the DataService group
from reachmailapi import request

def upload(credentials, request_body):
	service = request.DataService.Upload
	service_uri = service['uri']
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def download(credentials, data_id):
	service = request.DataService.Download
	service_uri = service['uri'] % str(data_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def download_file(credentials, data_id, filename):
	service = request.DataService.DownloadFile
	service_uri = service['uri'] % (str(data_id), str(filename))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def exists(credentials, data_id):
	service = request.DataService.DownloadFile
	service_uri = service['uri'] % str(data_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response
