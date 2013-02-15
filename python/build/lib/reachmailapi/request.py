# Functions to call Reachmail API services
import urllib2

class Administration(object):
	base = 'https://services.reachmail.net/Rest/Administration/v1'
	EnumerateAddresses = {
                'uri': base + '/addresses/%s',
                'method': 'GET'
        }
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
	ModifyList = {
		'uri': base + '/lists/%s/%s',
		'method': 'PUT'
	}
	DeleteList = {
		'uri': base + '/lists/%s/%s',
		'method': 'DELETE'
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
		'uri': base + '/lists/groups/%s/%s',
		'method': 'GET'
	}
	ModifyGroup = {
		'uri': base + '/lists/groups/%s/%s',
		'method': 'PUT'
	}
	DeleteGroup = {
		'uri': base + '/lists/groups/%s/%s',
		'method': 'PUT'
	}
	ImportRecipients = {
		'uri': base + '/lists/import/%s/%s',
		'method': 'POST'
	}
	GetImportStatus = {
		'uri': base + '/lists/import/%s/%s',
		'method': 'GET'
	}
	OptInRecipientFromList = {
		'uri': base + '/lists/optin/%s/%s?redirecturl=%s',
		'method': 'POST'
	}
	OptOutRecipientFromList = {
		'uri': base + '/lists/optout/%s/%s?redirecturl=%s',
		'method': 'POST'
	}
	EnumerateLists = {
		'uri': base + '/lists/query/%s',
		'method': 'POST'
	}
	GetRecipient = {
		'uri': base + '/lists/query/%s/%s/%s',
		'method': 'GET'
	}
	ModifyRecipient = {
		'uri': base + '/lists/recipients/%s/%s/%s',
		'method': 'PUT'
	}
	DeleteRecipient = {
		'uri': base + '/lists/recipients/%s/%s/%s',
		'method': 'DELETE'
	}
	CreateRecipient = {
		'uri': base + '/lists/recipients/%s/%s?redirecturl=%s',
		'method': 'POST'
	}
	GetIndividualRecipientByQuery = {
		'uri': base + '/lists/recipients/find/%s/%s',
		'method': 'POST'
	}
	DeleteIndividualRecipientByQuery = {
		'uri': base + '/lists/recipients/find/delete/%s/%s',
		'method': 'POST'
	}
	ModifyIndividualRecipientByQuery = {
		'uri': base \
			+ '/lists/recipients/find/modify/%s/%s?redirecturl=%s',
		'method': 'POST'
	}
	EnumerateRecipients = {
		'uri': base + '/lists/recipients/query/%s/%s',
		'method': 'POST'
	}
	DeleteBatchRecipientsByQuery = {
		'uri': base + '/lists/recipients/query/delete/%s/%s',
		'method': 'POST'
	}
	ModifyBatchRecipientsByQuery = {
		'uri': base + '/lists/recipients/query/modify/%s/%s',
		'method': 'POST'
	}
	ModifyOrCreateIndividualRecipientByQuery = {
		'uri': base + \
			'/lists/recipients/subscribe/%s/%s?redirecturl=%s',
		'method': 'POST'
	}
	OptInRecipientFromAccount = {
		'uri': base + '/optin/%s?redirecturl=%s',
		'method': 'POST'
	}
	OptOutRecipientFromAccount = {
		'uri': base + '/optout/%s?redirecturl=%s',
		'method': 'POST'
	}

class ContentLibraryService(object):
	base = 'https://services.reachmail.net/Rest/Content/Library'
	AddFile = {
		'uri': base + '/files/%s/%s',
		'method': 'POST'
	}
	GetFile = {
		'uri': base + '/files/%s/%s',
		'method': 'GET'
	}
	ModifyFile = {
		'uri': base + '/files/%s/%s',
		'method': 'PUT'
	}
	DeleteFile = {
		'uri': base + '/files/%s/%s',
		'method': 'DELETE'
	}
	EnumerateFiles = {
		'uri': base + '/files/query/%s',
		'method': 'POST'
	}
	CreateFolder = {
		'uri': base + '/folders/%s',
		'method': 'POST'
	}
	GetFolder = {
		'uri': base + '/folders/%s/%s',
		'method': 'GET'
	}
	ModifyFolder = {
		'uri': base + '/folders/%s/%s',
		'method': 'PUT'
	}
	DeleteFolder = {
		'uri': base + '/folders/%s/%s',
		'method': 'DELETE'
	}
	EnumerateFolders = {
		'uri': base + '/folders/query/%s',
		'method': 'POST'
	}

