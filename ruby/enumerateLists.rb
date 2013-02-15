require 'net/http'
require 'net/https'
require 'rexml/document'

http = Net::HTTP.new('services.reachmail.net',443)
accountIdRequest = Net::HTTP::Get.new('/Rest/Administration/v1/users/current')
http.use_ssl = true
http.verify_mode = OpenSSL::SSL::VERIFY_NONE
accountIdRequest.basic_auth 'account-id\username', 'password'
serviceResponse = http.request(accountIdRequest)
doc = REXML::Document.new(serviceResponse.body)
root = doc.root
accountId = root.elements['AccountId'].text

listRequest = Net::HTTP::Post.new('/Rest/Contacts/v1/lists/query/'+accountId)
listRequest.basic_auth 'account-id\username', 'password'
listRequest["Content-Type"] = "text/xml"
filterXML = '<ListFilter></ListFilter>'
serviceResponse = http.request(listRequest, filterXML)

puts serviceResponse.body
