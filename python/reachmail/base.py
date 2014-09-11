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
import collections

BASEURL = "https://services.reachmail.net"

ServiceInformation = collections.namedtuple("ServiceInformation",
    "uri method")


class BaseResource(object):


    def __init__(self, **kwargs):
        self.kwargs = kwargs

    def request(self, method, data=None):
        return self.kwargs["request_func"](method, self.uri, data) 


class Administration(BaseResource):


    def users_current(self):
        self.uri = BASEURL + "/administration/users/current"
        return self.request("GET")

    def addresses(self, **kwargs):
        self.uri = BASEURL + "/administration/addresses/%(AccountId)s" % kwargs
        return self.request("GET")


class Campaigns(BaseResource):

    def send(self, **kwargs):
        self.uri = BASEURL + "/campaigns/%(AccountId)s" % kwargs
        return self.request("POST", data=kwargs["Queue"])

    def campaigns_messagetesting(self, **kwargs):
        self.uri = BASEURL + "/campaigns/messagetesting/%(AccountId)s" % kwargs
        return self.request("POST", data=kwargs["Queue"])

    def campaigns_messagetesting_remainder(self, **kwargs):
        self.uri = BASEURL \
            + "/campaigns/messagetesting/%(AccountId)s/%(CampaignId)s" % kwargs
        return self.request("POST", data=kwargs["Queue"])

class Data(BaseResource):


    def upload(self, **kwargs):
        self.uri = BASEURL + "/data"
        return self.request("POST", data=kwargs["Data"])

    def download(self, **kwargs):
        self.uri = BASEURL + "/data/%(DataId)s" % kwargs
        return self.request("GET")

class EasySmtp(BaseResource):

    def delivery(self, **kwargs):
        self.uri = BASEURL + "/easysmtp/%(AccountId)s" % kwargs
        return self.request("POST", data=kwargs["Data"])

