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
		public bool MessageReceived = false;

		public string Topic = "/irrigator1/C";

		[Test]
		public void Test_Irrigator()
		{
			WriteTestHeading ("Testing MQTT data for live GreenSense irrigator project");

			var helper = new DeviceMqttTestHelper("irrigator1");
			
			helper.Start();

			helper.RunReadIntervalTest(2, 5);
			
			helper.RunReadIntervalTest(3, 7);

			helper.End();
			
		}

	}
}

