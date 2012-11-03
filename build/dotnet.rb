require "albacore"
require_relative "path"
require_relative "gallio-task"
require_relative "csharp-wrapper-task"

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

csharpwrapper :generateWrapper => :assemblyInfo do |options|
    options.spec_path = 'apispec.json'
    options.template_path = 'dotnet/Reachmail/Reachmail.Template.cs'
    options.output_path = 'dotnet/Reachmail/Reachmail.cs'
end

msbuild :buildLibrary => :generateWrapper do |msb|
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
packageFilePath = File.join(deployPath, "reachmail.#{version}.nupkg")

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

task :publishLocalPackage => :createPackage do
  Path.EnsurePath('artifacts')
  Path.CopyFiles(packageFilePath, 'artifacts')
end

nugetpush :publishPublicPackage => :createPackage do |nuget|
  nuget.apikey = nugetApiKey
  nuget.package = packageFilePath.gsub('/', '\\')
end