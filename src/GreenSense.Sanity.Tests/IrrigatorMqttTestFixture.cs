using System;
using NUnit.Framework;
using System.IO;
using System.Net.NetworkInformation;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using System.Threading;

namespace GreenSense.Sanity.Tests
{
	[TestFixture]
	public class IrrigatorMqttTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_Irrigator()
		{
			WriteTestHeading ("Testing MQTT data for live GreenSense irrigator project");

			var helper = new DeviceMqttTestHelper("irrigator1");
			
			helper.Start();

			var maxInterval = 5;
			
			for (int i = 1; i <= maxInterval; i+=2)
				helper.RunReadIntervalTest(i);

			helper.End();
			
		}

	}
}

