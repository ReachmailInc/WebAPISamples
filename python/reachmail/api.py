# -*- coding: utf-8 -*-
#
# Copyright (C) 2013 ReachMail, Inc. / Dan Nielsen 
#
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with this program.  If not, see <http://www.gnu.org/licenses/>.
from . import base
from . import helpers

class ReachMail(object):


    def __init__(self, token, request_func=helpers.request):
        self.token = token
        self.request_func = request_func

    @property
    def adminsitration(self):
        return base.Administration(request_func=self.request)

    @property
    def campaigns(self):
        return base.Campaigns(request_func=self.request)

    @property
    def data(self):
        return base.Data(request_func=self.request)
        
    @property
    def lists(self):
        return base.Lists(request_func=self.request)

    @property
    def mailings(self):
        return base.Mailings(request_func=self.request)

    @property
    def reports(self):
        return base.Reports(request_func=self.request)

    def request(self, method, url, data=None):
        return self.request_func(method, url, self.token, data)
