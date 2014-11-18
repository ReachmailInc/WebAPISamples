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
    this.port     = 443;
    this.path     = path;
    this.method   = method;
    this.headers  = headers;
    this.body     = body;

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
                callback(status_code, json);
            } catch (err) {
                callback(status_code, err);
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
        token     : null,

        headers   : {
            'Accept'        : 'application/json',
            'Content-Type'  : 'application/json',
            'User-Agent'    : 'reachmail-node-' + VERSION,
            'Authorization' : 'token '
        },

        base_url  : 'services.reachmail.net'
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

    var opts = new request_options(this.options.base_url, url, "GET",
            this.options.headers, null);

    call_service(opts, callback);
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

    var opts = new request_options(this.options.base_url, url, 'POST',
            this.options.headers, content);

    call_service(opts, callback);
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

    var opts = new request_options(this.options.base_url, url, 'PUT',
            this.options.headers, content);

    call_service(opts, callback);
}

/*
 * `delete(url, callback)` root prototype for HTTP DELETE requests
 */
ReachMail.prototype.delete = function (url, callback) {
    if (typeof callback !== 'function') {
        throw "Invalid Callback, function required";
        return this;
    }

    var opts = new request_options(this.options.base_url, url, 'DELETE',
            this.options.headers, null);

    call_service(opts, callback);
}

/*
 * Administration services
 */
ReachMail.prototype.administrationUsersCurrent = function (callback) {
    this.get('/administration/users/current', callback);
}

ReachMail.prototype.administrationAddresses = function (accountid, callback) {
  this.get('/administration/addresses/' + accountid, callback);
}

/*
 * Campaign Services
 */
ReachMail.prototype.campaigns = function (accountid, callback) {
  this.post('/campaigns/' + accountid, body, callback);
}

ReachMail.prototype.campaignsMessageTesting = function (accountid, callback) {
  this.post('/campaigns/messagetesting/' + accountid, body, callback);
}

/* 
 * Data services
 */
ReachMail.prototype.dataUpload = function (body, callback) {
  this.post('/data/', body, callback);
}

ReachMail.prototype.dataDownload = function (dataid, callback) {
  this.get('/data/' + dataid, callback);
}

/*
** EasySmtpDelivery services
*/
ReachMail.prototype.easySmtpDelivery = function (accountId, body, callback) {
    this.post ("/easysmtp/" + accountId, body, callback);
}

/* 
 * List services
 */
ReachMail.prototype.listsCreate = function (accountid, body, callback) {
  this.post('/lists/' + accountid, body, callback);
}

ReachMail.prototype.listsInformation = function (accountid, listid, callback) {
  var u = util.format('/lists/%s/%s', accountid, listid);
  this.get(u, callback);
}

ReachMail.prototype.listsModify = function (accountid, listid, body, callback) {
  var u = util.format('/lists/%s/%s', accountid, listid);
  this.put(u, body, callback);
}

ReachMail.prototype.listsDelete = function (accountid, listid, callback) {
  var u = util.format('/lists/%s/%s', accountid, listid);
  this.delete(u, callback);
}

ReachMail.prototype.listsExport = function (accountid, listid, body, callback) {
  var u = util.format('/lists/export/%s/%s', accountid, listid);
  this.post(u, body, callback);
}

ReachMail.prototype.listsExportStatus = function (accountid, exportid, cb) {
  var u = util.format('/lists/export/status/%s/%s', accountid, exportid);
  this.get(u, cb);
}

ReachMail.prototype.listsFields = function (accoutid, callback) {
  this.get('/lists/' + accountid, callback);
}

ReachMail.prototype.listsFiltered = function (accountid, body, callback) {
  this.post('/lists/filtered/' + accountid, body, callback);
}

ReachMail.prototype.listsGroupCreate = function (accountid, body, callback) {
  this.post('/lists/groups/' + accountid, body, callback);
}

ReachMail.prototype.listsGroupInformation = function (accountid, groupid, cb) {
  var u = util.format('/lists/groups/%s/%s', accountid, groupid);
  this.get(u, callback);
}

