r""" Basic client notifications function """
import smtplib
import sys
sys.path.insert(0, './')
sys.path.insert(0, '../')

from frame.conf import appconfig
from frame.errors import apperror

class NotificationError(apperror.ApplicationError):
	pass

class Notification(object):
	def __init__(self, config_files, to=None, sender=None, message=None):
		self.auth_required = False
		self.tls_required = False
		self.config = appconfig.Config(config_files)
		try:
			self.to = self.config.parser.get('Notifications', 'email')
		except:
			if not to:
				raise NotificationError(401, 'notification email not specified')
			else:
				self.to = to
		if not sender:
			self.sender = 'support@reachmail.com'
		if not message:
			raise NotificationError(402, 'no message specifed')
		if self.config.parser.has_option('Notifications', 'host'):
			self.host = self.config.parser.get('Notifications', 'host')
		else:
			self.host = 'localhost'
		if self.config.parser.has_option('Notifications', 'port'):
			self.port = self.config.parser.get('Notifications', 'port')
		else:
			self.port = 25
		if self.config.parser.has_option('Notifications', 'method'):
			self.auth_method = self.config.parser.get('Notifications', 'method')
		else:
			self.auth_method = 'PLAIN'
		if self.config.parser.has_option('Notifications', 'auth_required'):
			self.auth_required = True
			self.auth_user = self.config.parser.get('Notifications', 'username')
			self.auth_pass = self.config.parser.get('Notifications', 'password')
		if self.config.parser.has_option('Notifications', 'tls_required'):
			self.tls_required = True	

	def sendmail(self):
		try:
			session = smtplib.SMTP(self.host, self.port)
			session.ehlo()
			if self.tls_required:
				session.starttls()
				session.ehlo()
			if self.auth_required():
				session.login(self.auth_user, self.auth_pass)
			session.sendmail(self.sender, self.to, self.message)
			session.quit()
		except Exception, e:
			raise NotificationError(403, str(e))
		return True
