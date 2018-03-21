using System;
using NUnit.Framework;
using System.IO;
using System.Net.NetworkInformation;

namespace GreenSense.Sanity.Tests
{
	[TestFixture]
	public class GardenMqttTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_PingGarden()
		{
			var user = Environment.GetEnvironmentVariable ("MOSQUITO_USERNAME");
			var pass = Environment.GetEnvironmentVariable ("MOSQUITO_PASSWORD");

			Console.WriteLine ("Username: " + user);
			Console.WriteLine ("Password: " + pass);
		}

	}
}

