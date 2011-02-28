require 'net/http'
require 'net/https'
require 'rexml/document'

http = Net::HTTP.new('services.reachmail.net',443)
req = Net::HTTP::Get.new('/Rest/Administration/v1/users/current')
http.use_ssl = true
http.verify_mode = OpenSSL::SSL::VERIFY_NONE
req.basic_auth 'ACCOUNT_ID\username', 'password'
response = http.request(req)
doc = REXML::Document.new(response.body)
root = doc.root
puts root.elements['AccountId'].text
