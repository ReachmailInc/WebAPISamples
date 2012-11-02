require "albacore"
require_relative "path"
require_relative "gallio-task"

version = ENV['BUILD_NUMBER']
reportsPath = 'reports'

task :buildDotNet => :publishLocalPackage
task :deployDotNet => :publishPublicPackage

assemblyinfo :assemblyInfo do |asm|
    asm.version = version
    asm.company_name = "Reachmail Inc."
    asm.product_name = "Reachmail API Wrapper"
    asm.title = "Reachmail API Wrapper"
    asm.description = "Wrapper for the Reachmail API."
    asm.copyright = "Copyright (c) #{Time.now.year} Reachmail Inc."
    asm.output_file = "dotnet/Reachmail/Properties/AssemblyInfo.cs"
end

msbuild :buildLibrary => :assemblyInfo do |msb|
    msb.properties :configuration => :Release
    msb.targets :Clean, :Build
    msb.solution = "dotnet/Reachmail/Reachmail.csproj"
end

msbuild :buildTests => :buildLibrary do |msb|
    msb.properties :configuration => :Release
    msb.targets :Clean, :Build
    msb.solution = "dotnet/Tests/Tests.csproj"
end

task :unitTestInit do
	Path.EnsurePath(reportsPath)
end

gallio :unitTests => [:buildTests, :unitTestInit] do |runner|
	runner.echo_command_line = true
	runner.add_test_assembly("dotnet/Tests/bin/Release/Tests.dll")
	runner.verbosity = 'Normal'
	runner.report_directory = reportsPath
	runner.report_name_format = 'tests'
	runner.add_report_type('Html')
end

nugetApiKey = ENV["NUGET_API_KEY"]
deployPath = "dotnet/deploy"

packagePath = File.join(deployPath, "package")
nuspecName = "reachmail.nuspec"
packageLibPath = File.join(packagePath, "lib")
binPath = "dotnet/Reachmail/bin/Release"
packageFilename = File.join(deployPath, "reachmail.#{version}.nupkg").gsub('/', '\\')

task :prepPackage => :unitTests do
  Path.DeleteDirectory(deployPath)
  
  Path.EnsurePath(packageLibPath)
  Path.CopyFiles(File.join(binPath, "Reachmail.dll"), packageLibPath)
  Path.CopyFiles(File.join(binPath, "Reachmail.pdb"), packageLibPath)
end

nuspec :createSpec => :prepPackage do |nuspec|
   nuspec.id = "reachmail"
   nuspec.version = version
   nuspec.authors = "Reachmail, Inc."
   nuspec.owners = "Reachmail, Inc."
   nuspec.title = "Reachmail API Wrapper"
   nuspec.description = "This library allows you to easily interact with the Reachmail API."
   nuspec.summary = "This library allows you to easily interact with the Reachmail API."
   nuspec.language = "en-US"
   nuspec.licenseUrl = "https://raw.github.com/ReachmailInc/WebAPISamples/master/LICENSE"
   nuspec.projectUrl = "https://github.com/ReachmailInc/WebAPISamples/tree/master/dotnet"
   nuspec.iconUrl = "https://raw.github.com/ReachmailInc/WebAPISamples/master/dotnet/logo.png"
   nuspec.working_directory = packagePath
   nuspec.output_file = nuspecName
   nuspec.tags = "reachmail email esp"
end

nugetpack :createPackage => :createSpec do |nugetpack|
   nugetpack.nuspec = File.join(packagePath, nuspecName)
   nugetpack.base_folder = packagePath
   nugetpack.output = deployPath
end

task :publishLocalPackage => :unitTests do
  Path.CopyFiles(packageFilename, 'artifacts')
end

nugetpush :publishPublicPackage => :createPackage do |nuget|
  nuget.apikey = nugetApiKey
  nuget.package = packageFilename
end