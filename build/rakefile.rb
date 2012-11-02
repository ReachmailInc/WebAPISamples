require 'net/http'
require_relative 'dotnet'

task :build => [:downloadApiSpec, :buildDotNet]
task :deploy => [:downloadApiSpec, :deployDotNet]

task :downloadApiSpec do
  puts "Downloading the API spec from #{ENV['API_HOST']}..."
  Net::HTTP.start(ENV['API_HOST']) do |http|
      resp = http.get('/documentation/data', { 'accept' => 'application/json'})
      open('apispec.json', 'wb') do |file|
          file.write(resp.body)
      end
  end
  puts 'Download complete.'
end