using FunctionComplete.Models;
using FunctionComplete.Services;
using SignatureRepository;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FunctionComplete
{
    public class TokenCompleter
    {
        private readonly FunctionCompleteService functionCompleteService;
        private readonly VariableCompleteService variableCompleteService;
        private readonly StructureCompleteService structureCompleteService;
        private readonly WholeFunctionSignatureService functionSignatureService;
        private readonly AllowedFunctionsService parameterTypeService;

        private readonly List<FunctionSignature> functions;
        private readonly List<Variable> variables;
        private readonly List<Structure> structures;

        public TokenCompleter(ISignaturesService signaturesService)
        {
            functionCompleteService = new FunctionCompleteService();
            variableCompleteService = new VariableCompleteService();
            structureCompleteService = new StructureCompleteService();
            functionSignatureService = new WholeFunctionSignatureService();
            parameterTypeService = new AllowedFunctionsService();
            functions = new FunctionParserService(signaturesService.GetRawFunctions()).GetAllFunctions();
            variables = new VarableParserService(signaturesService.GetRawVariables()).GetAllVariables();
            structures = new StructureParserService(signaturesService.GetRawStructures()).GetAllStructures();
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

            var currentTypingToken = functionCompleteService.CurrentFunctionName(cleanToken);
            result.TokenToCurrent = cleanToken;
            if (currentTypingToken.Contains("."))
            {
                result.CompleteStructures = structureCompleteService.GetStructureComplete(currentTypingToken, structures, variables);
                return result;
            }
            else
            {
                var currentWholeFunction = functionSignatureService.GetWholeCurrentFunctionName(cleanToken);
                var allowedFunctionTypes = parameterTypeService.GetAllowedFunctionTypes(cleanToken, currentWholeFunction, functions, variables, structures);
                if (!string.IsNullOrWhiteSpace(currentTypingToken))
                {
                    result.TokenToCurrent = cleanToken.Substring(0, cleanToken.LastIndexOf(currentTypingToken));
                }
                result.CompleteFunctions = functionCompleteService.GetFunctionComplete(currentTypingToken, functions, allowedFunctionTypes);
                result.Signatures = functionSignatureService.GetFunctionSignatures(currentWholeFunction, functions);
                result.CompleteVariables = variableCompleteService.GetVariableComplete(currentTypingToken, variables, allowedFunctionTypes);
                return result;
            }
        }
    }
}
