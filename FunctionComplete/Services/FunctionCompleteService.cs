using FunctionComplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionComplete.Services
{
    public class FunctionCompleteService
    {
        /// <summary>
        /// Current function typing name based on token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>function name</returns>
        public string CurrentFunctionName(string token)
        {
            var lastIndexOfComma = token.LastIndexOf(",");
            var lastIndexOfEquals = token.LastIndexOf("=");
            var lastIndexOfOpenParenthesis = token.LastIndexOf("(");
            int lastOfAll = Math.Max(lastIndexOfComma, Math.Max(lastIndexOfEquals, lastIndexOfOpenParenthesis));
            if (lastOfAll < 1) return token;
            string currentFunctionName = token.Substring(lastOfAll + 1, token.Length - lastOfAll - 1);
            return currentFunctionName;
        }

        /// <summary>
        /// All suggestions
        /// </summary>
        /// <param name="functionToken">Token</param>
        /// <param name="functions">All functions</param>
        /// <returns></returns>
        public List<String> GetFunctionComplete(string functionToken, List<FunctionSignature> functions)
        {
            return functions.Where(t => t.FunctionName.ToUpper().StartsWith(functionToken.ToUpper()))
                .Select(b => b.FunctionName)
                .Distinct()
                .ToList();
        }
    }
}
