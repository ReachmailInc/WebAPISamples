# ruby_example - Simple examples of Reachmail API function calls
require 'net/http'
require 'net/https'
require 'rexml/document'

credentials = [ARGV[0], ARGV[1], ARGV[2]]

service_map = {
	'GetCurrentUser' => '/Rest/Administration/v1/users/current',
	'EnumerateLists' => '/Rest/Contacts/v1/lists/query/'
}

def make_request(service_url, credentials, method, req_body=false)
	api_user = credentials[0] + "\\" + credentials[1]
	passwd = credentials[2]
	http = Net::HTTP.new('services.reachmail.net', 443)
	http.use_ssl = true
	http.verify_mode = OpenSSL::SSL::VERIFY_NONE	
	if method == 'GET'
		req = Net::HTTP::Get.new(service_url)
		req.basic_auth api_user, passwd
		service_response = http.request(req)
	elsif method == 'POST'
		req = Net::HTTP::Post.new(service_url)
		req.basic_auth api_user, passwd
		req.content_type = "texl/xml"
		req.body = req_body
		service_response = http.start { |http| http.request req }
	end
	return service_response
end

def get_current_user(service_url, credentials)
	response = make_request(service_url, credentials, 'GET')
	doc = REXML::Document.new(response.body)
	root = doc.root
	account_id = root.elements['AccountId'].text
	return account_id
end

def enumerate_lists(service_url, credentials, account_id)
	req_body = "<ListFilter></ListFilter>"
	response = make_request(service_url+account_id, credentials, 'POST', 
		req_body)
	return response
end

def run(service_map, credentials)
	account_id = get_current_user(service_map['GetCurrentUser'],
		credentials)
	puts "AccountId: #{account_id}"
	puts enumerate_lists(service_map['EnumerateLists'], credentials,
		account_id).body
end

run(service_map, credentials)
