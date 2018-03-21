using System;
using NUnit.Framework;
using System.IO;
using System.Net.NetworkInformation;

namespace GreenSense.Sanity.Tests
{
    [TestFixture]
    public class PingGardenTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_PingGarden()
        {
			PingHost ("greensense.hopto.org");
        }

		public static bool PingHost(string nameOrAddress)
		{
			bool pingable = false;
			Ping pinger = new Ping();
			try
			{
				PingReply reply = pinger.Send(nameOrAddress);
				pingable = reply.Status == IPStatus.Success;
			}
			catch (PingException ex)
			{
				Assert.Fail (ex.ToString ());
			}
			return pingable;
		}
    }
}

