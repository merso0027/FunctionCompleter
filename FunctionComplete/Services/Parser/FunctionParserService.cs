using FunctionComplete.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FunctionComplete.Services
{
    internal class FunctionParserService
    {
        private List<string> rawFunctions;
        public FunctionParserService(List<string> rawFunctions)
        {
            this.rawFunctions = rawFunctions;
        }

        /// <summary>
        /// Get all functions paresed from raw list.
        /// </summary>
        /// <returns>List of function signature objects.</returns>
        public List<FunctionSignature> GetAllFunctions()
        {
            var cleanRaw = cleanRawFunctions(rawFunctions);
            var res = ParseFunctionSignatures(cleanRaw);
            return res;
        }

        /// <summary>
        /// Clean raw data before parsing.
        /// </summary>
        /// <param name="rawFunctions">Raw functions list.</param>
        /// <returns>Clean function list</returns>
        private List<string> cleanRawFunctions(List<string> rawFunctions)
        {
            var res = new List<string>();
            foreach (var item in rawFunctions)
            {
                string cleanItem = item.Trim();
                cleanItem = Regex.Replace(cleanItem, @"\s+", " ");
                cleanItem = cleanItem.Replace("( ", "(");
                cleanItem = cleanItem.Replace(" (", "(");
                cleanItem = cleanItem.Replace(") ", ")");
                cleanItem = cleanItem.Replace(" )", ")");
                cleanItem = cleanItem.Replace(", ", ",");
                cleanItem = cleanItem.Replace(" ,", ",");
                cleanItem = cleanItem.Replace(": ", ":");
                cleanItem = cleanItem.Replace(" :", ":");
                res.Add(cleanItem);
            }
            return res;
        }

        /// <summary>
        /// Parse list of string functions to list of object function signatures.
        /// </summary>
        /// <param name="signatrues">List of clean string functions</param>
        /// <returns></returns>
        private List<FunctionSignature> ParseFunctionSignatures(List<string> signatrues)
        {
            var res = new List<FunctionSignature>();
            foreach (var item in signatrues)
            {
                res.Add(ParseFunctionSignature(item));
            }
            return res;
        }

        /// <summary>
        /// Parse one function string signature to function object signature.
        /// </summary>
        /// <param name="signature">Function signature string</param>
        /// <returns>Function signature object.</returns>
        private FunctionSignature ParseFunctionSignature(string signature)
        {
            string returnType = signature.Split(':')[1];
            string functionName = (signature.Split('(')[0]).Trim();
            int functionStartIndex = signature.IndexOf("(");
            int functionEndIndex = signature.IndexOf(")");
            string allArguments = signature.Substring(functionStartIndex + 1, functionEndIndex - functionStartIndex - 1);

            var arguments = new List<Argument>();
            if (allArguments != string.Empty)
            {
                string[] functionParameters = allArguments.Split(',');
                foreach (var item in functionParameters)
                {
                    var type = item.Split(' ')[0];
                    var name = item.Split(' ')[1];
                    arguments.Add(new Argument()
                    {
                        Name = name,
                        Type = type
                    });
                }
            }

            return new FunctionSignature()
            {
                SignatureText = signature,
                FunctionName = functionName,
                ReturnType = returnType,
                Arguments = arguments
            };
        }
    }
}
