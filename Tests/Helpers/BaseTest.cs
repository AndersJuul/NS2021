using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Tests.Helpers
{
    public class BaseTest
    {
        [SetUp]
        public void Setup()
        {
            Debug.WriteLine("Test started at: " + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"));
        }
    }
}