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
	public class MqttTestHelper : BaseMqttTestHelper
	{
	    public MqttTestHelper(string deviceName) : base(deviceName)
	    {}
	    
	    public void RunVersionTest(string projectName)
	    {
			Console.WriteLine("");
			Console.WriteLine("Checking device version");
			
			WaitForData(1);
			
			var deviceVersion = Data[0]["Z"];
			
			Console.WriteLine("Device version: " + deviceVersion);
			
			var baseUrl = "https://raw.githubusercontent.com/GreenSense/" + projectName  + "/master/";
			var buildNumberUrl = baseUrl + "buildnumber.txt";
			var versionUrl = baseUrl + "version.txt";
			
			var sourceVersion = GetHttpContent(versionUrl) + "-" + GetHttpContent(buildNumberUrl);
			
			Console.WriteLine("Source version: " + sourceVersion);
			
			Assert.AreEqual(sourceVersion, deviceVersion, "Invalid device version");
	    }
	}
}