class Lists(BaseResource):


    def create(self, **kwargs):
        self.uri = BASEURL + "/lists/%(AccountId)s" % kwargs
        return self.request("POST", data=kwargs["ListProperties"])

    def information(self, **kwargs):
        self.uri = BASEURL + "/lists/%(AccountId)s/%(ListId)" % kwargs
        return self.request("GET")

    def modify(self, **kwargs):
        self.uri = BASEURL + "/lists/%(AccountId)s/%(ListId)" % kwargs
        return self.request("PUT", data=kwargs["ListProperties"])

    def delete(self, **kwargs):
        self.uri = BASEURL + "/lists/%(AccountId)s/%(ListId)" % kwargs
        return self.request("DELETE")

    def export(self, **kwargs):
        self.uri = BASEURL + "/lists/export/%(AccountId)s/%(ListId)" % kwargs
        return self.request("POST", data=kwargs["ExportParameters"])

    def export_status(self, **kwargs):
        self.uri = (BASEURL
            + "/lists/export/status/%(AccountId)s/%(ExportId)s" % kwargs)
        return self.request("GET")

    def fields(self, **kwargs):
        self.uri = BASEURL + "/lists/fields/%(AccountId)s" % kwargs
        return self.request("GET")
        
    def filtered(self, **kwargs):
        self.uri = BASEURL + "/lists/filtered/%(AccountId)s" % kwargs
        return self.request("POST", data=kwargs["ListFilter"])

    def groups(self, **kwargs):
        self.uri = BASEURL + "/lists/groups/%(AccountId)s" % kwargs
        return self.request("GET")

    def groups_create(self, **kwargs):
        self.uri = BASEURL + "/lists/groups/%(AccountId)s" % kwargs
        return self.request("POST", data=kwargs["GroupProperties"])

    def groups_information(self, **kwargs):
        self.uri = BASEURL + "/lists/groups/%(AccountId)s/%(GroupId)s" % kwargs
        return self.request("GET")

    def groups_modify(self, **kwargs):
        self.uri = BASEURL + "/lists/groups/%(AccountId)s/%(GroupId)s" % kwargs
        return self.request("PUT", data=kwargs["GroupProperties"])

    def groups_delete(self, **kwargs):
        self.uri = BASEURL + "/lists/groups/%(AccountId)s/%(GroupId)s" % kwargs
        return self.request("DELETE")

    def import_list(self, **kwargs):
        self.uri = BASEURL + "/lists/import/%(AccountId)s/%(ListId)s" % kwargs
        return self.request("POST", data=kwargs["Parameters"])

    def import_status(self, **kwargs):
        self.uri = (BASEURL
            + "/lists/import/status/%(AccountId)s/%(ImportId)s" % kwargs)
        return self.request("GET")

    def optout(self, **kwargs):
        self.uri = BASEURL + "/lists/optout/%(AccountId)s/%(ListId)s" % kwargs
        return self.request("POST", data=kwargs["Recipient"])

    def recipients_create(self, **kwargs):
        self.uri = (BASEURL
            + "/lists/recipients/%(AccountId)s/%(ListId)s" % kwargs)
        return self.request("POST", data=kwargs["RecipientProperties"])

    def recipients_information(self, **kwargs):
        self.uri = (BASEURL
            + "/lists/recipients/%(AccountId)s/%(ListId)s/%(Email)s" % kwargs)
        return self.request("GET")

    def recipients_delete(self, **kwargs):
        self.uri = (BASEURL
            + "/lists/recipients/%(AccountId)s/%(ListId)s/%(Email)s" % kwargs)
        return self.request("DELETE")

    def recipients_modify(self, **kwargs):
        self.uri = (BASEURL
            + "/lists/recipients/%(AccountId)s/%(ListId)s/%(LookupEmail)s"
            % kawrgs)
        return self.request("PUT", data=kwargs["RecipientProperties"])

    def recipients_filtered(self, **kwargs):
        self.uri = (BASEURL 
            + "/lists/recipients/filtered/%(AccountId)s/%(ListId)s" % kwargs)
        return self.request("POST", data=kwargs["RecipientFilter"])

    def recipients_filtered_delete(self, **kwargs):
        self.uri = (BASEURL 
            + "/lists/recipients/filtered/delete/%(AccountId)s/%(ListId)s" 
            % kwargs)
        return self.request("POST", data=kwargs["RecipientFilter"])

    def recipients_filtered_modify(self, **kwargs):
        self.uri = (BASEURL 
            + "/lists/recipients/filtered/modify/%(AccountId)s/%(ListId)s" 
            % kwargs)
        return self.request("POST", data=kwargs["RecipientParameters"])

    def recipients_filtered_subscribe(self, **kwargs):
        self.uri = (BASEURL 
            + "/lists/recipients/filtered/subscribe/%(AccountId)s/%(ListId)s" 
            % kwargs)
        return self.request("POST", data=kwargs["RecipientParameters"])

    def subscriptionform(self, **kwargs):
        self.uri = (BASEURL
            + "/lists/subscriptionform/%(AccountId)s/%(SubscriptionFormId)s"
            % kwargs)
        return self.request("GET")

    def subscriptionform_filtered(self, **kwargs):
        self.uri = (BASEURL
            + "/lists/subscriptionform/filtered/%(AccountId)s" % kwargs)
        return self.request("POST", data=kwargs["SubscriptionFormFilter"])


