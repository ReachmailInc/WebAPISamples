r""" Miscellaneous utilities """
import os
import sys

def load_ok(limit):
	""" Checks the current load average against a limit. Returns true if load
		average is less than limit.
	"""
	if os.getloadavg()[0] < limit:
		return True
	return False
