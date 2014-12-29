using System;
using Reachmail.Easysmtp.Post.Request;

public void Main() 
{
    var reachmail = Reachmail.Api.Create("<API Token>");

    var request = new DeliveryRequest { 
        FromAddress = "from@from.com",
        Recipients = new Recipients { 
            new Recipient { 
                Address = "to@to.com"
            }
        },
        Subject = "Subject",
        BodyText = "Text",
        BodyHtml = "<p>html</p>",
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

    var result = reachmail.Easysmtp.Post(request);
}