r"""Test for the utilities classes and functions"""
import sys
sys.path.insert(0, './')
sys.path.insert(0, '../')

import unittest
from xml.dom import minidom
from frame.reachmailapi import utilities

class UtilTest(unittest.TestCase):
	def setUp(self):
		unittest.TestCase.setUp(self)
		self.doc = minidom.parse('sample.xml')
		self.doc_root = self.doc.documentElement.tagName
		#self.sdoc = utilities.MailingReportParser(
		#	open('sample.xml', 'r').read(),
		#	'MailingReport')
		self.EMR = utilities.EnumerateMailingReports(
			open('sample.xml', 'r').read().strip())
		self.GTLRBMI = utilities.GetTrackedLinkReportByMailingId(
			open('lt_sample.xml', 'r').read())

	def tearDown(self):
		unittest.TestCase.tearDown(self)

	def testTrue(self):
		self.assertEqual(self.doc_root, 'MailingReports')
		self.assertEqual(self.EMR.reports[0].createdby, 'admin')
		self.assertEqual(self.EMR.reports[0].message.subject,
			'Please interact with this email')
		self.assertEqual(self.EMR.reports[0].lists[0].recipientcount.sent,
			'130')
		self.assertEqual(self.GTLRBMI.links[0].totalclicks, '2')
		self.assertEqual(self.GTLRBMI.links[0].linkmailingformat, 'Html')
		self.assertEqual(self.GTLRBMI.links[1].uniqueclicks, '1')

if __name__ == '__main__':
	unittest.main()
