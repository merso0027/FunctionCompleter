using FunctionComplete.Interfaces;
using FunctionComplete.Models;
using FunctionComplete.Repository;
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

        public TokenCompleter()
        {
            IFunctionRepository functionRepository = new FunctionRepository();
            functionCompleteService = new FunctionCompleteService();
            functionSignatureService = new FunctionSignatureService();
            tokenValidationService = new TokenValidationService();
            functionService = new FunctionParserService(functionRepository);
        }

        public Suggestions Run(string token)
        {
            string cleanToken = Regex.Replace(token, @"\s+", "");
            if (cleanToken == string.Empty)
            {
                return new Suggestions();
            }
            if (!tokenValidationService.IsTokenValid(cleanToken))
            {
                throw new NotImplementedException();
            }
            List<FunctionSignature> functions = functionService.GetAllFunctions();

            var currentFunction = functionCompleteService.GetCurrentFunctionName(cleanToken);
            var currentWholeFunction = functionSignatureService.GetWholeCurrentFunctionName(cleanToken);
            var tokenToFunction = cleanToken.Substring(0, cleanToken.LastIndexOf(currentFunction));

            var complete = functionCompleteService.GetFunctionComplete(currentFunction, functions);
            var signatures = functionSignatureService.GetFunctionSignatures(currentWholeFunction, functions);

            return new Suggestions()
            {
                Complete = complete,
                Signatures = signatures,
                TokenToCurrent = tokenToFunction
            };
        }
    }
}
