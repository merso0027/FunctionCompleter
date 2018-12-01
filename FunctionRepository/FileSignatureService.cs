using System.Collections.Generic;
using System.IO;

namespace SignatureRepository
{
    internal class FileSignatureService : ISignaturesService
    {
        public char[] GetOperators()
        {
            char[] operators = new char[5];
            operators[0] = '+';
            operators[1] = '-';
            operators[2] = '*';
            operators[3] = '/';
            operators[4] = '^';
            return operators;
        }

        /// <summary>
        /// Read all functions signatures from file
        /// </summary>
        /// <returns></returns>
        public List<string> GetRawFunctions()
        {

            return new List<string>() {
                       "plus( int a,   int   b)   :   int",
                       "plus   (int a, int b, int c) : int",
                       "plus (string first, string second)    :   string",
                       "minus(int a, int b)  :  int",
                       "times(int x, int y) : int",
                       "times(int x, int y, int z) :int",
                       "max (int x, int y):int",
                       "min (int x, int y):Task",
                       "AddCusomer(string name,int age): string",
                       "random() :double",
                       "getEurope():Europe",
                       "getAmerica():America",
                       "getAsia():Asia",
                       "getAfrica():Africa",
                       "getContinent():Continent"
                       };
        }

        public List<string> GetRawStructures()
        {
            return new List<string>() {
                      "Task{int a,string b}",
                      " Continent{Europe europe, America america, Africa africa,Asia asia}",
                      " Europe{Country france, Country serbia}",
                      " America{Country usa, Country canada}",
                      " Africa{Country maroco, Country nigeria }",
                      " Asia{Country india, Country china }",
                      " Country{string name, string size}"
            };
        }

        public List<string> GetRawVariables()
        {
            return new List<string>() {
                      "customeAge:int",
                      "customerName:string",
                      "myTask:Task",
                      "myContinet:Continent",
                      "myEurope:Europe",
                      "myAfrica:Africa",
                      "myAsia:Asia",
                      "myAmerica:America"
            };
        }
    }
}
