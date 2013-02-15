# Functions for accessing the ReportService group
from reachmailapi import request

def get_bounce_detail_report(credentials, acct_id, mailing_id, request_body):
	service = request.ReportService.GetBounceDetailReport
	service_uri = service['uri'] % (str(acct_id), str(mailing_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_optout_detail_report(credentials, acct_id, mailing_id, request_body):
	service = request.ReportService.GetOptOutDetailReport
	service_uri = service['uri'] % (str(acct_id), str(mailing_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_read_detail_report(credentials, acct_id, mailing_id, request_body):
	service = request.ReportService.GetReadDetailReport
	service_uri = service['uri'] % (str(acct_id), str(mailing_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_tracked_link_detail_report(credentials, acct_id, mailing_id, 
	request_body):
	service = request.ReportService.GetTrackedLinkDetailReport
	service_uri = service['uri'] % (str(acct_id), str(mailing_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_mailing_report(credentials, acct_id, mailing_id):
	service = request.ReportService.GetMailingReport
	service_uri = service['uri'] % (str(acct_id), str(mailing_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def enumerate_mailing_Reports(credentials, acct_id, request_body):
	service = request.ReportService.EnumerateMailingReports
	service_uri = service['uri'] % str(acct_id)
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password,
		request_body=request_body)
	return response

def get_tracked_link_report_by_mailing_id(credentials, acct_id, mailing_id):
	service = request.ReportService.GetTrackedLinkReportByMailingId
	service_uri = service['uri'] % (str(acct_id), str(mailing_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def get_tracked_link_report_by_mailing_list_id(credentials, acct_id, 
	mailing_id, list_id):
	service = request.ReportService.GetTrackedLinkReportByMailingListId
	service_uri = service['uri'] % (str(acct_id), str(mailing_id),
		str(list_id))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response

def enumerate_easysmtp_mailings(credentials, acct_id, end_date, start_date):
	service = request.ReportService.EnumerateEasySMTPMailings
	service_uri = service['uri'] % (str(acct_id), str(end_date), 
		str(start_date))
	response = request.call(service_uri, service['method'],
		credentials.api_user, credentials.password)
	return response
