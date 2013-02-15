# Reachmail API credentials class

class Login(object):
	def __init__(self, account_key, username, password):
		self.acct_key = account_key.upper()
		self.username = username
		self.password = password
		self.api_user = self.acct_key + '\\' + username
