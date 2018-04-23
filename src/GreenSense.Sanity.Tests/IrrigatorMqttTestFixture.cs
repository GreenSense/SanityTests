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

			var helper = new IrrigatorMqttTestHelper("irrigator1");
			
			helper.Start();

			helper.RunReadIntervalTests(5, 2);
			
			helper.RunPumpStatusTests();
			
			helper.RunVersionTest("SoilMoistureSensorCalibratedPump");

			helper.End();
			
		}

	}
}

