using System;
using Reachmail.Easysmtp.Post.Request;

public void Main() 
{
    var client = Reachmail.Api.Create("<API Token>");

    var request = new Reachmail.EasySmtp.Post.Message {
        FromAddress = "from@from.com",
        Recipients = new Recipients { 
            new Recipient { 
                Address = "to@to.com"
            }
        },
        Subject = "Subject",
        BodyText = "Text",
        BodyHtml = "html",
        Headers = new Headers { 
            { "name", "value" }
        },
        Attachments = new Attachments { 
            new EmailAttachment { 
                Filename = "text.txt",
                Data = "b2ggaGFp", // Base64 encoded
                ContentType = "text/plain",
                ContentDisposition = "attachment",
                Cid = "<text.txt>"
            }
        },
        Tracking = true,
        FooterAddress = "footer@footer.com",
        SignatureDomain = "signature.net"
    };

    var accountId = client.Administration.Users.Current.Get();
    var result = client.EasySmtp.Post(request, accountId);
}
