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
	public class GardenMqttTestFixture : BaseTestFixture
	{
		public bool MessageReceived = false;

		[Test]
		public void Test_MqttServer()
		{
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

			var subscribeTopic = "/SanityTest/Key";
			mqttClient.Subscribe(new string[] {subscribeTopic}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

			mqttClient.Publish (subscribeTopic, Encoding.UTF8.GetBytes ("TestValue"));

			Thread.Sleep (1000);


			Assert.IsTrue (MessageReceived);
		}

		public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			var topic = e.Topic;

			var message = System.Text.Encoding.Default.GetString(e.Message);

			Console.WriteLine("Message received: " + message);

			Assert.AreEqual ("TestValue", message);

			MessageReceived = true;
		}

	}
}

