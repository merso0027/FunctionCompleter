using System.Collections.Generic;

namespace SignatureRepository
{
    public interface ISignaturesService
    {
        List<string> GetRawFunctions();

        List<string> GetRawStructures();

        List<string> GetRawVariables();

        char[] GetOperators();
    }
}
