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
        private readonly ParameterTypeService parameterTypeService;

        private readonly List<FunctionSignature> functions;
        private readonly List<Variable> variables;
        private readonly List<Structure> structures;

        public TokenCompleter(List<string> rawFunctions, List<string> rawStrucutres, List<string> rawVariables)
        {
            functionCompleteService = new FunctionCompleteService();
            functionSignatureService = new FunctionSignatureService();
            tokenValidationService = new TokenValidationService();
            parameterTypeService = new ParameterTypeService();
            functions = new FunctionParserService(rawFunctions).GetAllFunctions();
            variables = new VarableParserService(rawVariables).GetAllVariables();
            structures = new StructurePareserService(rawStrucutres).GetAllStructures();
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

            var currentFunction = functionCompleteService.CurrentFunctionName(cleanToken);
            var currentWholeFunction = functionSignatureService.GetWholeCurrentFunctionName(cleanToken);
            var allowedTypes = parameterTypeService.GetAllowedTypes(cleanToken, currentWholeFunction, functions);
            result.TokenToCurrent = cleanToken;
            if (!string.IsNullOrWhiteSpace(currentFunction))
            {
                result.TokenToCurrent = cleanToken.Substring(0, cleanToken.LastIndexOf(currentFunction));
            }
            result.CompleteFunctions = functionCompleteService.GetFunctionComplete(currentFunction, functions, allowedTypes);
            result.Signatures = functionSignatureService.GetFunctionSignatures(currentWholeFunction, functions);
            return result;
        }
    }
}
