require 'pathname'

def nunit(*args, &block)
	task = lambda { |*args|
		task = NUnit.new
		block.call(task)
		task.run
	}
	Rake::Task.define_task(*args, &task)
end

class NUnit

    attr_accessor :test_assembly_path, :namespace, :output_path, :filename
    
    def output(path, filename)
        if File.extname(filename) == '' then filename += '.xml' end
        @filename = filename
        @output_path = path
    end

    def run()
        
        command = []
		
		command << "\"#{to_windows_path(probe_nunit_path(@test_assembly_path))}\""
        if @namespace != nil then command << "/fixture:#{@namespace}" end
        if @filename != nil then command << "/result:\"#{@filename}\"" end
        if @output_path != nil then command << "/work:\"#{to_windows_path(@output_path)}\"" end
        command << "/labels"
        command << "/nodots"
        command << "\"#{to_windows_path(@test_assembly_path)}\""
        
		command = command.join(" ")

        puts command
        result = `#{command} 2>&1`
        puts result unless result.empty? 
        if @output_path != nil && @filename != nil then 
            puts "##teamcity[importData type='nunit' path='#{File.join(@output_path, @filename)}']"
        end
        fail "NUnit task failed: #{$?}." unless $? == 0
    end
	
	private
	
	def to_windows_path(path)
		path.gsub("/", "\\")
	end

    def probe_nunit_path(path)
        path = File.dirname(path)

        while !File.directory?((path = File.expand_path(path + '/..')) + '/packages') && !Pathname.new(path).root? do
        end
        results = Dir.glob(path + '/packages/NUnit.Runners.*/tools/nunit-console-x86.exe')

        if (results.count > 0) then return results[0]
        else throw 'Could not find nunit-console.exe.' end
    end
    
end

    