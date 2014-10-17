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
try:
    import simplejson as json
except ImportError:
    import json

import httplib2
import collections

VER_S = "1.3"

Response = collections.namedtuple("Response", "status content")

def request(method, url, token, data=None):

    if data is not None:
        data = json.dumps(data)

    headers = {"Accept":"application/json", "Content-Type":"application/json",
            "User-Agent":"ReachMail Python API wrapper " + VER_S,
            "Authorization": "token " + token}

    response, content = httplib2.Http(timeout=45).request(url,
            method=method.upper(), body=data, headers=headers)

    return Response(response.status, content)
