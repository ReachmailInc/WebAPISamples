import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.PrintStream;
import java.net.HttpURLConnection;
import java.net.URL;
import org.apache.commons.codec.binary.Base64;

/*
 * The following is a Java example of a basic Post request to the Reachmail API.  This code uses the HttpURLConnection library and the apache
 * base64 encryption tool amoung others.  This application runs the ContactService_CreateList function.
*/

public class Rest {

	public static void main(String[] args) throws IOException {
		// set the username and password single \ should be a \\ in java.  ex: accountID\\user
		String user = "ACCOUNTID\\USER";
		String pass = "PASSWORD";
		
		//set URL
		URL url = new URL("https://services.reachmail.net/Rest/Contacts/v1/lists/9D307247-13D3-E011-9A62-001EC9B929F4");

		//open URL connection
		HttpURLConnection connection = (HttpURLConnection)url.openConnection();
	    
		// set REST method
		connection.setRequestMethod("POST");
	    connection.setDoOutput(true);
	    connection.setAllowUserInteraction(false);
		
	    //set content type
	    connection.setRequestProperty("Content-Type", "application/xml");
		
		//prep credentials into a single string
		String userpass = user + ":" + pass;
		
		//encode credentials in base64
		String encodedPass = new String(Base64.encodeBase64(userpass.getBytes()));
		
		//set authorization using encoded credentials
		connection.setRequestProperty("Authorization", "Basic " + encodedPass);

	    	    
	    //set parameter.  This is your query to the URL above
		String query = "<ListProperties><Fields><Field>email</Field><Field>FullName</Field></Fields><Name>MikeList2</Name></ListProperties>";

		//send query 
		PrintStream ps = new PrintStream(connection.getOutputStream());
		ps.print(query);
		ps.close();

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
 
