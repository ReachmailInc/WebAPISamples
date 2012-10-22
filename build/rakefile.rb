require 'net/http'
require_relative 'dotnet'

task :build => [:downloadApiSpec, :buildDotNet]

task :downloadApiSpec do
  puts 'Downloading the API spec...'
  Net::HTTP.start('services.reachmail.net') do |http|
      resp = http.get('/documentation/data', { 'accept' => 'application/json'})
      open('apispec.json', 'wb') do |file|
          file.write(resp.body)
      end
  end
  puts 'Download complete.'
end