ReachMail.prototype.listsGroupModify = function (accountid, groupid, body, cb) {
  var u = util.format('/lists/groups/%s/%s', accountid, groupid);
  this.post(u, body, cb);
}

ReachMail.prototype.listsGroupDelete = function (accountid, groupid, callback) {
  var u = util.format('/lists/groups/%s/%s', accountid, groupid);
  this.delete(u, callback);
}

ReachMail.prototype.listsImport = function (accountid, listid, body, callback) {
  var u = util.format('/lists/import/%s/%s', accountid, listid);
  this.post(u, body, callback);
}

ReachMail.prototype.listsImportStatus = function (accountid, importid, cb) {
  var u = util.format('/lists/import/%s/%s', accountid, importid);
  this.get(u, callback);
}

ReachMail.prototype.listsOptOut = function (accountid, listid, body, callback) {
  var u = util.format('/lists/optout/%s/%s', accountid, listid);
  this.post(u, body, callback);
}

ReachMail.prototype.listsRecipientsCreate = function (accountid, listid, body, 
    callback) {
  var u = util.format('/lists/recipients/%s/%s', accountid, listid);
  this.post(u, body, callback);
}

ReachMail.prototype.listsRecipientsInformation = function (accountid, listid,
    email, callback) {
  var u = util.format('/lists/recipients/%s/%s/%s', accountid, listid, email);
  this.get(u, callback);
}

ReachMail.prototype.listsRecipientsDelete = function (accountid, listid, email,
    callback) {
  var u = util.format('/lists/recipients/%s/%s/%s', accountid, listid, email);
  this.delete(u, callback);
}

ReachMail.prototype.listsRecipientsModify = function (accountid, listid, email,
    body, callback) {
  var u = util.format('/lists/recipients/%s/%s/%s', accountid, listid, email);
  this.put(u, body, callback);
}

ReachMail.prototype.listsRecipientsFiltered = function (accountid, listid,
    body, callback) {
  var u = util.format('/lists/recipients/filtered/%s/%s', accountid, listid);
  this.post(u, body, callback);
}

ReachMail.prototype.listsRecipientsFilteredDelete = function (accountid, listid,
    body, callback) {
  var u = util.format('/lists/recipients/filtered/delete/%s/%s', accountid,
      listid);
  this.post(u, body, callback);
}

ReachMail.prototype.listsRecipientsFilteredModify = function (accountid, listid, 
    body, callback) {
  var u = util.format('/lists/recipients/filtered/modify/%s/%s', accountid,
      listid);
  this.post(u, body, callback);
}

ReachMail.prototype.listsRecipientsFilteredSubscribe = function (accountid,
    listid, body, callback) {
  var u = util.format('/lists/recipients/filtered/subscribe/%s/%s', accountid,
      listid);
  this.post(u, body, callback);
}

ReachMail.prototype.listsSubscriptionForm = function (accountid, formid, cb) {
  var u = util.format('/lists/subscriptionform/%s/%s', accountid, formid);
  this.get(u, callback);
}

ReachMail.prototype.listsSubscriptionFormFiltered = function (accountid, cb) {
  var u = util.format('/lists/subscriptionform/%s', accountid);
  this.get(u, callback);
}

/* 
 * Mailing services
 */
ReachMail.prototype.mailingsCreate = function (accountid, body, callback) {
  this.post('/mailings/' + accountid, body, callback);
}

ReachMail.prototype.mailingsInformation = function (accountid, mailingid, cb) {
  var u = util.format('/mailings/%s/%s', accountid, mailingid);
  this.get(u, callback);
}

ReachMail.prototype.mailingsModify = function (accountid, mailingid, body, cb) {
  var u = util.format('/mailings/%s/%s', accountid, mailingid);
  this.post(u, body, callback);
}

ReachMail.prototype.mailingsDelete = function (accountid, mailingid, cb) {
  var u = util.format('/mailings/%s/%s', accountid, mailingid);
  this.delete(u, cb);
}

