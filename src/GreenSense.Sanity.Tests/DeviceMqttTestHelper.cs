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
	public class DeviceMqttTestHelper
	{
	    public string DeviceName;
	    
		public List<Dictionary<string, string>> Data = new List<Dictionary<string, string>>();
		public Dictionary<string, string> DataEntry = new Dictionary<string, string>();
		
		public MqttClient Client;

	    public DeviceMqttTestHelper(string deviceName)
	    {
	        DeviceName = deviceName;
	    }
	    
	    public void Start()
	    {
			var host = Environment.GetEnvironmentVariable ("MOSQUITTO_HOST");
			var user = Environment.GetEnvironmentVariable ("MOSQUITTO_USERNAME");
			var pass = Environment.GetEnvironmentVariable ("MOSQUITTO_PASSWORD");

			Assert.IsNotNullOrEmpty (host, "MOSQUITTO_HOST environment variable is not set.");
			Assert.IsNotNullOrEmpty (user, "MOSQUITTO_USERNAME environment variable is not set.");
			Assert.IsNotNullOrEmpty (pass, "MOSQUITTO_PASSWORD environment variable is not set.");

			Console.WriteLine ("Host: " + host);
			Console.WriteLine ("Username: " + user);
			
			Client = new MqttClient(host);

			var clientId = Guid.NewGuid ().ToString ();

			Client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
			Client.Connect (clientId, user, pass);

			Client.Subscribe(new string[] {"/" + DeviceName + "/#"}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
	    }
	    
	    public void End()
	    {
			PublishSuccess();
			
			Client.Disconnect();
	    }
	    
	    public void RunReadIntervalTests(int maxInterval, int step)
	    {
			try
			{
				for (int i = 1; i <= maxInterval; i+=step)
					RunReadIntervalTest(i);
			}
			catch (Exception ex)
			{
				PublishError(ex.Message);
				
				throw ex;
			}
	    }
	    
		public void RunReadIntervalTest(int interval)
		{
			int sleepSeconds = interval
				* 2 // 2 entries are needed to compare
				+ 1; // 1 second margin of error
			
			Console.WriteLine("");
			Console.WriteLine("Running read interval test...");
			Console.WriteLine("Interval: " + interval);
			Console.WriteLine("Sleep secconds: " + sleepSeconds);
			
			Console.WriteLine("");
			Console.WriteLine("Setting read interval to " + interval);

			ResetData();
			
			SendCommand("V", interval);

			Console.WriteLine("Sleeping for " + sleepSeconds + " seconds.");

			Thread.Sleep (sleepSeconds * 1000);

			Console.WriteLine("");
			Console.WriteLine("Checking entry times");
			
			CheckDataEntryTimes(interval);
			
			Console.WriteLine("");
			Console.WriteLine("Checking read interval value");
			
			Assert.AreEqual(interval.ToString(), Data[Data.Count-1]["V"], "Invalid read interval value");
		}
		
		public void CheckDataEntryTimes(int expectedInterval)
		{
			Assert.IsTrue(Data.Count >= 2, "More data entries are needed");
			
			var secondLastTime = DateTime.Parse(Data[Data.Count-2]["Time"]);
			var lastTime = DateTime.Parse(Data[Data.Count-1]["Time"]);
			
			Console.WriteLine(secondLastTime.ToString());
			Console.WriteLine(lastTime.ToString());
			
			var timeSpan = lastTime.Subtract(secondLastTime);
		
			Console.WriteLine("Time difference (seconds): " + timeSpan.TotalSeconds);
			
			Assert.AreEqual(expectedInterval, timeSpan.TotalSeconds, "Invalid time difference");
		}
		
		public void SendCommand(string key, int value)
		{
			Console.WriteLine("");
			Console.WriteLine("Sending command...");
			Console.WriteLine("Key: " + key);
			Console.WriteLine("Value: " + value);
			var inTopic = "/" + DeviceName + "/" + key + "/in";
			
			Console.WriteLine("Topic: " + inTopic);
			Client.Publish (inTopic, Encoding.UTF8.GetBytes (value.ToString()));
			Console.WriteLine("");
		}
		
		public void PublishSuccess()
		{
			ClearErrorMessage();
			PublishStatus(0);
		}
		
		public void PublishError(string error)
		{
			var errorTopic = "/" + DeviceName + "/Error";
			Client.Publish (errorTopic, Encoding.UTF8.GetBytes (error));
			PublishStatus(1);
		}
		
		public void ClearErrorMessage()
		{
			var errorTopic = "/" + DeviceName + "/Error";
			Client.Publish (errorTopic, Encoding.UTF8.GetBytes (""));
		}
		
		public void PublishStatus(int status)
		{
			var statusTopic = "/" + DeviceName + "/Status";
			Client.Publish (statusTopic, Encoding.UTF8.GetBytes (status.ToString()));
		}
		
		public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
		{
			var topic = e.Topic;

			var value = System.Text.Encoding.Default.GetString(e.Message);

			var key = GetTopicKey(topic);

			DataEntry[key] = value;


			if (key == "Time")
			{
				Data.Add(DataEntry);
				PrintDataEntry(DataEntry);
				DataEntry = new Dictionary<string, string>();
			}
		}
		
		public void PrintDataEntry(Dictionary<string, string> dataEntry)
		{
			Console.WriteLine("");
			Console.WriteLine("===== Data");
			foreach (var key in dataEntry.Keys)
			{
				Console.Write(key + ":" + dataEntry[key] + ";");
			}
			Console.WriteLine(";");
			Console.WriteLine("=====");
			Console.WriteLine("");
		}
		
		public string GetTopicKey(string topic)
		{
			var parts = topic.Split('/');
			var key = parts[2];
			
			return key;
		}

		public void ResetData()
		{
			Data = new List<Dictionary<string, string>>();
		}
	}
}