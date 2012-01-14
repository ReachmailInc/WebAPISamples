# Functions to call Reachmail API services
import urllib2

class Administration(object):
	base = 'https://services.reachmail.net/Rest/Administration/v1'
	GetCurrentUser = {
		'uri': base + '/users/current',
		'method':'GET'
	}

class CampaignService(object):
	base = 'https://services.reachmail.net/Rest/Campaigns/v1'
	QueueMailing = {
		'uri': base + '/%s/queue',
		'method':'POST'
	}

class ContactService(object):
	base = 'https://services.reachmail.net/Rest/Contacts/v1'
	CreateList = {
		'uri': base + '/lists/%s',
		'method': 'POST'
	}
	GetList = {
		'uri': base + '/lists/%s/%s',
		'method': 'GET'
	}
	ExportRecipients = {
		'uri': base + '/lists/export/%s/%s',
		'method': 'POST'
	}
	GetExportStatus = {
		'uri': base + '/lists/export/status/%s/%s',
		'method': 'GET'
	}
	EnumerateFields = {
		'uri': base + '/lists/fields/%s',
		'method': 'GET'
	}
	EnumerateGroups = {
		'uri': base + '/lists/groups/%s',
		'method': 'GET'
	}
	CreateGroup = {
		'uri': base + '/lists/groups/%s',
		'method': 'POST'
	}
	GetGroup = {
		'uir': base + '/lists/groups/%s/%s',
		'method': 'GET'
	}
	EnumerateLists = {
		'uri': base + '/lists/query/%s',
		'method': 'POST'
	}

class MailingService(object):
	base = 'https://services.reachmail.net/Rest/Mailings/v1'
	EnumerateMailings = {
		'uri': base + '/query/%s',
		'method': 'POST'
	}

class ReportService(object):
	base = 'https://services.reachmail.net/Rest/Reports/v1'
	EnumerateMailingReports = {
		'uri': base + '/mailings/query/%s',
		'method': 'POST'
	}

def call(service_uri, method, api_user, password, request_body=None):
	pw_manager = urllib2.HTTPPasswordMgrWithDefaultRealm()
	pw_manager.add_password(None, service_uri, api_user, password)
	auth_handler = urllib2.HTTPBasicAuthHandler(pw_manager)
	opener = urllib2.build_opener(auth_handler)
	urllib2.install_opener(opener)
	try:
		if method == 'GET':
			response = urllib2.urlopen(service_uri).read()
		elif method == 'POST':
			request = urllib2.Request(service_uri, request_body)
			request.add_header('Content-Type', 'text/xml')
			response = urllib2.urlopen(request).read()
	except urllib2.HTTPError, e:
		response = "HTTP ERROR: %s %s" % (service_uri, e)
	except urllib2.URLError, e:
		response = "URL ERROR: %s %s" % (service_uri, e)
	except Exception, e:
		response = "I/O ERROR: %s" % e
	return response	 
	
