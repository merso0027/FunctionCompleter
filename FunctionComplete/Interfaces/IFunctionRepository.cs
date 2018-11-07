using System;
using System.Collections.Generic;

namespace FunctionComplete.Interfaces
{
    public interface IFunctionRepository
    {
        List<string> ReadRawFunctionsFromFile();
    }
}
