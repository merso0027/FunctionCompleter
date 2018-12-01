using FunctionComplete.Models;
using FunctionComplete.Services;
using SignatureRepository;
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
        private readonly ISignaturesService signaturesService;

        private List<FunctionSignature> functions;
        private List<Variable> variables;
        private List<Structure> structures;
        private char[] operators;

        public TokenCompleter(ISignaturesService signaturesService)
        {
            this.signaturesService = signaturesService;
            functionCompleteService = new FunctionCompleteService();
            variableCompleteService = new VariableCompleteService();
            structureCompleteService = new StructureCompleteService();
            functionSignatureService = new WholeFunctionSignatureService();
            parameterTypeService = new AllowedFunctionsService();
            RefreshSignatures();
        }

        /// <summary>
        /// Fresh raw data from services.
        /// </summary>
        public void RefreshSignatures()
        {
            functions = new FunctionParserService(signaturesService.GetRawFunctions()).GetAllFunctions();
            variables = new VarableParserService(signaturesService.GetRawVariables()).GetAllVariables();
            structures = new StructureParserService(signaturesService.GetRawStructures()).GetAllStructures();
            operators = signaturesService.GetOperators();
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

            var currentTypingToken = functionCompleteService.CurrentFunctionName(cleanToken, operators);
            result.TokenToCurrent = cleanToken;
            // Typing token is structure with property if conteins dot '.'
            if (currentTypingToken.Contains("."))
            {
                var lastDot = cleanToken.LastIndexOf(".");
                result.TokenToCurrent = cleanToken.Remove(lastDot) + ".";
                string function = string.Empty;
                // Structure root is function if typing token is ")."
                // Structure root could also be variable.
                if (currentTypingToken.StartsWith(")."))
                {
                    // Sturcutre root is function se check what is that function return type.
                    var previosTypingFunction = cleanToken.LastIndexOf(").");
                    var completeFunction = cleanToken.Remove(previosTypingFunction);
                    function = functionSignatureService.GetWholeCurrentFunctionName(completeFunction, operators);
                }
                result.CompleteStructures = structureCompleteService.GetStructureComplete(currentTypingToken, function, structures, variables, functions);
                return result;
            }
            // Typing token is function or variable
            else
            {
                var currentWholeFunction = functionSignatureService.GetWholeCurrentFunctionName(cleanToken, operators);
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
