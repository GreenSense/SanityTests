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
	public class MonitorMqttTestFixture : BaseTestFixture
	{
		public bool MessageReceived = false;

		public string Topic = "/monitor1/C";

		[Test]
		public void Test_MqttServer()
		{
			Console.WriteLine ("==========");
			Console.WriteLine ("Testing MQTT data for live GreenSense monitor project");
			Console.WriteLine ("==========");

			var host = Environment.GetEnvironmentVariable ("MOSQUITTO_HOST");
			var user = Environment.GetEnvironmentVariable ("MOSQUITTO_USERNAME");
			var pass = Environment.GetEnvironmentVariable ("MOSQUITTO_PASSWORD");

			Assert.IsNotNullOrEmpty (host, "MOSQUITTO_HOST environment variable is not set.");
			Assert.IsNotNullOrEmpty (user, "MOSQUITTO_USERNAME environment variable is not set.");
			Assert.IsNotNullOrEmpty (pass, "MOSQUITTO_PASSWORD environment variable is not set.");

			Console.WriteLine ("Host: " + host);
			Console.WriteLine ("Username: " + user);

			var mqttClient = new MqttClient(host);

			var clientId = Guid.NewGuid ().ToString ();

			mqttClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
			mqttClient.Connect (clientId, user, pass);

			mqttClient.Subscribe(new string[] {Topic}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

			Thread.Sleep (10000);

			Assert.IsTrue (MessageReceived, "No MQTT data was received.");
		}

		public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			var topic = e.Topic;

			if (Topic == topic) {
				var message = System.Text.Encoding.Default.GetString(e.Message);

				Console.WriteLine("Message received: " + message);

				MessageReceived = true;
			}
		}

	}
}

