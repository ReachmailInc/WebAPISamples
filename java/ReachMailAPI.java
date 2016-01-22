import java.io.IOException;

public class ReachMailAPI {
  private String _version    = "1.0";
  private String _user_agent = "reachmail_api.java.v";
  private String _token;

  public ReachMailAPI (String token) {
    _token = token;
  }

  public String token () {
    return _token;
  }

  public void token (String token) {
    _token = token;
  }

  public APIResponse get (String uri) throws IOException {
    APIRequest request = new APIRequest(uri, "GET", token());
    request.initConnection();
    return request.makeRequest();
  }

  public APIResponse put (String uri, String payload) throws IOException {
    APIRequest request = new APIRequest(uri, "PUT", token());
    return request.makeRequest(payload);
  }

  public APIResponse post (String uri, String payload) throws IOException {
    APIRequest request = new APIRequest(uri, "PUT", token());
    return request.makeRequest(payload);
  }

  public APIResponse delete (String uri) throws IOException {
    APIRequest request = new APIRequest(uri, "DELETE", token());
    request.initConnection();
    return request.makeRequest();
  }

  public APIResponse administrationUsersCurrent () throws IOException {
    return get("/administration/users/current");
  }

  public APIResponse administrationAddresses (String account_id) 
    throws IOException {
    return get(String.format("/administration/addresses/%s", account_id));
  }

  public APIResponse campaigns (String account_id, String payload)
    throws IOException {
    return post(String.format("/campaigns/%s", account_id), payload);
  }

  public APIResponse campaignsMessageTesting (String account_id, String payload)
    throws IOException {
    return post(String.format("/campaigns/messagetesing/%s", account_id), 
      payload);
  }

  public APIResponse dataUpload (String payload) throws IOExeption {
    return post("/data/", payload);
  }

  public APIResponse dataDownload (String data_id) throws IOException {
    return get(String.format("/data/%s", data_id));
  }

  public APIResponse easysmtp (String account_id, String payload) 
    throws IOException {
    return post(String.format("/easysmtp/%s", account_id), payload);
  }

  public APIResponse listsCreate (String account_id, String payload)
    throws IOException {
    return post(String.format("/lists/%s", account_id), payload);
  }

