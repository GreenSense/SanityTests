echo "Starting build for project"
echo "Dir: $PWD"

DIR=$PWD

msbuild src/GreenSense.Sanity.Tests.sln /p:Configuration=Release
