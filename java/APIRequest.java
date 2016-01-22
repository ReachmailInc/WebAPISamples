import java.net.URL;
import java.net.HttpURLConnection;

import java.io.IOException;
import java.io.InputStream;
import java.io.PrintStream;
import java.io.BufferedReader;
import java.io.InputStreamReader;

public class APIRequest
{
  private String _path;
  private String _method;
  private String _token;
  private String _host        = "https://services.reachmail.net";
  private String _version     = "1.0";
  private String _user_agent  = "reachmail-api-java-v";

  public APIRequest (String path, String method, String token)
  {
    _path   = path;
    _token  = token;
    _method = method;
  }

  public String userAgent ()
  {
    return _user_agent + _version;
  }

  public URL serviceUrl () throws IOException
  {
    return new URL(_host + _path);
  }

  protected HttpURLConnection initConnection () throws IOException
  {
    URL url = serviceUrl();
    HttpURLConnection connection = (HttpURLConnection)url.openConnection();

    connection.setRequestMethod(_method);
    connection.setDoOutput(false);
    connection.setAllowUserInteraction(false);

    connection.setRequestProperty("Content-Type",   "application/json");
    connection.setRequestProperty("Accept",         "application/json");
    connection.setRequestProperty("User-Agent",     userAgent());
    connection.setRequestProperty("Authorization", "token " + _token);

    return connection;
  }

  public APIResponse makeRequest () throws IOException
  {
    HttpURLConnection connection = initConnection();
    InputStream content = (InputStream)connection.getInputStream();
    BufferedReader br = new BufferedReader(new InputStreamReader(content));    
    String res = null;

    res = br.readLine();
    int code = connection.getResponseCode();
    br.close();
    return new APIResponse(code, res);
  }

  public APIResponse makeRequest (String payload) throws IOException
  {
    HttpURLConnection connection = initConnection();
    connection.setRequestProperty("Content-Length", 
      Integer.toString(payload.length()));

    PrintStream ps = new PrintStream(connection.getOutputStream());
    ps.print(payload);
    ps.close();

    InputStream content = (InputStream)connection.getInputStream();
    BufferedReader br = new BufferedReader(new InputStreamReader(content));    
    String res = null;

    res = br.readLine();
    int code = connection.getResponseCode();
    br.close();
    return new APIResponse(code, res);
  }
}
