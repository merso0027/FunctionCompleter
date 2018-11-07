using System;
using FunctionComplete.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunctionCompleterTests
{
    [TestClass]
    public class FunctionRepositoryTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var functionRepository = new FunctionRepository();
            functionRepository.GetCleanFunctions();
        }
    }
}
