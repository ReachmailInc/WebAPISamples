import java.io.IOException;
public class SampleApplication 
{
  public static void main (String[] args) throws IOException 
  {
    ReachMailAPI api = new ReachMailAPI(args[0]);
    APIResponse resp = api.administrationUsersCurrent();
    System.out.println(resp.code());
    System.out.println(resp.response());
  }
}
