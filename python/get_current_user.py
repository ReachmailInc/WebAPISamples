#!/usr/bin/env python
"""get_current_user.py [-k account key] [-u username]\n"""
# get_current_user.py - Demonstration of Reachmail API service
# AdministrationService\GetCurrentUser
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

def get_current_user(key, username, password):
	service_uri = 'https://services.reachmail.net/Rest/Administration/v1/users/current'
	credentials = Login(key, username, password)
	response = service_call(service_uri, 'GET', credentials.api_user, 
		credentials.password)
	return response

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
	response = get_current_user(options.acct_key, options.username, 
		password)
	print parse_response(response)

if __name__ == '__main__':
	run()
	sys.exit(0)
