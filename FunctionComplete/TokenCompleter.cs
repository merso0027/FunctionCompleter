using FunctionComplete.Services;
using System;
using System.Collections.Generic;

namespace FunctionComplete
{
    public class TokenCompleter
    {
        private FunctionCompleteService functionCompleteService;

        private FunctionSignatureService functionSignatureService;

        private TokenValidationService tokenValidationService;

        public TokenCompleter()
        {
            functionCompleteService = new FunctionCompleteService();
            functionSignatureService = new FunctionSignatureService();
            tokenValidationService = new TokenValidationService();
        }

        public Tuple<List<string>, List<string>> Complete(string token)
        {
            if (tokenValidationService.IsTokenValid(token))
            {
                throw new NotImplementedException();
            }

            var currentFunction = functionCompleteService.GetCurrentFunction(token);
            var currentWholeFunction = functionSignatureService.GetWholeCurrentFunction(token);

            var complete = functionCompleteService.GetFunctionComplete(token);
            var suggest = functionSignatureService.GetFunctionSignatures(token);

            return new Tuple<List<string>, List<string>>(complete, suggest);
        }
    }
}
