echo "Testing project"
echo "  Dir: $PWD"

mono lib/NUnit.Runners.2.6.4/tools/nunit-console.exe bin/Release/*.dll
