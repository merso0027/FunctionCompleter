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
    public class FunctionSignatureServiceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            FunctionSignatureService functionSignatureService = new FunctionSignatureService();
            var res = functionSignatureService.GetWholeCurrentFunctionName("Hello(hej(y,8),89");

          //  Assert.AreEqual("Hello", res);
        }

        [TestMethod]
        public void TestMethod2()
        {
            FunctionSignatureService functionSignatureService = new FunctionSignatureService();
            var res = functionSignatureService.GetWholeCurrentFunctionName("damir(jedan,dva,tri(a(t,y),b(),osam(4)");

            Assert.AreEqual("tri", res);
        }

        [TestMethod]
        public void TestMethod3()
        {
            FunctionSignatureService functionSignatureService = new FunctionSignatureService();
            var res = functionSignatureService.GetWholeCurrentFunctionName("damir(jedan,dva,tri(a,b,");

            Assert.AreEqual("jes", res);
        }

        [TestMethod]
        public void TestMethod4()
        {
            FunctionSignatureService functionSignatureService = new FunctionSignatureService();
            var res = functionSignatureService.GetWholeCurrentFunctionName("damir(jedan,dva,tri(a,b,");

            Assert.AreEqual("trala", res);
        }
    }
}
