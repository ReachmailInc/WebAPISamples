r""" apperror.py - Base class for application errors """
class ApplicationError(Exception):
	def __init__(self, *args):
		self.args = [a for a in args]
