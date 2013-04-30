require 'json'

def csharpwrapper(*args, &block)
    task = lambda { |*args|
        task = CSharpWrapper.new
        block.call(task)
        task.run
    }
    Rake::Task.define_task(*args, &task)
end

String.class_eval do
  def initial_cap
    self[0].upcase + self[1..self.length - 1]
  end
end

class CSharpWrapper

    attr_accessor :spec_path, :template_path, :output_path

    def run()
                
        spec = JSON.load(File.read('apispec.json'))
        output = Hash.new

        modules = get_modules(spec)
        output['url'] = "https://#{ENV['API_HOST']}"
        output['rootModules'] = modules.select{|x| !x['namespace'].include?('.')}.map{|x| x['namespace']}
        output['modules'] = modules
        output['types'] = get_endpoint_types(spec)

        wrapper_path = 'csharp-wrapper.json'
        File.write(wrapper_path, JSON.pretty_generate(output))

        sh "nustache.exe #{@template_path} #{wrapper_path} #{@output_path}"

    end

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
        when 'duration' then 'TimeSpan' + (required ? '' : '?')
        when 'guid' then 'Guid' + (required ? '' : '?')
        when 'char' then 'Char' + (required ? '' : '?')
        when 'base64Binary' then 'byte[]'
        else 
			if type.start_with?('ArrayOf') 
				"List<#{get_data_type(type.gsub(/ArrayOf/, ''))}>"
			else
				type + (required ? '' : '?')
			end
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

    def add_modules(modules, endpoint)
        namespaces = get_namespaces(endpoint['Url'])
        namespaces
            .slice(0, namespaces.length)
            .select{|x| !modules.has_key?(x)}
            .each{|x| modules[x] = Hash['namespace' => x, 'endpoints' => Array.new, 'modules' => Array.new]}
        if namespaces.length > 1 
            (0..namespaces.length - 2)
                .each{|x| 
                    moduleDef = modules[namespaces[x]]
                    subModule = namespaces[x + 1].split(/\./).last
                    if !moduleDef['modules'].include?(subModule) then moduleDef['modules'].push(subModule) end
                }
        end
        modules[namespaces.last]
    end

    def get_endpoint_data_type(spec, endpoint, direction)
        if endpoint[direction] != nil
            namespace = endpoint['Method'].capitalize + '.' + direction + '.'
            if endpoint[direction]['Collection'] 
                modelType = spec['Types'].select{|x| x['Id'] == endpoint[direction]['Type']}[0]
                return "List<#{namespace}#{modelType != nil ? modelType['Name'] : get_data_type(endpoint[direction]['Type'], true)}>" 
            else 
                return endpoint[direction]['Type'] == 'stream' ? 'Stream' : (namespace + endpoint[direction]['Name'])
            end
        end    
    end

    def get_modules(spec)
        modules = Hash.new
    	spec['Modules'].map{|x| x['Resources'].map {|y|  y['Endpoints']}}.flatten().each {|sourceEndpoint|

            currentModule = add_modules(modules, sourceEndpoint)
            
            urlParameters = sourceEndpoint['UrlParameters'] != nil ? sourceEndpoint['UrlParameters'] : []
            querystringParameters = sourceEndpoint['QuerystringParameters'] != nil ? sourceEndpoint['QuerystringParameters'] : []

            endpoint = Hash.new
    		endpoint['method'] = sourceEndpoint['Method'].capitalize
            endpoint['comments'] = sourceEndpoint['Comments']
            endpoint['url'] = sourceEndpoint['Url'].split(/\?/)[0]
            endpoint['urlArguments'] = urlParameters.map{|x| "{\"#{x['Name'][0, 1].downcase + x['Name'][1..-1]}\", #{x['Name'][0, 1].downcase + x['Name'][1..-1]}}"}.join(', ')
            endpoint['querystringArguments'] = querystringParameters.map{|x| "{\"#{x['Name'][0, 1].downcase + x['Name'][1..-1]}\", #{x['Name'][0, 1].downcase + x['Name'][1..-1]}}"}.join(', ')

            requestType = get_endpoint_data_type(spec, sourceEndpoint, 'Request')
            endpoint['requestArgument'] = sourceEndpoint['Request'] != nil ? 'request' : 'null'

            parameterComments = Array.new
            parameterComments.concat(urlParameters.map{|x| Hash['name' => x['Name'][0, 1].downcase + x['Name'][1..-1], 'comments' => x['Comments']]})
            if sourceEndpoint['Request'] != nil then parameterComments.push Hash['name' => 'request', 'comments' => sourceEndpoint['Request']['comments']] end
            parameterComments.concat(querystringParameters.map{|x| Hash['name' => x['Name'][0, 1].downcase + x['Name'][1..-1], 'comments' => x['Comments']]})
            endpoint['parameterComments'] = parameterComments

            parameters = Array.new
            parameters.concat(urlParameters.select{|x| x['Name'].downcase != 'accountid'}.map{|x| get_data_type(x['Type'], true) + ' ' + x['Name'][0, 1].downcase + x['Name'][1..-1]})
            if sourceEndpoint['Request'] != nil then parameters.concat([requestType + ' request']) end
            parameters.concat(querystringParameters.map{|x| get_data_type(x['Type'], x['Required']) + ' ' + x['Name'][0, 1].downcase + x['Name'][1..-1] + (x['Required'] ? '' : ' = null')})
            parameters.concat(urlParameters.select{|x| x['Name'].downcase == 'accountid'}.map{|x| get_data_type(x['Type'], false) + ' ' + x['Name'][0, 1].downcase + x['Name'][1..-1] + ' = null'})
            endpoint['parameters'] = parameters.join(', ')
            
            responseType = get_endpoint_data_type(spec, sourceEndpoint, 'Response')
            if sourceEndpoint['Response'] != nil then endpoint['responseComments'] = sourceEndpoint['Response']['Comments'] end
            endpoint['responseArgument'] = sourceEndpoint['Response'] == nil ? 'null' : "typeof(#{responseType})"
            endpoint['returnType'] = sourceEndpoint['Response'] == nil ? 'void' : responseType
            endpoint['returnKeyword'] = sourceEndpoint['Response'] == nil ? '' : "return (#{endpoint["returnType"]})"

    		currentModule['endpoints'].push(endpoint)
    	}
    	modules.values
    end

    def get_endpoint_types(spec)
        types = Hash.new
        spec['Modules'].map{|x| x['Resources'].map {|y| y['Endpoints']}}.flatten().each{|endpoint|
            namespace = get_namespaces(endpoint['Url']).last + '.' + endpoint['Method'].capitalize + '.'
            if endpoint['Request'] != nil then get_types(spec, types, endpoint['Request']['Type'], namespace + 'Request') end
            if endpoint['Response'] != nil then get_types(spec, types, endpoint['Response']['Type'], namespace + 'Response') end
        }
        types.values
    end

    def get_member_data_type(spec, member)
        modelType = spec['Types'].select{|x| x['Id'] == member['Type']}[0]
        if member['Collection'] 
            return "List<#{modelType != nil ? modelType['Name'] : get_data_type(member['Type'], true)}>" 
        else 
            return modelType != nil ? modelType['Name'] : get_data_type(member['Type'], member['Required'])
        end
    end

    def get_types(spec, types, typeId, namespace)
        modelType = spec['Types'].select{|x| x['Id'] == typeId}[0]
        if modelType != nil
            fullname = namespace + '.' + modelType['Name']
            if types.has_key?(fullname) then return end
            types[fullname] = type = Hash['namespace' => namespace, 'name' => modelType['Name'], 'comments' => modelType['Comments'], 'members' => Array.new, 'enums' => Array.new]
            modelType['Members'].each{|member|
                if member['Options'] != nil && member['Options'].length > 0
                    typeName = member['Name'] + 'Options'
                    type['enums'].push Hash['name' => typeName, 'values' => member['Options'].map {|option| Hash['value' => option['Value'], 'comments' => option['Comments']] }]
					typeName = typeName + '?' unless member['Required']
                else
                    typeName = get_member_data_type(spec, member)
                    get_types(spec, types, member['Type'], namespace)
                end
                type['members'].push Hash['type' => typeName, 'name' => member['Name'], 'comments' => member['Comments']]
            }
        end
    end

end