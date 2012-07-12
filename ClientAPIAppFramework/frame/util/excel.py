r""" Classes for buliding Excel reports """
import sys, os
from datetime import date

class ExcelReport(object):
	"""
	@template - file object, unread
	@report - MailingReport object
	@config - AppConfig object

	returns @output - string object for writing to a file
	"""
	def __init__(self, template, report, tracking, image_url):
		self.temp = template
		self.report = report
		self.tracking = tracking
		self.image = image_url
		self.html_tracking = [x for x in self.tracking.links 
			if x.linkmailingformat == 'Html']
		self.text_tracking = [x for x in self.tracking.links 
			if x.linkmailingformat == 'Text']
		
		# Replace the basic data
		self.output = self.temp.replace('{{report_image}}',
			self.image)
		self.output = self.output.replace('{{id}}',
			self.report.message.name.replace('/', '').replace('\\', ''))
		self.output = self.output.replace('{{title}}',
			self.report.message.name)
		self.output = self.output.replace('{{campaign_name}}',
			self.report.message.name)
		self.output = self.output.replace('{{report_date}}',
			date.today().strftime('%m/%d/%Y'))
		self.output = self.output.replace('{{report_range}}',
			'-- All Dates --')
		self.output = self.output.replace('{{s_sd}}',
			self.report.delivereddate)
		self.output = self.output.replace('{{s_ad}}',
			self.report.lastlistdelivereddate)
		self.output = self.output.replace('{{s_tr}}',
			self.report.recipientsummary.active)
		self.output = self.output.replace('{{s_sr}}',
			self.report.recipientsummary.suppressed)
		self.output = self.output.replace('{{s_ar}}',
			self.report.recipientsummary.sent)
		self.output = self.output.replace('{{s_sent}}',
			self.report.recipientsummary.received)
		self.output = self.output.replace('{{s_trr}}',
			self.report.recipientsummary.read)
		self.output = self.output.replace('{{s_pr}}',
			'%.2f' % (100 * ((float(self.report.recipientsummary.read) /
				float(self.report.recipientsummary.received)))))
		self.output = self.output.replace('{{s_b}}',
			self.report.recipientsummary.bounce)
		self.output = self.output.replace('{{s_sb}}',
			self.report.recipientsummary.softbounce)
		self.output = self.output.replace('{{s_hb}}',
			self.report.recipientsummary.hardbounce)
		self.output = self.output.replace('{{s_oo}}',
			self.report.recipientsummary.optout)
		self.output = self.output.replace('{{s_fbl}}',
			self.report.recipientsummary.spamreport)
		self.output = self.output.replace('{{loop_list}}',
			self.build_list_row())
		if self.html_tracking:
			self.output = self.output.replace('{{loop_track}}',
				self.build_tracking_row(self.html_tracking))
		else:
			self.output.replace('{{loop_track}}', '')
		if self.text_tracking:
			self.output = self.output.replace('{{loop_track_text}}',
				self.build_tracking_row(self.text_tracking))
		else: 
			self.output = self.output.replace('{{loop_track_text}}', '')
		self.output = self.output.replace('{{loop_forward}}',
			self.list_forward_summary())
		self.output = self.output.replace('{{loop_ftaf}}',
			self.ftf_summary())

	def build_list_row(self):
		list_row_template = """
			<tr align="center">
    		<td bgcolor="#{{loop_color}}" align="left" colspan="2">
			<font face="Arial, Helvetica, sans-serif" size="2">{{l_ln}}</font>
			</td>
    		<td bgcolor="#{{loop_color}}" width="129">
			<font face="Arial, Helvetica, sans-serif" size="2">{{l_s}}</font>
			</td>
    		<td bgcolor="#{{loop_color}}" width="129">
			<font face="Arial, Helvetica, sans-serif" size="2">
				{{l_suppressed}}</font>
			</td>
    		<td bgcolor="#{{loop_color}}" width="129">
			<font face="Arial, Helvetica, sans-serif" size="2">{{l_sent}}</font>
			</td>
    		<td bgcolor="#{{loop_color}}" width="110">
			<font face="Arial, Helvetica, sans-serif" size="2">{{l_d}}</font>
			</td>
    		<td bgcolor="#{{loop_color}}" width="139">
			<font face="Arial, Helvetica, sans-serif" size="2">{{l_r}}</font>
			</td>
    		<td bgcolor="#{{loop_color}}" width="59">
			<font face="Arial, Helvetica, sans-serif" size="2">{{l_pr}}</font>
			</td>
    		<td bgcolor="#{{loop_color}}" width="54">
			<font face="Arial, Helvetica, sans-serif" size="2">{{l_b}}</font>
			</td>
    		<td bgcolor="#{{loop_color}}" width="49">
			<font face="Arial, Helvetica, sans-serif" size="2">{{l_sb}}</font>
			</td>
    		<td bgcolor="#{{loop_color}}" width="51">
			<font face="Arial, Helvetica, sans-serif" size="2">{{l_hb}}</font>
			</td>
    		<td bgcolor="#{{loop_color}}" width="54">
			<font face="Arial, Helvetica, sans-serif" size="2">{{l_o}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" width="54">
			<font face="Arial, Helvetica, sans-serif" size="2">{{l_fbl}}</font>
			</td>
			</tr>
		"""
		full_list_rows = ''
		for ind, itm in enumerate(self.report.lists):
			row_color = 'FFFFFF'
			if ind % 2 == 0: # even row
				row_color = 'FFFF99'
			row = list_row_template.replace('{{loop_color}}', row_color)
			row = row.replace('{{l_ln}}', itm.listname)
			row = row.replace('{{l_s}}', itm.recipientcount.active)
			row = row.replace('{{l_suppressed}}', itm.recipientcount.suppressed)
			row = row.replace('{{l_sent}}', itm.recipientcount.sent)
			row = row.replace('{{l_d}}', itm.recipientcount.received)
			row = row.replace('{{l_r}}', itm.recipientcount.read)
			row = row.replace('{{l_pr}}', '%.2f' % (100 * (
				float(itm.recipientcount.read) /
				float(itm.recipientcount.received))))
			row = row.replace('{{l_b}}', itm.recipientcount.bounce)
			row = row.replace('{{l_sb}}', itm.recipientcount.softbounce)
			row = row.replace('{{l_hb}}', itm.recipientcount.hardbounce)
			row = row.replace('{{l_o}}', itm.recipientcount.optout)
			row = row.replace('{{l_fbl}}', itm.recipientcount.spamreport)
			full_list_rows += row
		return full_list_rows

	def build_tracking_row(self, tracking_source):
		link_row_template = """
			<tr align="center">
    		<td bgcolor="#{{loop_color}}" align="left" width="124" colspan=2>
			<font face="Arial, Helvetica, sans-serif" size="2">{{t_on}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" width="96">
			<font face="Arial, Helvetica, sans-serif" size="2">{{t_a}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" width="129">
			<font face="Arial, Helvetica, sans-serif" size="2">{{t_d}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" width="110">
			<font face="Arial, Helvetica, sans-serif" size="2">{{t_v}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" width="139">
			<font face="Arial, Helvetica, sans-serif" size="2">{{t_c}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" colspan="3">
			<font face="Arial, Helvetica, sans-serif" size="2">{{t_uc}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" colspan="2">
			<font face="Arial, Helvetica, sans-serif" size="2">{{t_ct}}</font>
			</td>
			</tr>
		"""
		tracking_rows = ''
		total_clicks = 0
		unique_clicks = 0
		for ind, itm in enumerate(tracking_source):
			row_color = 'FFFFFF'
			if ind % 2 == 0:
				row_color = 'FFFF99'
			row = link_row_template.replace('{{loop_color}}', row_color)
			row = row.replace('{{t_on}}', itm.name)
			row = row.replace('{{t_a}}', '--None--')
			row = row.replace('{{t_d}}', self.report.delivereddate)
			row = row.replace('{{t_v}}', self.report.recipientsummary.read)
			row = row.replace('{{t_c}}', itm.totalclicks)
			row = row.replace('{{t_uc}}', itm.uniqueclicks)
			row = row.replace('{{t_ct}}', '%.2f' % (100 * (
				float(itm.uniqueclicks) /
				float(self.report.recipientsummary.read))))
			total_clicks = total_clicks + int(itm.totalclicks)
			unique_clicks = unique_clicks + int(itm.uniqueclicks)
			tracking_rows += row
		# Summary
		row = link_row_template.replace('{{loop_color}}', 'FFFFFF')
		row = row.replace('{{t_on}}', 'Link Summary')
		row = row.replace('{{t_a}}', '--None--')
		row = row.replace('{{t_d}}', self.report.delivereddate)
		row = row.replace('{{t_v}}', self.report.recipientsummary.read)
		row = row.replace('{{t_c}}', str(total_clicks))
		row = row.replace('{{t_uc}}', str(unique_clicks))
		row = row.replace('{{t_ct}}', '%.2f' % (100 * (
			float(unique_clicks) / float(self.report.recipientsummary.read))))
		tracking_rows += row
		return tracking_rows
	
	def list_forward_summary(self):
		forward_row_template = """
			<tr align="center">
			<td bgcolor="#{{loop_color}}" align="left" colspan="3">
			<font face="Arial, Helvetica, sans-serif" size="2">{{f_ln}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" colspan="2">
			<font face="Arial, Helvetica, sans-serif" size="2">{{f_s}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" colspan="6">
			<font face="Arial, Helvetica, sans-serif" size="2">{{f_f}}</font>
			</td>
			</tr>
		"""
		forward_rows = ''
		for ind, itm in enumerate(self.report.lists):
			row_color = 'FFFFFF'
			if ind % 2 == 0:
				row_color = 'FFFF99'
			row = forward_row_template.replace('{{loop_color}}', row_color)
			row = row.replace('{{f_ln}}', itm.listname)
			row = row.replace('{{f_s}}', itm.recipientcount.active)
			row = row.replace('{{f_f}}', itm.forwardsummary.total)
			forward_rows += row
		return forward_rows

	def ftf_summary(self):
		ftf_row_template = """
			<tr align="center">
			<td bgcolor="#{{loop_color}}" align="left" colspan="2">
			<font face="Arial, Helvetica, sans-serif" size="2">{{f_ln}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" colspan="2">
			<font face="Arial, Helvetica, sans-serif" size="2">{{f_email}}</font>
			</td>
    		<td bgcolor="#{{loop_color}}" colspan="1">
			<font face="Arial, Helvetica, sans-serif" size="2">{{f_fb}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" colspan="1">
			<font face="Arial, Helvetica, sans-serif" size="2">{{f_tw}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" colspan="1">
			<font face="Arial, Helvetica, sans-serif" size="2">{{f_li}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" colspan="1">
			<font face="Arial, Helvetica, sans-serif" size="2">{{f_ms}}</font>
			</td>
			<td bgcolor="#{{loop_color}}" colspan="2">
			<font face="Arial, Helvetica, sans-serif" size="2">{{f_gp}}</font>
			</td>
			</tr>
		"""
		ftf_rows = ''
		for ind, itm in enumerate(self.report.lists):
			row_color = 'FFFFFF'
			if ind % 2 == 0:
				row_color = 'FFFF99'
			row = ftf_row_template.replace('{{loop_color}}', row_color)
			row = row.replace('{{f_ln}}', itm.listname)
			row = row.replace('{{f_email}}', itm.forwardsummary.sendtofriend)
			row = row.replace('{{f_fb}}', itm.forwardsummary.facebook)
			row = row.replace('{{f_tw}}', itm.forwardsummary.twitter)
			row = row.replace('{{f_li}}', itm.forwardsummary.linkedin)
			row = row.replace('{{f_ms}}', itm.forwardsummary.myspace)
			row = row.replace('{{f_gp}}', itm.forwardsummary.googlebuzz)
			ftf_rows += row
		return ftf_rows
