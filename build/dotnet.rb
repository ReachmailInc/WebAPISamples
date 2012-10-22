require "albacore"
require_relative "path"
require_relative "gallio-task"

task :buildDotNet => :createPackage

assemblyinfo :assemblyInfo do |asm|
    asm.version = version
    asm.company_name = "Ultraviolet Catastrophe"
    asm.product_name = "FubuMVC Swank"
    asm.title = "FubuMVC Swank"
    asm.description = "FubuMVC Swank."
    asm.copyright = "Copyright (c) #{Time.now.year} Ultraviolet Catastrophe"
    asm.output_file = "src/Swank/Properties/AssemblyInfo.cs"
end

msbuild :buildLibrary => :assemblyInfo do |msb|
    msb.properties :configuration => :Release
    msb.targets :Clean, :Build
    msb.solution = "src/Swank/Swank.csproj"
end

msbuild :buildTestHarness => :buildLibrary do |msb|
    msb.properties :configuration => :Release
    msb.targets :Clean, :Build
    msb.solution = "src/TestHarness/TestHarness.csproj"
end

msbuild :buildTests => :buildTestHarness do |msb|
    msb.properties :configuration => :Release
    msb.targets :Clean, :Build
    msb.solution = "src/Tests/Tests.csproj"
end

task :unitTestInit do
	Path.EnsurePath(reportsPath)
end

gallio :unitTests => [:buildTests, :unitTestInit] do |runner|
	runner.echo_command_line = true
	runner.add_test_assembly("src/Tests/bin/Release/Tests.dll")
	runner.verbosity = 'Normal'
	runner.report_directory = reportsPath
	runner.report_name_format = 'tests'
	runner.add_report_type('Html')
end

nugetApiKey = ENV["NUGET_API_KEY"]
deployPath = "deploy"

packagePath = File.join(deployPath, "package")
nuspecFilename = "FubuMVC.Swank.nuspec"
packageContentPath = File.join(packagePath, "content/fubu-content")

task :prepPackage => :unitTests do
	Path.DeleteDirectory(deployPath)
	Path.EnsurePath(packageContentPath)
    packageLibPath = File.join(packagePath, "lib")
	Path.EnsurePath(packageLibPath)
	Path.CopyFiles("src/Swank/bin/FubuMVC.Swank.*", packageLibPath)
end

create_fubu_bottle :createBottle => :prepPackage do |bottle|
    bottle.package_type = :zip
    bottle.source_path = 'src/Swank'
    bottle.output_path = File.join(packageContentPath, 'fubu-swank.zip')
    bottle.include_pdb = true
    bottle.overwrite = true
end

nuspec :createSpec => :createBottle do |nuspec|
   nuspec.id = "FubuMVC.Swank"
   nuspec.version = version
   nuspec.authors = "Mike O'Brien"
   nuspec.owners = "Mike O'Brien"
   nuspec.title = "FubuMVC Swank"
   nuspec.description = "A FubuMVC plugin that allows you to describe and publish documentation for RESTful services."
   nuspec.summary = "A FubuMVC plugin that allows you to describe and publish documentation for RESTful services."
   nuspec.language = "en-US"
   nuspec.licenseUrl = "https://github.com/mikeobrien/FubuMVC.Swank/blob/master/LICENSE"
   nuspec.projectUrl = "https://github.com/mikeobrien/FubuMVC.Swank"
   nuspec.iconUrl = "https://github.com/mikeobrien/FubuMVC.Swank/raw/master/misc/logo.png"
   nuspec.working_directory = packagePath
   nuspec.output_file = nuspecFilename
   nuspec.tags = "fubumvc"
   nuspec.dependency "FubuMVC.References", "0.9.0.0"
   nuspec.dependency "FubuMVC.Spark", "0.9.0.0"
   nuspec.dependency "MarkdownSharp", "1.0.0.0"
end

nugetpack :createPackage => :createSpec do |nugetpack|
   nugetpack.nuspec = File.join(packagePath, nuspecFilename)
   nugetpack.base_folder = packagePath
   nugetpack.output = deployPath
end

nugetpush :pushPackage => :createPackage do |nuget|
    nuget.apikey = nugetApiKey
    nuget.package = File.join(deployPath, "FubuMVC.Swank.#{version}.nupkg").gsub('/', '\\')
end