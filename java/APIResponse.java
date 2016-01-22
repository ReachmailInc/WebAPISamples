public class APIResponse
{
  private int     _code;
  private String  _response;

  public APIResponse (int code, String response)
  {
    _code     = code;
    _response = response;
  }

  public int code () { return _code; }
  public String response () { return _response; }
}
