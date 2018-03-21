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
			var token = Environment.GetEnvironmentVariable ("TESTTOKEN");

			Console.WriteLine (token);
		}

	}
}

