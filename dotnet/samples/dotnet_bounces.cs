using System;

public void Main() 
{
    var reachmail = Reachmail.Api.Connect("<API Token>");

    var enddate = new DateTime(1985, 10, 26, 1, 20, 0);
    var startdate = new DateTime(1985, 10, 26, 1, 20, 0);
    var result = reachmail.Reports.Easysmtp.Bounces.Get(enddate, startdate);
}