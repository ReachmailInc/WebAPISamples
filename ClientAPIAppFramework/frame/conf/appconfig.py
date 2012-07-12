r""" appconfig.py - Generic config parser for client applications """
import sys
sys.path.insert(0, './')
sys.path.insert(0, '../')
try:
	import configparser
except:
	import ConfigParser as configparser

from frame.errors import apperror
 
class ConfigError(apperror.ApplicationError):
	# Config error subclass
	pass

class Config(object):
	# The base class for config settings.
	# Requires a list of config files and optionally, a list of required
	# config settings to check.
	# Required input should be ('Section Name', ['list', 'of', 'options']) 
	# The 'config' method will host the parsed results, options can be 
	# retrieved using the 'get' method. For example:
	# >>> cfg = appconfig.Config(config_files=['config.ini'])
	# >>> user = cfg.config.get('RMAccountDetails', 'username')
	# >>> user
	# 'admin'
	def __init__(self, config_files=[], required=[]):
		if not required:
			required = [('RMAccountDetails', ['account_id', 'username',
				'password'])]
		try:
			self.candidates = config_files
			self.parser = configparser.SafeConfigParser()
			self.config = self.parser.read(self.candidates)
			self.missing = set(self.candidates) - set(self.config)
			# Simple check to ensure that at least one config file was
			# found and parsed
			if len(self.missing) == len(self.candidates):
				raise ConfigError(301, "No config file found")
			# Execute the checks of the required config elements
			# At minimum, the 'RMAccountDetails' section will be verified
			for section in required:
				if self.parser.has_section(section[0]):
					for option in section[1]:
						if not self.parser.has_option(section[0], option):
							raise ConfigError(303, 
								"missing config option %s, %s" % (section,
									option))
				else:
					raise ConfigError(302, "missing required config section" % \
						section)  
		except ConfigError, e:
			print "error %d: %s" % (e.args[0], e.args[1])
		except Exception, e:
			print e

class SimpleConfig(Config):
	# A convenience class that contains just the account login details
	def account_id(self):
		return self.parser.get('RMAccountDetails', 'account_id')
	def username(self):
		return self.parser.get('RMAccountDetails', 'username')
	def password(self):
		return self.parser.get('RMAccountDetails', 'password')