  public APIResponse listsInformation (String account_id, String list_id)
    throws IOException {
    return get(String.format("/lists/%s/%s", account_id, list_id);
  }

  public APIResponse listsModify (String account_id, String list_id, 
    String payload) throws IOException {
    return put(String.format("/lists/%s/%s", account_id, list_id), payload);
  }

  public APIResponse listsDelete (String account_id, String list_id)
    throws IOException {
    return delete(String.format("/lists/%s/%s", account_id, list_id));
  }

  public APIResponse listsExport (String account_id, String list_id,
    String payload) throws IOException {
    return post(String.format("/lists/%s/%s", account_id, list_id), payload);
  }

  public APIResponse listsExportStatus (String account_id, String export_id)
    throws IOException {
    return get(String.format("/lists/export/status/%s/%s", account_id, 
      export_id));
  }

  public APIResponse listsFields (String account_id) throws IOException {
    return get(String.format("/lists/%s", account_id));
  }

  public APIResponse listsFiltered (String account_id, String payload)
    throws IOException {
    return post(String.format("/lists/filtered/%s", account_id), payload);
  }

  public APIResponse listsGroupCreate (String account_id, String payload)
    throws IOException {
    return post(String.format("/lists/groups/%s", account_id));
  }

  public APIRResponse listsGroupInformation (String account_id, String group_id)
    throws IOException {
    return get(String.format("/lists/groups/%s/%s", account_id, group_id));
  }

  public APIResponse listsGroupModify (String account_id, String group_id,
    String payload throws IOException {
    return post(String.format("/lists/groups/%s/%s", account_id, group_id),
      payload);
  }

  public APIResponse listsGroupDelete (String account_id, String group_id) 
    throws IOException {
    return delete(String.format("/lists/groups/%s/%s", account_id, group_id));
  }

  public APIResponse listsImport (String account_id, String list_id, 
    String payload) throws IOException {
    return post(String.format("/lists/import/%s/%s", account_id, list_id),
      payload);
  }

  public APIResponse listsImportStatus (String account_id, String import_id)
    throws IOException {
    return get(String.format("/lists/import/%s/%s", account_id, import_id));
  }

  public APIResponse listsOptOut (String account_id, String list_id, 
    String payload) throws IOException {
    return post(String.format("/lists/optout/%s/%s", account_id, list_id));
  }

  public APIResponse listsRecipientsCreate (String account_id, String list_id,
    String Payload) throws IOException {
    return post(String.format("/lists/recipients/%s/%s", account_id, list_id),
      payload);
  }

  public APIResponse listsRecipientsInformation (String account_id,
    String list_id, String email) throws IOException {
    return get(String.format("/lists/recipients/%s/%s/%s", account_id, list_id,
      email));
  }

  public APIResponse listsRecipientsDelete (String account_id, String list_id,
    String email) throws IOException {
    return delete(String.format("/lists/recipients/%s/%s/%s", account_id, 
      list_id, email));
  }

  public APIResponse listsRecipientsModify (String account_id, String list_id,
    String email, String payload) throws IOException {
    return post(String.format("/lists/recipients/%s/%s/%s", account_id, 
      list_id, email), payload);
  }

  public APIResponse listsRecipientsFiltered (String account_id, String list_id,
    String payload) throws IOException {
    return post(String.format("/lists/recipients/filtered/%s/%s", account_id, 
      list_id), payload);
  }

  public APIResponse listsRecipientsFilteredDelete (String account_id,
    String list_id, String payload) throws IOException {
    return post(String.format("/lists/recipients/filtered/delete/%s/%s",
      account_id, list_id), payload);
  }

  public APIResponse listsRecipientsFilteredModify (String account_id,
    String list_id, String payload) throws IOException {
    return post(String.format("/lists/recipients/filtered/modify/%s/%s",
      account_id, list_id), payload);
  }

  public APIResponse listsRecipientsFilteredSubscribe (String account_id,
    String list_id, String payload) throws IOException {
    return post(String.format("/lists/recipients/filtered/subscribe/%s/%s",
      account_id, list_id), payload);
  }

  public APIResponse listsSubscriptionForm (String account_id, String form_id)
    throws IOException {
    return get(String.format("/lists/subscriptionform/%s/%s", account_id,
      form_id));
  }

  public APIResponse listsSubscriptionFormFiltered (String account_id, 
    String payload) throws IOException {
    return post(String.format("/lists/subscriptionform/filtered/%s", 
      account_id), payload);
  }

  public APIResponse mailingsCreate (String account_id, String payload) 
    throws IOException {
    return post(String.format("/mailings/%s", account_id), payload);
  }

  public APIResponse mailingsInformation (String account_id, String mailing_id)
    throws IOException {
    return get(String.format("/mailings/%s/%s", account_id, mailing_id));
  }

  public APIResponse mailingsModify (String account_id, String mailing_id,
    String payload) throws IOException {
    return put(String.format("/mailings/%s/%s", account_id, mailing_id),
      payload);
  }

  public APIResponse mailingsDelete (String account_id, String mailing_id)
    throws IOException {
    return delete(String.format("/mailings/%s/%s", account_id, mailing_id));
  }

  public APIResponse mailingsFiltered (String account_id, String payload) 
    throws IOException {
    return post(String.format("/mailings/%s", account_id), payload);
  }

  public APIResponse mailingsGroups (String account_id) throws IOException {
    return get(String.format("/mailings/%s", account_id));
  }

  public APIResponse mailingsGroupsCreate (String account_id, String payload) 
    throws IOException {
    return post(String.format("/mailings/%s", account_id), payload);
  }

  public APIResponse mailingsGroupsInformation (String account_id, 
    String group_id) throws IOException {
    return get(String.format("/mailings/%s/%s", account_id, group_id));
  }

  public APIResponse mailingsGroupsModify (String account_id, String group_id,
    String payload) throws IOException {
    return put(String.format("/mailings/%s/%s", account_id, group_id),
      payload);
  }

  public APIResponse mailingsGroupsDelete(String account_id, String group_id) 
    throws IOException {
    return delete(String.format("/mailings/%s/%s", account_id, group_id));
  }

  public APIResponse reportsMailingsBouncesDetail (String account_id,
    String mailing_id, String payload) throws IOException {
    return post(String.format("/reports/mailings/bounces/detail/%s/%s",
      account_id, mailing_id), payload);
  }

  public APIResponse reportsMailingsDetail (String account_id, String payload) 
    throws IOException {
    return post(String.format("/reports/mailings/detail/%s", account_id),
      payload);
  }

  public APIResponse reportsMailingsDetailInformation (String account_id,
    String mailing_id) throws IOException {
    return get(String.format("/reports/mailings/detail/%s/%s", account_id,
      mailing_id));
  }

  public APIResponse reportsMailingsMessageTesting (String account_id,
    String payload) throws IOException {
    return post(String.format("/reports/mailings/mesagetesting/%s", account_id),
      payload);
  }

  public APIResponse reportsMailingsOpenDetail (String account_id,
    String mailing_id, String payload) throws IOException {
    return post(String.format("/reports/mailings/opens/detail/%s/%s", 
      account_id, mailing_id), payload);
  }

  public APIResponse reportsMailingsOptOutsDetail (String account_id,
    String mailing_id, String payload) throws IOException {
    return post(String.format("/reports/mailings/optouts/detail/%s/%s",
      account_id, mailing_id), payload);
  }

  public APIResponse reportsMailingsTrackedLinksDetail (String account_id,
    String mailing_id, String payload) throws IOException {
    return post(String.format("/reports/mailings/trackedlinks/detail/%s/%s",
      account_id, mailing_id), payload);
  }

  public APIResponse reportsMailingsTrackedLinksSummary (String account_id,
    String mailing_id) throws IOException {
    return get(String.format("/reports/mailings/trackedlinks/summary/%s/%s",
      account_id, mailing_id));
  }

  public APIResponse reportsMailingsTrackedLinksSummaryList (String account_id,
    String mailing_id, String list_id) throws IOException {
    return get(String.format("/reports/mailings/trackedlinks/summary/%s/%s/%s",
      account_id, mailing_id, list_id));
  }

  public APIResponse reportsEasySmtp (String account_id, String start, 
    String end) throws IOException {
    return get(String.format("/reports/easysmtp/%s?startdate=%s&enddate=%s",
      account_id, start, end));
  }

  public APIResponse reportsEasySmtpBounces (String account_id, String start, 
    String end) throws IOException {
    return get(
      String.format("/reports/easysmtp/bounces/%s?startdate=%s&enddate=%s",
      account_id, start, end));
  }

  public APIResponse reportsEasySmtpOptouts (String account_id, String start, 
    String end) throws IOException {
    return get(
      String.format("/reports/easysmtp/optouts/%s?startdate=%s&enddate=%s",
      account_id, start, end));
  }

  public APIResponse reportsEasySmtpOpens (String account_id, String start, 
    String end) throws IOException {
    return get(
      String.format("/reports/easysmtp/opens/%s?startdate=%s&enddate=%s",
      account_id, start, end));
  }

  public APIResponse reportsEasySmtpClicks (String account_id, String start, 
    String end) throws IOException {
    return get(
      String.format("/reports/easysmtp/clicks/%s?startdate=%s&enddate=%s",
      account_id, start, end));
  }
}
