using FunctionCompleteCommon;
using System.Collections.Generic;
using System.IO;

namespace FunctionCompleteCommon
{
    internal class FileSignatureService : ISignaturesService
    {
        public List<string> GetOperators()
        {
            throw new System.NotImplementedException();
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
