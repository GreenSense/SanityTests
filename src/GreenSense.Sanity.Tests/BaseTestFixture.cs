using System;
using NUnit.Framework;
using System.IO;

namespace GreenSense.Sanity.Tests
{
    public abstract class BaseTestFixture
    {
        public string OriginalDirectory;
        public string TemporaryDirectory;

        public bool DeleteTemporaryDirectory = true;

        public BaseTestFixture ()
        {
        }

        [SetUp]
        public void SetUp()
		{
			Console.WriteLine ("");

            OriginalDirectory = Environment.CurrentDirectory;

            TemporaryDirectory = new TemporaryDirectoryCreator ().Create ();

            Directory.SetCurrentDirectory (TemporaryDirectory);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.SetCurrentDirectory (OriginalDirectory);

            if (DeleteTemporaryDirectory && TemporaryDirectory.ToLower().Contains("-tmp")) {
                Directory.Delete (TemporaryDirectory, true);
            }

			Console.WriteLine ("");
			Console.WriteLine ("");
        }
    }
}

