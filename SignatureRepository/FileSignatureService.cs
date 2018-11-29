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
            var path = Directory.GetCurrentDirectory() + "\\Data\\AvailableFunctions.txt";
            return ReadFromFile(path);
        }

        public List<string> GetRawStructures()
        {
            var path = Directory.GetCurrentDirectory() + "\\Data\\AvailableStructures.txt";
            return ReadFromFile(path);
        }

        public List<string> GetRawVariables()
        {
            var path = Directory.GetCurrentDirectory() + "\\Data\\AvailableVarables.txt";
            return ReadFromFile(path);
        }

        private List<string> ReadFromFile(string path)
        {
            var lines = File.ReadLines(path);
            var res = new List<string>();
            foreach (var line in lines)
            {
                res.Add(line);
            }
            return res;
        }
    }
}
