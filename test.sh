echo "Testing project"
echo "  Dir: $PWD"

#export GARDEN_HOST=garden
#export MOSQUITTO_HOST=garden
#export MOSQUITTO_USERNAME=user
#export MOSQUITTO_PASSWORD=pass

mono lib/NUnit.Runners.2.6.4/tools/nunit-console.exe bin/Release/*.dll
