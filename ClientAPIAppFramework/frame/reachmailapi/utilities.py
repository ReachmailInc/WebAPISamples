r""" utilities.py - Wrappers for some of the commonly required API functions """
import sys
sys.path.insert(0, './')
sys.path.insert(0, '../')

from xml.dom import minidom

# Import all the api classes
from frame.reachmailapi import administration, contact, mailing, report, content

class ApiError(object):
	""" Simple reporting class for any API errors, requires error XML
	ApiError.message - message from API error
	ApiError.status - status code from API error
	"""
	def __init__(self, source_xml):
		try:
			xml_dom = minidom.parseString(source_xml)
			self.error = xml_dom.getElementsByTagName('Error')[0]
		except Exception, e:
			self.error = None
		self.message = self.error.getElementsByTagName('Message')[0].firstChild.nodeValue
		self.status = self.error.getElementsByTagName('Status')[0].firstChild.nodeValue

def is_error(source_xml):
	""" Quick check for an API error """
	xml_dom = minidom.parseString(source_xml)
	root = xml_dom.documentElement.tagName
	if root == 'Error':
		return True
	return False

class ParseResponse(object):
	""" Generic response parser """
	def __init__(self, source_xml):
		self.source = str(source_xml).encode('string_escape')
		if is_error(self.source):
			self.error = ApiError(self.source)
			return
		self.doc = minidom.parseString(self.source)
		self.root_name = self.doc.documentElement.tagName
		self.root = self.doc.getElementsByTagName(self.root_name)[0]
		if self.root.hasChildNodes():
			for node in self.root.childNodes:
				exec 'self.%s=node' % node.tagName.lower()

class CustomParser(ParseResponse):
	""" A more specific response parser for sub-classing """
	def __init__(self, source_xml, root_name):
		self.source = str(source_xml).encode('string_escape')
		if is_error(self.source):
			self.error = ApiError(self.source)
			return
		self.doc = minidom.parseString(self.source)
		self.root = self.doc.getElementsByTagName(root_name)[0]
		if self.root.hasChildNodes():
			for node in self.root.childNodes:
				try:
					exec 'self.%s="""%s"""' % (node.tagName.lower(), 
						node.firstChild.nodeValue)
				except Exception, e:
					pass

class MailingListReportParser(CustomParser):
	"""Mailing List Report specifics"""
	def __init__(self, source_xml, root_name):
		CustomParser.__init__(self, source_xml, root_name)
		self.forwardsummary = CustomParser(
			self.doc.getElementsByTagName('ForwardSummary')[0].toxml(),
			'ForwardSummary')
		self.recipientcount = CustomParser(
			self.doc.getElementsByTagName('RecipientCount')[0].toxml(),
			'RecipientCount')

class MailingReportParser(CustomParser):
	""" Mailing report specific parser """
	def __init__(self, source_xml, root_name):
		CustomParser.__init__(self, source_xml, root_name)
		self.forwardsummary = CustomParser(
			self.doc.getElementsByTagName('ForwardSummary')[0].toxml(),
			'ForwardSummary')
		self.lists = [MailingListReportParser(x.toxml(), 
			'MailingListReport') for x in self.doc.getElementsByTagName(
				'MailingListReport')]
		self.message = CustomParser(
			self.doc.getElementsByTagName('Message')[0].toxml(),
			'Message')
		self.recipientsummary = CustomParser(
			self.doc.getElementsByTagName('RecipientSummary')[0].toxml(),
			'RecipientSummary')

class EnumerateMailingReports(object):
	""" Convenience class for enumerate_mailing_reports results """
	def __init__(self, source_xml):
		self.source = str(source_xml).encode('string_escape')
		self.doc = minidom.parseString(self.source)
		self.reports = [MailingReportParser(x.toxml(), 
			'MailingReport') for x in self.doc.getElementsByTagName(
				'MailingReport')]

class GetTrackedLinkReportByMailingId(CustomParser):
	""" Convenience class for get_tracked_link_report_by_mailing_id results """
	def __init__(self, source_xml):
		self.doc = minidom.parseString(source_xml)
		self.links = [CustomParser(x.toxml(),
			'TrackedLink') for x in self.doc.getElementsByTagName(
				'TrackedLink')]
