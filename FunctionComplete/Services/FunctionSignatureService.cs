using FunctionComplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionComplete.Services
{
    public class FunctionSignatureService
    {
        private FunctionCompleteService functionCompleteService;

        public FunctionSignatureService()
        {
            functionCompleteService = new FunctionCompleteService();
        }

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
            var res = functionCompleteService.GetCurrentFunctionName(tokenToWholeFunction);
            return res;
        }

        public List<String> GetFunctionSignatures(string functionToken, List<FunctionSignature> functions)
        {
            return functions.Where(t => t.Name.StartsWith(functionToken))
               .Select(b => b.Signature)
               .ToList();
        }
    }
}
