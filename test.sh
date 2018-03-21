echo "Testing project"
echo "  Dir: $PWD"

export GARDEN_HOST=garden
export MOSQUITTO_HOST=garden
export MOSQUITTO_USERNAME=j
export MOSQUITTO_PASSWORD=ywgtpJ8ms!

mono lib/NUnit.Runners.2.6.4/tools/nunit-console.exe bin/Release/*.dll
