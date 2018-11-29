using FunctionComplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionComplete.Services
{
    internal class WholeFunctionSignatureService
    {
        private FunctionCompleteService functionCompleteService;

        public WholeFunctionSignatureService()
        {
            functionCompleteService = new FunctionCompleteService();
        }

        /// <summary>
        /// Get name of complete function. Function that is started, not finished by name is writen but without all parameters.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetWholeCurrentFunctionName(string token)
        {
            char[] array = token.ToCharArray();

            int openParenthesisCount = 0;
            int closedParenthesisCount = 0;
            int indexWhereWholeFunctionEnds = 0;
            for (int i = array.Length - 1; i != 0; i--)
            {
                if (array[i] == ')') closedParenthesisCount++;
                if (array[i] == '(') openParenthesisCount++;

                if (openParenthesisCount > closedParenthesisCount)
                {
                    indexWhereWholeFunctionEnds = i;
                    break;
                }
            }
            string tokenToWholeFunction = token.Substring(0, indexWhereWholeFunctionEnds);
            var res = functionCompleteService.CurrentFunctionName(tokenToWholeFunction);
            return res;
        }

        /// <summary>
        /// Signatures of whole current function.
        /// </summary>
        /// <param name="functionToken">Token</param>
        /// <param name="functions">All function names.</param>
        /// <returns></returns>
        public List<String> GetFunctionSignatures(string functionToken, List<FunctionSignature> functions)
        {
            if (functionToken == string.Empty) return new List<string>();

            return functions.Where(t => t.FunctionName.ToUpper() == functionToken.ToUpper())
               .Select(b => b.SignatureText)
               .ToList();
        }
    }
}
