echo "Starting build for gitter project"
echo "Dir: $PWD"

DIR=$PWD

xbuild src/GreenSense.Sanity.Tests.sln /p:Configuration=Release
