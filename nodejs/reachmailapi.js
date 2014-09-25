/*
Copyright (C) <2014>  <Dan Nielsen / ReachMail, Inc.>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

----

This module is a simple wrapper to the ReachMail API 
(services.reachmail.net/documentation). It contains four basic functions,
`get`, `post`, `put`, and `delete` for use in accessing the ReachMail API.
Some convenience functions have been added to aid access to certain services,
more will be added at a later date.

USAGE:

var reachmail = require('/path/to/reachmailapi');

var api = new reachmail({token: 'your-token-here'});

api.get('/administration/users/current', function (http_code, response) {
    console.log(http_code);
    console.log(response);
});

----

As indicated by the above example, each function requires a callback capable
of handling the http response code and the response itself.

Function parameters:

get (service_url, callback())
post (service_url, post_body, callback())
put (service_url, post_body, callback())
delete (service_url, callback())

Questions regarding use of this software should be referred to 
support@reachmail.com

*/
var VERSION = '0.7.1',
    https   = require('https'),
    util    = require('util');

function merge (def, opt) {
    defaults = def || {};

    if (opt && typeof opt === 'object') {
        var keys = Object.keys(opt);
        for (var j = 0, len = keys.length; j < len; j++) {
            var k = keys[j];
            if (opt[k] !== undefined) def[k] = opt[k];
        }
    }
    return def;
}

var request_options = function (host, path, method, headers, body) {
    this.hostname = host;
    this.port = 443;
    this.path = path;
    this.method = method;
    this.headers = headers;
    this.body = body;

    if (body !== null) {
        this.headers['Content-Length'] = body.length;
    }
}

function call_service (options, callback) {

    var request = https.request(options, function (response) {
        var status_code = response.statusCode;
        var body = '';
        response.setEncoding('utf8');
        response.on('data', function (part) {
            body += part;
        });
        response.on('end', function () {
            try {
                var json = JSON.parse(body);
                callback (status_code, json);
            } catch (err) {
                callback (status_code, err);
            }
        });
    });

    request.on('error', function (err) {
        throw err;
    });

    if (options.method == 'PUT' || options.method == 'POST') {
        request.write(options.body + '\n');
    }

    request.end();
}

function ReachMail (options) {

    if (!(this instanceof ReachMail)) return new ReachMail(options);

    var defaults = {
        token: null,

        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'User-Agent': 'reachmail-node-' + VERSION,
            'Authorization': 'token '
        },

        base_url: 'services.reachmail.net'
    };

    this.options = merge(defaults, options);
    this.options.headers['Authorization'] += this.options.token;
}
module.exports = ReachMail;

/*
 * `get(url, callback)` root prototype for HTTP GET requests
*/
ReachMail.prototype.get = function (url, callback) {
    if (typeof callback !== 'function') {
        throw "Invalid Callback, function required";
        return this;
    }

    var opts = new request_options (this.options.base_url, url, "GET",
            this.options.headers, null);

    call_service (opts, callback);
}

/*
 * `post(url, content, callback)` root prototype for HTTP POST requests
 */
ReachMail.prototype.post = function (url, content, callback) {
    if (typeof content === 'function') {
        callback = content;
        content = null;
    } 

    if (typeof callback !== 'function') {
        throw "Invalid Callback, function required";
        return this;
    }

    var opts = new request_options (this.options.base_url, url, 'POST',
            this.options.headers, content);

    call_service (opts, callback);
}

/*
 * `put(url, content, callback)` root prototype for HTTP PUT requests
 */
ReachMail.prototype.put = function (url, content, callback) {
    if (typeof content === 'function') {
        callback = content;
        content = null;
    } 

    if (typeof callback !== 'function') {
        throw "Invalid Callback, function required";
        return this;
    }

    var opts = new request_options (this.options.base_url, url, 'PUT',
            this.options.headers, content);

    call_service (opts, callback);
}

/*
 * `delete(url, callback)` root prototype for HTTP DELETE requests
 */
ReachMail.prototype.delete = function (url, callback) {
    if (typeof callback !== 'function') {
        throw "Invalid Callback, function required";
        return this;
    }

    var opts = new request_options (this.options.base_url, url, 'DELETE',
            this.options.headers, null);

    call_service (opts, callback);
}

/*
 * Administration services
 */
ReachMail.prototype.AdministrationUsersCurrent = function (callback) {
    this.get('/administration/users/current', callback);
}

/*
** EasySmtpDelivery services
*/
ReachMail.prototype.easySmtpDelivery = function (accountId, body, callback) {
    this.post ("/easysmtp/" + accountId, body, callback);
}

/*
 * Reporting services
 */
ReachMail.prototype.reportsEasySmtp = function (accountId, startDate, endDate,
        callback) {
    var u = util.format("/reports/easysmtp/%s?startdate=%s&enddate=%s", 
        accountId, startDate, endDate);
    this.get (u, callback);
}

ReachMail.prototype.reportsEasySmtpBounces = function (accountId, startDate,
        endDate, callback) {
    var u = util.format("/reports/easysmtp/bounces/%s?startdate=%s&enddate=%s", 
        accountId, startDate, endDate);
    this.get (u, callback);
}

ReachMail.prototype.reportsEasySmtpOptouts = function (accountId, startDate,
        endDate, callback) {
    var u = util.format("/reports/easysmtp/optouts/%s?startdate=%s&enddate=%s", 
        accountId, startDate, endDate);
    this.get (u, callback);
}
