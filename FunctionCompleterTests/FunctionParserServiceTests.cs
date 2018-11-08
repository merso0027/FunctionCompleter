using FunctionComplete.Interfaces;
using FunctionComplete.Repository;
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
    public class FunctionParserServiceTests
    {
        private FunctionParserService functionParserService;
        public FunctionParserServiceTests()
        {
            IFunctionRepository functionRepository = new FunctionRepository();
            functionParserService = new FunctionParserService(functionRepository);
        }
        [TestMethod]
        public void TestMethod1()
        {

            Assert.AreEqual("Hello", "");
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual("hej", "");
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual("hej", "");
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual("hej", "");
        }
    }
}
