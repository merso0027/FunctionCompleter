using FunctionComplete.Models;
using FunctionComplete.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FunctionComplete
{
    public class TokenCompleter
    {
        private readonly FunctionCompleteService functionCompleteService;
        private readonly FunctionSignatureService functionSignatureService;
        private readonly TokenValidationService tokenValidationService;
        private readonly List<FunctionSignature> functions;

        public TokenCompleter(List<string> rawFunctions)
        {
            functionCompleteService = new FunctionCompleteService();
            functionSignatureService = new FunctionSignatureService();
            tokenValidationService = new TokenValidationService();
            var functionService = new FunctionParserService(rawFunctions);
            functions = functionService.GetAllFunctions();
        }

        /// <summary>
        /// Analize token to get suggestions
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>Suggestion object</returns>
        public Suggestions Run(string token)
        {
            string cleanToken = Regex.Replace(token, @"\s+", "");
            var result = new Suggestions();
            if (string.IsNullOrWhiteSpace(cleanToken))
            {
                return result;
            }

            if (!tokenValidationService.IsTokenValid(cleanToken))
            {
                throw new NotImplementedException();
            }

            var currentFunction = functionCompleteService.GetCurrentFunctionName(cleanToken);
            var currentWholeFunction = functionSignatureService.GetWholeCurrentFunctionName(cleanToken);

            result.TokenToCurrent = cleanToken.Substring(0, cleanToken.LastIndexOf(currentFunction));
            result.Complete = functionCompleteService.GetFunctionComplete(currentFunction, functions);
            result.Signatures = functionSignatureService.GetFunctionSignatures(currentWholeFunction, functions);
            return result;
        }
    }
}
