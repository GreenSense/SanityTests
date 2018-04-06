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
	public class MonitorMqttTestHelper : BaseMqttTestHelper
	{
	    public MonitorMqttTestHelper(string deviceName) : base(deviceName)
	    {
	    }
	    
	    public void RunReadIntervalTests(int maxInterval, int step)
	    {
			try
			{
				WaitForData(1);
				
				var existingInterval = Data[Data.Count-1]["V"];
				
				Console.WriteLine("Existing reading interval: " + existingInterval);
				
				for (int i = 1; i <= maxInterval; i+=step)
				{
					RunReadIntervalTest(i);
				}
				Console.WriteLine("");
				Console.WriteLine("Restoring original reading interval: " + existingInterval);
				
				SendCommand("V", existingInterval);
				
				Thread.Sleep(1000);
			}
			catch (Exception ex)
			{
				PublishError(ex.Message);
				
				throw ex;
			}
	    }
	    
		public void RunReadIntervalTest(int interval)
		{
			Console.WriteLine("");
			Console.WriteLine("Running read interval test...");
			Console.WriteLine("Interval: " + interval);
			
			Console.WriteLine("");
			Console.WriteLine("Setting read interval to " + interval);

			SendCommand("V", interval);
			
			Thread.Sleep(1000);

			WaitForData(3);

			Console.WriteLine("");
			Console.WriteLine("Checking entry times");
			
			CheckDataEntryTimes(interval);
			
			Console.WriteLine("");
			Console.WriteLine("Checking read interval value");
			
			Assert.AreEqual(interval.ToString(), Data[Data.Count-1]["V"], "Invalid read interval value");
		}
	}
}