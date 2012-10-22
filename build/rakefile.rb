reportsPath = "reports"
version = ENV["BUILD_NUMBER"]

task :build => [:downloadApiSpec]#[:buildDotNet]

task :downloadApiSpec do
  puts 'oh hai'
end