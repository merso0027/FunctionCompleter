using FunctionComplete.Interfaces;
using FunctionComplete.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FunctionComplete.Services
{
    public class FunctionParserService
    {
        private IFunctionRepository functionRepository;

        public FunctionParserService(IFunctionRepository functionRepository)
        {
            this.functionRepository = functionRepository;
        }

        public List<FunctionSignature> GetAllFunctions()
        {
            var raw = functionRepository.GetRawFunctions();
            var cleanRaw = cleanRawFunctions(raw);
            var res = ParseFunctionSignatures(cleanRaw);
            return res;
        }

        private List<string> cleanRawFunctions(List<string> rawFunctions)
        {
            var res = new List<string>();
            foreach (var item in rawFunctions)
            {
                if (!isValid(item)) continue;
                string cleanItem = item.Trim();
                cleanItem = Regex.Replace(cleanItem, @"\s+", " ");
                cleanItem = cleanItem.Replace("( ", "(");
                cleanItem = cleanItem.Replace(" (", "(");
                cleanItem = cleanItem.Replace(") ", ")");
                cleanItem = cleanItem.Replace(" )", ")");
                cleanItem = cleanItem.Replace(", ", ",");
                cleanItem = cleanItem.Replace(" ,", ",");
                res.Add(cleanItem);
            }
            return res;
        }

        private bool isValid(string function)
        {
            return true;
        }

        private List<FunctionSignature> ParseFunctionSignatures(List<string> signatrues)
        {
            var res = new List<FunctionSignature>();
            foreach (var item in signatrues)
            {
                res.Add(ParseFunctionSignature(item));
            }
            return res;
        }

        private FunctionSignature ParseFunctionSignature(string signature)
        {
            string functionName = (signature.Split('(')[0]).Trim();
            int functionStartIndex = signature.IndexOf("(");
            int functionEndIndex = signature.IndexOf(")");
            string allArguments = signature.Substring(functionStartIndex + 1, functionEndIndex - functionStartIndex - 1);

            string[] functionParameters = allArguments.Split(',');
            var arguments = new List<Argument>();
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

            return new FunctionSignature()
            {
                Signature = signature,
                Name = functionName,
                Arguments = arguments
            };
        }
    }
}
