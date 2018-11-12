using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class FunctionRepository
    {
        /// <summary>
        /// Read all functions signatures from file.
        /// </summary>
        /// <returns></returns>
        public List<string> GetRawFunctions()
        {
            var path = Directory.GetCurrentDirectory() + "\\Data\\AvailableFunctions.txt";
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
