r""" Tests for the Notification class """
import sys
sys.path.insert(0, './')
sys.path.insert(0, '../')

import unittest
from frame.notify import notify

class ConfigTest(unittest.TestCase):
	def setUp(self):
		unittest.TestCase.setUp(self)
		self.notification = notify.Notification(['./app_config.ini'],
			message="This is a test")

	def tearDown(self):
		unittest.TestCase.tearDown(self)

	def testTrue(self):
		self.assertEqual(self.notification.to, 'dnielsen@reachmail.com')
		self.assertEqual(self.notification.host, 'sub.domain.tld')
		self.assertEqual(int(self.notification.port), 2525)
		self.assertEqual(self.notification.auth_required, True)
		self.assertEqual(self.notification.tls_required, True)
		self.assertEqual(self.notification.auth_user, 'smtpuser')
		self.assertEqual(self.notification.auth_pass, 'F4k3Pa55')

if __name__ == '__main__':
	unittest.main()
