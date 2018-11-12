using FunctionComplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionComplete.Services
{
    public class FunctionCompleteService
    {
        public string GetCurrentFunctionName(string token)
        {
            var lastIndexOfComma = token.LastIndexOf(",");
            var lastIndexOfEquals = token.LastIndexOf("=");
            var lastIndexOfOpenParenthesis = token.LastIndexOf("(");
            int lastOfAll = Math.Max(lastIndexOfComma, Math.Max(lastIndexOfEquals, lastIndexOfOpenParenthesis));
            if (lastOfAll < 1) return token;
            string currentFunctionName = token.Substring(lastOfAll + 1, token.Length - lastOfAll - 1);
            return currentFunctionName;
        }

        public List<String> GetFunctionComplete(string functionToken, List<FunctionSignature> functions)
        {
            return functions.Where(t => t.Name.StartsWith(functionToken))
                .Select(b => b.Name)
                .Distinct()
                .ToList();
        }
    }
}
