using FunctionComplete.Interfaces;
using FunctionComplete.Models;
using FunctionComplete.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FunctionComplete
{
    public class TokenCompleter
    {
        private FunctionCompleteService functionCompleteService;

        private FunctionSignatureService functionSignatureService;

        private TokenValidationService tokenValidationService;

        private FunctionParserService functionService;

        public TokenCompleter(IFunctionRepository functionRepository)
        {
            functionCompleteService = new FunctionCompleteService();
            functionSignatureService = new FunctionSignatureService();
            tokenValidationService = new TokenValidationService();
            functionService = new FunctionParserService(functionRepository);
        }

        public Tuple<List<string>, List<string>> Complete(string token)
        {
            string cleanToken = Regex.Replace(token, @"\s+", "");
            if (!tokenValidationService.IsTokenValid(cleanToken))
            {
                throw new NotImplementedException();
            }
            List<FunctionSignature> functions = functionService.GetAllFunctions();

            var currentFunction = functionCompleteService.GetCurrentFunctionName(cleanToken);
            var currentWholeFunction = functionSignatureService.GetWholeCurrentFunctionName(cleanToken);

            var complete = functionCompleteService.GetFunctionComplete(currentFunction, functions);
            var suggest = functionSignatureService.GetFunctionSignatures(currentWholeFunction, functions);

            return new Tuple<List<string>, List<string>>(complete, suggest);
        }
    }
}
