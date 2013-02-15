require "fileutils"

module Path

	def Path.EnsurePath(path)
		if !Dir.exists?(path) then 
			FileUtils.mkdir_p(path)
		end
	end

	def Path.DeleteDirectory(path)
		if Dir.exists?(path) then 
			 FileUtils.rm_rf path
		end
	end

	def Path.CopyFiles(source, target) 
		Dir.glob(source) do |name|
			FileUtils.cp(name, target)
		end	
	end
end