ReachMail.prototype.mailingsFiltered = function (accountid, body, callback) {
  this.post('/mailings/' + accountid, body, callback);
}

ReachMail.prototype.mailingsGroups = function (accountid, callback) {
  this.get('/mailings/groups/' + accountid, callback);
}

ReachMail.prototype.mailingsGroupsCreate = function (accountid, body, callback) {
  this.post('/mailings/groups/' + accountid, body, callback);
}

ReachMail.prototype.mailingsGroupsInformation = function (accountid, groupid,
    callback) {
  var u = util.format('/mailings/groups/%s/%s', accountid, groupid);
  this.get(u, callback);
}

ReachMail.prototype.mailingsGroupsModify = function (accountid, groupid, body,
    callback) {
  var u = util.format('/mailings/groups/%s/%s', accountid, groupid);
  this.put(u, body, callback);
}

ReachMail.prototype.mailingsGroupsDelete = function (accountid, groupid,
    callback) {
  var u = util.format('/mailings/groups/%s/%s', accountid, groupid);
  this.delete(u, callback);
}

/*
 * Reporting services
 */
ReachMail.prototype.reportsMailingsBouncesDetail = function (accountid,
    mailingid, body, callback) {
  var u = util.format('/reports/mailings/bounces/detail/%s/%s', accountid,
      mailingid);
  this.post(u, body, callback);
}

ReachMail.prototype.reportsMailingsDetail = function (accountid, mailingid, 
    body, callback) {
  var u = util.format('/reports/mailings/detail/%s/', accountid);
  this.post(u, body, callback);
}

ReachMail.prototype.reportsMailingsDetailInformation = function (accountid,
    mailingid, callback) {
  var u = util.format('/reports/mailings/detail/%s/%s', accountid, mailingid);
  this.get(u, callback);
}

ReachMail.prototype.reportsMailingsMessageTesting = function (accountid, 
    body, callback) {
  this.post('/reports/mailings/messagetesting/%s' + accountid, body, callback);
}

ReachMail.prototype.reportsMailingsOpensDetail = function (accountid,
    mailingid, body, callback) {
  var u = util.format('/reports/mailings/opens/detail/%s/%s', accountid, 
      mailingid);
  this.post(u, body, callback);
}

ReachMail.prototype.reportsMailingsOptOutsDetail = function (accountid,
    mailingid, body, callback) {
  var u = util.format('/reports/mailings/optouts/detail/%s/%s', accountid, 
      mailingid);
  this.post(u, body, callback);
}
  
ReachMail.prototype.reportsMailingsTrackedLinksDetail = function (accountid,
    mailingid, body, callback) {
  var u = util.format('/reports/mailings/trackedlinks/detail/%s/%s', accountid, 
      mailingid);
  this.post(u, body, callback);
}

ReachMail.prototype.reportsMailingsTrackedLinksSummary = function (accountid,
    mailingid, callback) {
  var u = util.format('/reports/mailings/trackedlinks/summary/%s/%s', 
      accountid, mailingid);
  this.get(u, callback);
}

ReachMail.prototype.reportsMailingsTrackedLinksSummaryList = function(accountid,
    mailingid, listid, callback) {
  var u = util.format('/reports/mailings/trackedlinks/summary/%s/%s/%s', 
      accountid, mailingid, listid);
  this.get(u, callback);
}

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

ReachMail.prototype.reportsEasySmtpOpens = function (accountId, startDate,
        endDate, callback) {
    var u = util.format("/reports/easysmtp/opens/%s?startdate=%s&enddate=%s", 
        accountId, startDate, endDate);
    this.get (u, callback);
}

ReachMail.prototype.reportsEasySmtpClicks = function (accountId, startDate,
        endDate, callback) {
    var u = util.format("/reports/easysmtp/clicks/%s?startdate=%s&enddate=%s", 
        accountId, startDate, endDate);
    this.get (u, callback);
}
