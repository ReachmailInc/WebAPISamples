r""" mailingsummary.py - Application core """
import sys
sys.path.insert(0, './')
sys.path.insert(0, '../')

# Standard library modules
from datetime import datetime, timedelta
try:
	import sqlite3 as sqlite
except:
	import sqlite

# Application modules
from frame.reachmailapi import report, credentials, administration, utilities
from frame.conf import appconfig
from frame.util import excel

def get_account_id(user):
	response = administration.get_current_user(user)
	try:
		acct = utilities.ParseResponse(response).accountid.firstChild.nodeValue
	except:
		print response
		sys.exit(1)
	return acct

def enumerate_mailings(user, account_id, mailing_id=None, max_results=50,
	delivery_start=None, delivery_end=None, start_index=None):
	service_xml = '<MailingReportFilter>'
	if mailing_id:
		service_xml += '<MailingId>%s</MailingId>' % mailing_id
	service_xml += '<MaxResults>%d</MaxResults>' % max_results
	if delivery_start:
		service_xml += '<ScheduledDeliveryOnOrAfter>%s</ScheduledDeliveryOnOrAfter>' % delivery_start
	if delivery_end:
		service_xml += '<ScheduledDeliveryOnOrBefore>%s</ScheduledDeliveryOnOrBefore>' % delivery_end
	if start_index:
		service_xml += '<StartIndex>%d</StartIndex>' % start_index
	service_xml += '</MailingReportFilter>'
	response = report.enumerate_mailing_reports(user, account_id, service_xml)
	return response

def get_links(user, account_id, mailing_id):
	response = report.get_tracked_link_report_by_mailing_id(user, account_id,
		mailing_id)
	return response

def setup_sql():
	con = sqlite.connect('../lib/mailing_reports.db')
	cur = con.cursor()
	create_table_sql = """
		create table if not exists mailing_reports (mailing_id text,
			sent integer)
	"""
	cur.execute(create_table_sql)
	con.commit()
	con.close()
	
def report_sent(mailing_id):
	con = sqlite.connect('../lib/mailing_reports.db')
	cur = con.cursor()
	sql = """
		select count(*) from mailing_reports where sent = 1 and 
		mailing_id = '%s' 
	""" % mailing_id
	sent = cur.execute(sql).fetchone()
	if sent:
		return True 
	return False

def send_report(api_login, report, config):
	rpt_file = make_xls_report(api_login, report, config)
	# Do the sending stuff
	#con = sqlite.connect('../lib/mailing_reports.db')
	#cur = con.cursor()
	#sql = """
	#	insert into mailing_reports (mailing_id, sent)
	#	values (?, 1)
	#"""
	#cur.execute(sql, report.mailingid)
	#con.commit()
	#fh = open(report.mailingid + '.xls', 'w')
	fh = open(report.message.name.replace('/', '').replace('\\', '') + '.xls', 
		'w')
	fh.write(rpt_file)
	return True 

def make_xls_report(api_login, report, config):
	tracking = utilities.GetTrackedLinkReportByMailingId(
		get_links(api_login, report.accountid, report.mailingid))
	template = open('../lib/rm_excel_template.html', 'r').read()
	source = excel.ExcelReport(template, report, tracking,
		config.parser.get('ClientImage', 'url')).output
	return source
		
def run():
	config = appconfig.Config(['../config/app_config.ini'])

	### For testing only, remove this section prior to delivery ###
	### Make sure that the install script sets these values ###
	config.parser.set('RMAccountDetails', 'account_id', 'sendoffe')
	config.parser.set('RMAccountDetails', 'username', 'admin2')
	config.parser.set('RMAccountDetails', 'password', 'Q8$Van?e')
	config.parser.set('ClientImage', 'url',
		'https://go.reachmail.net/client_report_images/6046e923-8d8b-4f7b-8bf9-548d3db663da.jpg')
	config.parser.set('Notifications', 'email', 'dnielsen@reachmail.com')
	### End testing config ###

	api_login = credentials.Login(
		config.parser.get('RMAccountDetails', 'account_id'),
		config.parser.get('RMAccountDetails', 'username'),
		config.parser.get('RMAccountDetails', 'password'))

	setup_sql()
	account_id = get_account_id(api_login)
	two_days = timedelta(days=2)
	four_days = timedelta(days=4)
	today = datetime.now()
	mailing_reports = utilities.EnumerateMailingReports(
		enumerate_mailings(api_login, account_id,
		delivery_start=(today - four_days).isoformat()[:-7],
		delivery_end=(today - two_days).isoformat()[:-7]))
	print 'Found %d mailing reports' % len(mailing_reports.reports) 
	for report in mailing_reports.reports:
		print report.delivereddate, report.mailingid
		send_report(api_login, report, config)

if __name__ == '__main__':
	run()
	sys.exit(0)