class MailingService(object):
	base = 'https://services.reachmail.net/Rest/Mailings/v1'
	CreateMailing = {
		'uri': base + '/%s',
		'method': 'POST'
	}
	GetMailing = {
		'uri': base + '/%s/%s',
		'method': 'POST'
	}
	ModifyMailing = {
		'uri': base + '/%s/%s',
		'method': 'POST'
	}
	DeleteMailing = {
		'uri': base + '/%s/%s',
		'method': 'POST'
	}
	EnumerateGroups = {
		'uri': base + '/groups/%s',
		'method': 'POST'
	}
	CreateGroup = {
		'uri': base + '/groups/%s',
		'method': 'POST'
	}
	GetGroup = {
		'uri': base + '/groups/%s/%s',
		'method': 'GET'
	}
	ModifyGroup = {
		'uri': base + '/groups/%s/%s',
		'method': 'PUT'
	}
	DeleteGroup = {
		'uri': base + '/group/%s/%s',
		'method': 'DELETE'
	}
	EnumerateMailings = {
		'uri': base + '/query/%s',
		'method': 'POST'
	}
	CreateMailingtemplate = {
		'uri': base + '/templates/%s',
		'method': 'POST'
	}
	GetMailingTemplate = {
		'uri': base + '/templates/%s/%s',
		'method': 'GET'
	}
	ModifyMailingTemplate = {
		'uri': base + '/templates/%s/%s',
		'method': 'PUT'
	}
	DeleteMailingTemplate = {
		'uri': base + '/templates/%s/%s',
		'method': 'DELETE'
	}
	EnumerateTemplateGroups = {
		'uri': base + '/templates/groups/%s',
		'method': 'GET'
	}
	CreateTemplateGroup = {
		'uri': base + '/templates/groups/%s',
		'method': 'POST'
	}
	GetTemplateGroup = {
		'uri': base + '/templates/groups/%s/%s',
		'method': 'GET'
	}
	ModifyTemplateGroup = {
		'uri': base + '/templates/groups/%s/%s',
		'method': 'PUT'
	}
	DeleteTemplateGroup = {
		'uri': base + '/templates/groups/%s/%s',
		'method': 'DELETE'
	}
	EnumerateMailingTemplates = {
		'uri': base + '/templates/query/%s',
		'method': 'POST'
	}

class DataService(object):
	base = 'https://services.reachmail.net/Rest/Data'
	Upload = {
		'uri': base + '/',
		'method': 'POST'
	}
	Download = {
		'uri': base + '/%s',
		'method': 'GET'
	}
	DownloadFile = {
		'uri': base + '/%s/%s',
		'method': 'GET'
	}
	Exists = {
		'uri': base + '/exists/%s',
		'method': 'GET'
	}

class ReportService(object):
	base = 'https://services.reachmail.net/Rest/Reports/v1'
	GetBounceDetailReport = {
		'uri': base + '/details/mailings/bounces/%s/%s',
		'method': 'POST'
	}
	GetOptOutDetailReport = {
		'uri': base + '/details/mailings/optouts/%s/%s',
		'method': 'POST'
	}
	GetReadDetailReport = {
		'uri': base + '/details/mailings/reads/%s/%s',
		'method': 'POST'
	}
	GetTrackedLinkDetailReport = {
		'uri': base + '/details/mailings/trackedLink/%s/%s',
		'method': 'POST'
	}
	GetMailingReport = {
		'uri': base + '/mailings/%s/%s',
		'method': 'GET'
	}
	EnumerateMailingReports = {
		'uri': base + '/mailings/query/%s',
		'method': 'POST'
	}
	GetTrackedLinkReportByMailingId = {
		'uri': base + '/mailings/trackedLinks/%s/%s',
		'method': 'GET'
	}
	GetTrackedLinkReportByMailingListId = {
		'uri': base + '/mailings/trackedLinks/%s/%s/%s',
		'method': 'GET'
	}

class RuntimeService(object):
	GetInfo = {
		'uri': 'https://services.reachmail.net/Rest/Runtime/',
		'method': 'GET'
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
		elif method == 'PUT':
			request = urllib2.Request(service_uri, request_body)
			request.add_header('Content-Type', 'text/xml')
			request.get_method = lambda: 'PUT'
			response = urllib2.urlopen(request).read()
		elif method == 'DELETE':
			request = urllib2.Request(service_uri, request_body)
			request.add_header('Content-Type', 'text/xml')
			request.get_method = lambda: 'DELETE'
			response = urllib2.urlopen(request).read()
	except urllib2.HTTPError, e:
		response = "HTTP ERROR: %s %s" % (service_uri, e)
	except urllib2.URLError, e:
		response = "URL ERROR: %s %s" % (service_uri, e)
	except Exception, e:
		response = "I/O ERROR: %s" % e
	return response	 
	
