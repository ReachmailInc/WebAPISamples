# Example script for reachmailapi
"""api_example.py [-k account key] [-u username]"""
from reachmailapi import administration, contact, credentials, mailing, report
from xml.dom import minidom
import getpass, getopt

class Options:
	pass

def get_account_id(user):
	response = administration.get_current_user(user)
	account_id = parse_response(response, 'AccountId')[0].firstChild.nodeValue
	return account_id

def parse_response(response, node):
	try:
		xmldom = minidom.parseString(response)
		node_list = xmldom.getElementsByTagName(node)
	except Exception, e:
		node_list = None
	return None

def parse_args():
	try:
		opts, args = getopt.getopt(sys.argv[1:], 'hk:u:',
			['acct_key=', 'username='])
	except getopt.error, e:
		usage(1, e)
	options = Options()
	for opt, arg in opts:
		if opt in ('-h', '--help'):
			usage(0)
		elif opt in ('-k', '--acct-key'):
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
	options = parse_args()
	if not options.acct_key:
		options.acct_key = raw_input('Account key: ')
	if not options.username:
		options.username = raw_input('Username: ')
	password = getpass.getpass('Password: ')
	user = credentials.Login(options.acct_key, options.username, password)
	print "Account Id: %s" % get_account_id(user)
	
if __name__ == '__main__':
	run()
	sys.exit(0)
