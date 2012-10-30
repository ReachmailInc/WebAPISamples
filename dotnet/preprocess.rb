require 'json'

String.class_eval do
  def initial_cap
    self[0].upcase + self[1..self.length - 1]
  end
end

def get_namespaces(url)
	urlParts = url.split(/\?/)[0]
	    .split(/\//)
	    .delete_if {|x| x.downcase == 'v1' || x.downcase == 'rest' || x == '' || x.downcase == '{accountid}'}
	    .map{|x| (x.start_with?('{') ? 'By' : '') + x.gsub(/({|})/, '').initial_cap()}
	(0..urlParts.length)
		.map {|x| urlParts.slice(0, x).join('.').gsub(/\A\.|\.\Z/, '') }
	    .delete_if {|x| x == ''}
end

spec = JSON.load(File.read('apispec.json'))

def get_data_type(type, required)
    return case type
    when 'string' then 'String'
    when 'boolean' then 'Boolean' + (required ? '' : '?')
    when 'decimal' then 'Decimal' + (required ? '' : '?')
    when 'double' then 'Double' + (required ? '' : '?')
    when 'float' then 'Single' + (required ? '' : '?')
    when 'unsignedByte' then 'Byte' + (required ? '' : '?')
    when 'byte' then 'SByte' + (required ? '' : '?')
    when 'short' then 'Int16' + (required ? '' : '?')
    when 'unsignedShort' then 'UInt16' + (required ? '' : '?')
    when 'int' then 'Int32' + (required ? '' : '?')
    when 'unsignedInt' then 'UInt32' + (required ? '' : '?')
    when 'long' then 'Int64' + (required ? '' : '?')
    when 'unsignedLong' then 'UInt64' + (required ? '' : '?')
    when 'dateTime' then 'DateTime' + (required ? '' : '?')
    when 'guid' then 'Guid' + (required ? '' : '?')
    when 'char' then 'Char' + (required ? '' : '?')
    when 'base64Binary' then 'byte[]'
    else type.start_with?("ArrayOf") ? "List<#{get_data_type(type.gsub(/ArrayOf/, ''))}>" : 'object'
    end
end

def get_modules(spec)
	modules = Hash.new
	spec['Modules'].map{|x| x['Resources'].map {|y|  y['Endpoints']}}.flatten().each {|endpoint|
		endpoint["Method"] = endpoint["Method"].capitalize
        endpoint["Url"] = endpoint["Url"].split(/\?/)[0]
        parameters = Array.new
        parameters.concat(endpoint['UrlParameters'].select{|x| x['Name'].downcase != 'accountid'}.map{|x| get_data_type(x['Type'], true) + ' ' + x['Name']})
        if endpoint["Request"] != nil then parameters.concat([endpoint["Method"] + '.Request request']) end
        parameters.concat(endpoint['QuerystringParameters'].map{|x| get_data_type(x['Type'], x['Required']) + ' ' + x['Name'] + (x['Required'] ? '' : ' = null')})
        endpoint["parameters"] = parameters.join(', ')
        endpoint['urlArguments'] = endpoint['UrlParameters'].select{|x| x['Name'].downcase != 'accountid'}.map{|x| "{\"#{x['Name']}\", #{x['Name']}}"}.join(', ')
        endpoint["querystringArguments"] = endpoint['QuerystringParameters'].map{|x| "{\"#{x['Name']}\", #{x['Name']}}"}.join(', ')
        endpoint["returnType"] = endpoint["Response"] == nil ? 'void' : (endpoint["Method"] + '.Response')
        endpoint["returnKeyword"] = endpoint["Response"] == nil ? '' : "return (#{endpoint["returnType"]})"
		namespaces = get_namespaces(endpoint['Url'])
		namespaces
			.slice(0, namespaces.length)
			.select{|x| !modules.has_key?(x)}
			.each{|x| modules[x] = Hash["namespace" => x, "endpoints" => Array.new, "modules" => Array.new]}
		if namespaces.length > 1 
			(0..namespaces.length - 2)
				.each{|x| 
					moduleDef = modules[namespaces[x]]
					subModule = namespaces[x + 1].split(/\./).last
					if !moduleDef['modules'].include?(subModule) then moduleDef['modules'].push(subModule) end
				}
		end
		modules[namespaces.last]["endpoints"].push(endpoint)
	}
	Hash["rootModules" => modules.values.select{|x| !x['namespace'].include?('.')}.map{|x| x['namespace']}, 
	     "modules" => modules.values]
end

File.write('apiwrapper.json', JSON.pretty_generate(get_modules(spec)))