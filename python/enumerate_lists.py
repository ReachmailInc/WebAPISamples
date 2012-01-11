#!/usr/bin/env python
"""enumerate_lists.py [-k account key] [-u username]\n"""
# enumerate_lists.py - Demonstration of Reachmail API service 
# ContactService\EnumerateLists 
import sys, urllib2, getpass, getopt
from xml.dom import minidom

class Options:
	pass

class Login:
	def __init__(self, key, username, password):
		self.key = key.upper()
		self.username = username
		self.password = password
		self.api_user = key + '\\' + username

def service_call(service_uri, method, api_user, password, request_body=None):
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
		print "HTTP ERROR: %s\n%s\n" % (service_uri, e)
		sys.exit(1)
	except urllib2.URLError, e:
		print "URL ERROR: %s\n%s\n" % (service_uri, e)
		sys.exit(1)
	except urllib2.IOError, e:
		print "I/O ERROR: %s\n" % e
		sys.exit(1)
	return response

def get_current_user(credentials):
	service_uri = 'https://services.reachmail.net/Rest/Administration/v1/users/current'
	response = service_call(service_uri, 'GET', credentials.api_user, 
		credentials.password)
	try:
		xmldom = minidom.parseString(response)
		_id = xmldom.getElementsByTagName('AccountId')[0].firstChild.nodeValue
	except Exception, e:
		_id = None
		return _id, e 
	return _id, None 

def enumerate_lists(credentials, _id):
	service_uri = 'https://services.reachmail.net/Rest/Contacts/v1/lists/query/%s' % str(_id)
	response = service_call(service_uri, 'POST', credentials.api_user,
		credentials.password, request_body='<ListFilter></ListFilter>')
	try:
		xmldom = minidom.parseString(response)
		_lists = xmldom.getElementsByTagName('List')
	except Exception, e:
		_lists = None
		return _lists, e
	return _lists, None

def parse_response(response):
	try:
		xmldom = minidom.parseString(response)
		_id = xmldom.getElementsByTagName('AccountId')[0].firstChild.nodeValue
	except Exception, e:
		_id = response
	return _id

def parseargs():
	try:
		opts, args = getopt.getopt(sys.argv[1:], 'hk:u:',
			['acct_key=', 'username='])
	except getopt.error, e:
		usage(1, e)
	options = Options()
	for opt, arg in opts:
		if opt in ('-h', '--help'):
			usage(0)
		elif opt in ('-k', '--acct_key'):
			options.acct_key = arg
		elif opt in ('-u', '--username'):
			options.username = arg
	return options

def usage(code, msg=''):
	if msg:
		print msg
	else:
		print "%s" % __doc__
	sys.exit(code)

def run():
	options = parseargs()
	if not options.acct_key: 
		options.acct_key = raw_input('Account key: ')
	if not options.username:
		options.username = raw_input('Username:' )
	password = getpass.getpass('Password: ')
	credentials = Login(options.acct_key, options.username, password)
	_id, err = get_current_user(credentials)
	if not _id:
		print err
		sys.exit(2)
	_lists, err = enumerate_lists(credentials, _id)
	if not _lists:
		print err
		sys.exit(2)
	print "Account %s has %d lists" % (credentials.key, len(_lists))

if __name__ == '__main__':
	run()
	sys.exit(0)
