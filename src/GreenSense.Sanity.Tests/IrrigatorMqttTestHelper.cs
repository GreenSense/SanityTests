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
	public class IrrigatorMqttTestHelper : MonitorMqttTestHelper
	{
	    public IrrigatorMqttTestHelper(string deviceName) : base(deviceName)
	    {
	    }
	    
	    public void RunPumpStatusTests()
	    {
	        Console.WriteLine("");
			Console.WriteLine("Running pump status test...");
			
			// Off
			RunPumpStatusTest(0);
			
			// Auto
			RunPumpStatusTest(2);
			
	    }
	    
	    public void RunPumpStatusTest(int pumpStatus)
	    {
			Console.WriteLine("");
			Console.WriteLine("Setting pump status to: " + pumpStatus);

			SendCommand("P", pumpStatus);
			
			Thread.Sleep(3000);
			
			WaitForData(1);
			
			Assert.AreEqual(pumpStatus.ToString(), Data[0]["P"], "Invalid pump status");
	    }
	}
}