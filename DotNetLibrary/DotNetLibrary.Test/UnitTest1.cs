using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using DotNetLibrary.Interfaces;

namespace DotNetLibrary.Test
{
    [TestFixture]
    public class UnitTest1
    {
        private readonly IApiProcessor _processor = new ApiProcessor();

        [Test]
        public void TestMethod1()
        {
            try
            {
                object obj = _processor.Get<object>("http://clubsyncws.valuecomusa.com/v2/Item/GetDetailItemObject?itemisn=70013245");
                Console.WriteLine("End");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
