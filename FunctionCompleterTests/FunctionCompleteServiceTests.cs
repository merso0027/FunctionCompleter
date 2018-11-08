using FunctionComplete.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionCompleterTests
{
    [TestClass]
    public class FunctionCompleteServiceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            FunctionCompleteService functionCompleteService = new FunctionCompleteService();
            var res = functionCompleteService.GetCurrentFunctionName("Hello");

            Assert.AreEqual("Hello", res);
        }

        [TestMethod]
        public void TestMethod2()
        {
            FunctionCompleteService functionCompleteService = new FunctionCompleteService();
            var res = functionCompleteService.GetCurrentFunctionName("Hello(hej");

            Assert.AreEqual("hej", res);
        }

        [TestMethod]
        public void TestMethod3()
        {
            FunctionCompleteService functionCompleteService = new FunctionCompleteService();
            var res = functionCompleteService.GetCurrentFunctionName("Hello(hej,jes");

            Assert.AreEqual("jes", res);
        }

        [TestMethod]
        public void TestMethod4()
        {
            FunctionCompleteService functionCompleteService = new FunctionCompleteService();
            var res = functionCompleteService.GetCurrentFunctionName("a=trala");

            Assert.AreEqual("trala", res);
        }
    }
}
