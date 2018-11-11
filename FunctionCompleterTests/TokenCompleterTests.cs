//using FunctionComplete;
//using FunctionComplete.Interfaces;
//using FunctionComplete.Repository;
//using FunctionComplete.Services;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Moq;

//namespace FunctionCompleterTests
//{
//    [TestClass]
//    public class TokenCompleterTests
//    {
//        private static List<string> repoData = new List<string>()
//        {
//            "jedan( int a,   int   b)",
//            "jedinica   (double hej, Task tra)",
//            "jedanAliVredan(  Task t1,   int b2)",
//            "GetSomething1(int a, int b, double c,  Task g, string first, string second, int others)",
//            "jedan( int a,   int   b, string         c)",
//            "GetElse  ( int g, int h, int s)",
//            "function1()",
//        };

//        private TokenCompleter tokenCompleter;

//        public TokenCompleterTests()
//        {
//            var mockFunctionRepository = new Mock<IFunctionRepository>();
//            mockFunctionRepository.Setup(f => f.GetRawFunctions())
//                .Returns(repoData);
//            tokenCompleter = new TokenCompleter();
//        }

//        [TestMethod]
//        public void TestMethod1()
//        {
//            var res = tokenCompleter.Complete("je");
//            var completeList = res.Item1;
//            var suggestionList = res.Item2;

//            Assert.AreEqual(4, completeList.Count);
//            Assert.AreEqual(0, suggestionList.Count);
//        }

//        [TestMethod]
//        public void TestMethod2()
//        {
//            var res = tokenCompleter.Complete("jedan(Ge");
//            var completeList = res.Item1;
//            var suggestionList = res.Item2;

//            Assert.AreEqual(2, completeList.Count);
//            Assert.AreEqual(2, suggestionList.Count);
//        }

//        [TestMethod]
//        public void TestMethod3()
//        {
//            var res = tokenCompleter.Complete("GetElse( jedinica (4, GetSomething1  (5,6,7)),   jedanAli");
//            var completeList = res.Item1;
//            var suggestionList = res.Item2;

//            Assert.AreEqual(1, completeList.Count);
//            Assert.AreEqual(1, suggestionList.Count);
//            Assert.AreEqual("jedanAliVredan", completeList.First());
//            Assert.AreEqual("GetElse(int g,int h,int s)", suggestionList.First());
//        }

//        [TestMethod]
//        public void TestMethod4()
//        {
//            var res = tokenCompleter.Complete("function1(function1(),functi");
//            var completeList = res.Item1;
//            var suggestionList = res.Item2;

//            Assert.AreEqual(1, completeList.Count);
//            Assert.AreEqual(1, suggestionList.Count);
//            Assert.AreEqual("function1", completeList.First());
//            Assert.AreEqual("function1()", suggestionList.First());
//        }
//    }
//}