class Mailings(BaseResource):


    def create(self, **kwargs):
        self.uri = BASEURL + "/mailings/%(AccountId)s" % kwargs
        return self.request("POST", data=kwargs["MailingProperties"])

    def information(self, **kwargs):
        self.uri = BASEURL + "/mailings/%(AccountId)s/%(MailingId)s" % kwargs
        return self.request("GET")

    def modify(self, **kwargs):
        self.uri = BASEURL + "/mailings/%(AccountId)s/%(MailingId)s" % kwargs
        return self.request("PUT", data=kwargs["MailingProperties"])

    def delete(self, **kwargs):
        self.uri = BASEURL + "/mailings/%(AccountId)s/%(MailingId)s" % kwargs
        return self.request("DELETE")

    def filtered(self, **kwargs):
        self.uri = BASEURL + "/mailings/filtered/%(AccountId)s" % kwargs
        return self.request("POST", data=kwargs["MailingFilter"])

    def groups(self, **kwargs):
        self.uri = BASEURL + "/mailings/groups/%(AccountId)s" % kwargs
        return self.request("GET")

    def groups_create(self, **kwargs):
        self.uri = BASEURL + "/mailings/groups/%(AccountId)s" % kwargs
        return self.request("POST", data=kwargs["GroupProperties"])

    def groups_information(self, **kwargs):
        self.uri = (BASEURL
            + "/mailings/groups/%(AccountId)s/%(GroupId)s" % kwargs)
        return self.request("GET")

    def groups_modify(self, **kwargs):
        self.uri = (BASEURL
            + "/mailings/groups/%(AccountId)s/%(GroupId)s" % kwargs)
        return self.request("PUT", data=kwargs["GroupProperties"])

    def groups_delete(self, **kwargs):
        self.uri = (BASEURL
            + "/mailings/groups/%(AccountId)s/%(GroupId)s" % kwargs)
        return self.request("DELETE")


class Reports(BaseResource):


    def easysmtp_mailings(self, **kwargs):
        self.uri = (BASEURL 
            + "/reports/easysmtp/mailings/%(AccountId)s"
            + "?enddate=%(enddate)s&startdate=%(startdate)s" % kwargs)
        return self.request("GET")

    def mailings_bounces_detail(self, **kwargs):
        self.uri = (BASEURL
            + "/reports/mailings/bounces/detail/%(AccountId)s/%(MailingId)s"
            % kwargs)
        return self.request("POST", data=kwargs["DetailReportFilter"])

    def mailings_detail(self, **kwargs):
        self.uri = BASEURL + "/reports/mailings/detail/%(AccountId)s" % kwargs
        return self.request("POST", data=kwargs["MailingReportFilter"])

    def mailings_detail_information(self, **kwargs):
        self.uri = (BASEURL
            + "/reports/mailings/detail/%(AccountId)s/%(MailingId)s" % kwargs)
        return self.request("GET")

    def mailings_messgetesting(self, **kwargs):
        self.uri = (BASEURL
            + "/reports/mailings/messagetesting/%(AccountId)s" % kwargs)
        return self.request("POST", data=kwargs["MailingReportFilter"])

    def mailings_opens_detail(self, **kwargs):
        self.uri = (BASEURL
            + "/reports/mailings/opens/detail/%(AccountId)s/%(MailingId)s"
            % kwargs)
        return self.request("POST", data=kwargs["DetailReportFilter"])

    def mailings_optouts_detail(self, **kwargs):
        self.uri = (BASEURL
            + "/reports/mailings/optouts/detail/%(AccountId)s/%(MailingId)s"
            % kwargs)
        return self.request("POST", data=kwargs["DetailReportFilter"])

    def mailings_trackedlinks_detail(self, **kwargs):
        self.uri = (BASEURL
            + "/reports/mailings/trackedlinks/detail/"
            + "%(AccountId)s/%(MailingId)s"
            % kwargs)
        return self.request("POST", data=kwargs["Filter"])

    def mailings_trackedlinks_summary(self, **kwargs):
        self.uri = (BASEURL
            + "/reports/mailings/trackedlinks/summary/"
            + "%(AccountId)s/%(MailingId)s"
            % kwargs)
        return self.request("GET")

    def mailings_trackedlinks_summary_list(self, **kwargs):
        self.uri = (BASEURL
            + "/reports/mailings/trackedlinks/summary/"
            + "%(AccountId)s/%(MailingId)s/%(ListId)s"
            % kwargs)
        return self.request("GET")
