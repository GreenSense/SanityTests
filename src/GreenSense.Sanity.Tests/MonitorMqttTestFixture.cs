using System;
using NUnit.Framework;
using System.IO;
using System.Net.NetworkInformation;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace GreenSense.Sanity.Tests
{
	[TestFixture]
	public class MonitorMqttTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_Monitor()
		{
			WriteTestHeading ("Testing MQTT data for live GreenSense monitor project");

			var helper = new DeviceMqttTestHelper("monitor1");
			
			helper.Start();

			var maxInterval = 5;
			
			for (int i = 1; i <= maxInterval; i+=2)
				helper.RunReadIntervalTest(i);

			helper.End();
		}
	}
}

