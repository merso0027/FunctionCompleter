using System.Collections.Generic;

namespace FunctionCompleteCommon
{
    public interface ISignaturesService
    {
        List<string> GetRawFunctions();

        List<string> GetRawStructures();

        List<string> GetRawVariables();

        List<string> GetOperators();
    }
}
