r"""Test for the appconfig class"""
import sys
sys.path.insert(0, './')
sys.path.insert(0, '../')

import unittest
from frame.conf import appconfig

class ConfigTest(unittest.TestCase):
	def setUp(self):
		unittest.TestCase.setUp(self)
		self.config = appconfig.Config(['./app_config.ini'])
		self.simple = appconfig.SimpleConfig(['./app_config.ini'])

	def tearDown(self):
		unittest.TestCase.tearDown(self)

	def testTrue(self):
		self.assertEqual(self.config.parser.get('RMAccountDetails', 
			'account_id'), 'rmtr')
		self.assertEqual(self.config.parser.get('Notifications', 'email'),
			'dnielsen@reachmail.com')
		self.assertEqual(self.simple.account_id(), 'rmtr')


if __name__ == '__main__':
	unittest.main()
