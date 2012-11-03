cd ..
rm artifacts -r -f
rm reports -r -f
rm *.json
rm dotnet/deploy -r -f
git checkout HEAD dotnet/Reachmail/Properties/AssemblyInfo.cs
git checkout HEAD dotnet/Reachmail/Reachmail.cs