<?php

//---- API Service: MailingService\CreateMailing
//---- This API service creates a mail.

//---- Setting some basic variables. If you need help determining your
//---- account key, username or password please contact you ReachMail account
//---- administrator or support@reachmail.com. For information on the API
//---- service URL please refer to the documentation at
//---- services.reachmail.net
$account_key = 'account-id';
$username = 'username';
$password = 'password';
$api_service_url = 'https://services.reachmail.net/REST/Content/Mailings/v1/';

//---- The account id is API specific. For more information on
//---- getting this variable please refer to the code example for
//---- AdministrationService/GetCurrentUser.
$account_id = 'api-account-id';
$api_service_url = $api_service_url.$account_id;

//---- The header variable is used to set the content type of the request and
//---- will be used later in the cURL options
$header = array("Content-Type: application/xml");

//---- Here the request body is set.
$request_body = '<MailingProperties><FromEmail>tsolyan@reachmail.com</FromEmail><FromName>API Template Test #1</FromName><GroupId>aa585421-a1db-46dd-ae1d-9e72014b472b</GroupId><HtmlContent><![CDATA[<html><head><title>Tom Template 1</title></head><body><table width="700px" bgcolor="#f0f0f0" style="border:1px solid black;" align="center"><tbody><tr><td colspan="2"style="text-align:center; border:1px solid black;padding:10px;"><font face="Arial" size="5"><strong>LOGO GOES HERE</font><br><font face="Arial" size="4">Tag Line Goes Here</strong></font></td></tr><tr><td colspan="2"style="text-align:center; border:1px solid black;padding:10px;"><font face="Arial" size="3"><a href="#">www.yourwebsite.com</a></font></td></tr><tr><td bgcolor="#f0f0f0" style="border:1px solid black;padding:10px;"><font face="Arial" size="2" style="margin-left:55px;"><strong>Your Introduction Line Goes Here</strong></font><br><font style="text-align:justify;" face="Arial" size="2"><p>To change the font face, color, or size just highlight the text you want to change and use the tools you see above. If you are using Microsoft Word&nbsp; do not paste directly into this editor. Either use the paste from Word tool above (clipboard with gray "W"), or paste from Word into notePad or textEdit first, then copy and paste from there to this editor. To make a hyperlink just highlight text you want to link and choose the link tool above (globe with chain), then enter your URL in the resulting pop up window.<br><span style="margin-left:100px;"><a href="#">www.link.com</a></span></p><p>Praesent vereor nibh eligo premo occuro olim. Mos vulputate vindico wisi ut persto, vereor mauris quadrum jumentum. Obruo suscipit tation nostrud bene eros ex pneum diam ulciscor duis euismod, ideo aliquip. Diam tum defui, ea et esca consequat olim, demoveo accumsan, iusto nulla ibidem caecus. Amet bene sino quod eu exputo ullamcorper immitto, consequat dolore wisi vulpes. Pecus, validus, nunc vel fere suscipere abdo olim vero comis minim.<br><span style="margin-left:100px;"><a href="#">www.link.com</a></span></p><p>Praesent vereor nibh eligo premo occuro olim. Mos vulputate vindico wisi ut persto, vereor mauris quadrum jumentum. Obruo suscipit tation nostrud bene eros ex pneum diam ulciscor duis euismod, ideo aliquip. Diam tum defui, ea et esca consequat olim, demoveo accumsan, iusto nulla ibidem caecus. Amet bene sino quod eu exputo ullamcorper immitto, consequat dolore wisi vulpes. Pecus, validus, nunc vel fere suscipere abdo olim vero comis minim.<br><span style="margin-left:100px;"><a href="#">www.link.com</a></span></p> </font></td><td cellpadding="10" bgcolor="#f0f0f0" style="border:1px solid black;padding:10px;"><font face="Arial" size="2" style="margin-left:55px;"><strong>Your Introduction Line Goes Here</strong></font><br><font style="text-align:justify;" face="Arial" size="2"><p>Oppeto verto laoreet nisl dignissim ad lucidus, magna, sit sed zelus feugiat, ut feugiat, molior. Luctus in at quis ille euismod dolore secundum dolor quibus sed epulae. Ventosus vel ingenium nobis ille plaga quadrum, commoveo eros in macto sagaciter cui. Facilisis vulpes transverbero, cogo, sagaciter, pala. Ad commodo minim in dolore multo dignissim immitto, probo conventio refoveo rusticus. Quidne huic opto ad volutpat et tation indoles lenis at.<br><span style="margin-left:100px;"><a href="#">www.link.com</a></span></p> <p>Oppeto verto laoreet nisl dignissim ad lucidus, magna, sit sed zelus feugiat, ut feugiat, molior. Luctus in at quis ille euismod dolore secundum dolor quibus sed epulae. Ventosus vel ingenium nobis ille plaga quadrum, commoveo eros in macto sagaciter cui. Facilisis vulpes transverbero, cogo, sagaciter, pala. Ad commodo minim in dolore multo dignissim immitto, probo conventio refoveo rusticus. Quidne huic opto ad volutpat et tation indoles lenis at. <br><span style="margin-left:100px;"><a href="#">www.link.com</a></span></p><p>Oppeto verto laoreet nisl dignissim ad lucidus, magna, sit sed zelus feugiat, ut feugiat, molior. Luctus in at quis ille euismod dolore secundum dolor quibus sed epulae. Ventosus vel ingenium nobis ille plaga quadrum, commoveo eros in macto sagaciter cui. Facilisis vulpes transverbero, cogo, sagaciter, pala. Ad commodo minim in dolore multo dignissim immitto, probo conventio refoveo rusticus. Quidne huic opto ad volutpat et tation indoles lenis at.<br><span style="margin-left:100px;"><a href="#">www.link.com</a></span></p></font></td></tr><tr><td colspan="2"style="text-align:center; border:1px solid black;padding:10px;"><font face="Arial" size="5"><strong>Footer Goes Here</strong></font><br><font face="Arial" size="3"><a href="mailto:#">you@youremail.com</a></font></td></tr></tbody></table></body><!--- tsolyan@reachmail.com 2010 ---></html>]]></HtmlContent><TextContent>This is the Text Content</TextContent><MailingFormat>TextAndHtml</MailingFormat><Name>API Template Test #1</Name><Subject>API Template Test #1</Subject><ReplyToEmail>tsolyan@reachmail.com</ReplyToEmail><TrackedLink><Created>2011-01-24</Created><Modified>2011-01-24</Modified><Url></Url></TrackedLink></MailingProperties>';

$create_mail_request = curl_init();
$curl_options = array(
CURLOPT_URL => $api_service_url,
CURLOPT_HEADER => false,
CURLOPT_USERPWD => "$account_key\\$username:$password",
CURLOPT_HTTPHEADER => $header,
CURLOPT_POST => true,
CURLOPT_POSTFIELDS => $request_body,
CURLOPT_RETURNTRANSFER => true
);

curl_setopt_array($create_mail_request, $curl_options);

$create_mail_response = curl_exec($create_mail_request);

curl_close($create_mail_request);

$xml = simplexml_load_string($create_mail_response);

$mail_id = $xml->Id;
print_r($xml);
//print "\nMail has been successfully created!\nYour mail id: $mail_id\n\n";

?>