import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import org.apache.commons.codec.binary.Base64;


/*
 * The following is a Java example of a basic Get request to the Reachmail API.  This code uses the HttpURLConnection library and the apache
 * base64 encryption tool amoung others.  This application returns the AministrationService_GetCurrentUser information
*/


public class RestGet {

	public static void main(String[] args) throws IOException {
		// set the username and password single \ should be a \\ in java.  ex: accountID\\user
		String user = "ACCOUNTID\\USER";
		String pass = "PASSWORD";
		
		//set URL
		URL url = new URL("https://services.reachmail.net/Rest/Administration/v1/users/current");

		// open the HTTP URL Connection
		HttpURLConnection connection = (HttpURLConnection)url.openConnection();
		
		//set REST method
	    connection.setRequestMethod("GET");
	    connection.setDoOutput(false);
	    connection.setAllowUserInteraction(false);
	    
	    //set content type
		connection.setRequestProperty("Content-Type", "application/xml");
		
		// prep the credentials
		String userpass = user + ":" + pass;
		
		//encode the credentials in base 64
		String encodedPass = new String(Base64.encodeBase64(userpass.getBytes()));
		
		//pass basic authorization
		connection.setRequestProperty("Authorization", "Basic " + encodedPass);
   

		//get result
		InputStream content = (InputStream)connection.getInputStream();
		BufferedReader br = new BufferedReader(new InputStreamReader(content));
		String l = null;
		while ((l=br.readLine())!=null) {
			System.out.println(l);
		}
		br.close();
	}
}
 
