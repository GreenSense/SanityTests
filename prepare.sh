echo "Preparing for project"
echo "Dir: $PWD"

export DEBIAN_FRONTEND=noninteractive

VERSION_NAME=$(lsb_release -cs)

sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb http://download.mono-project.com/repo/ubuntu stable-$VERSION_NAME main" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list

sudo apt-get update -qq && \
sudo apt-get install -y --no-install-recommends git wget mono-complete msbuild ca-certificates-mono
