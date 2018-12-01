using System.Collections.Generic;

namespace SignatureRepository
{
    /// <summary>
    /// Service signatures interface
    /// </summary>
    public interface ISignaturesService
    {
        /// <summary>
        /// Get raw functions list of strings.
        /// Functions has to be in format:
        /// functionName(type1 variable1,type2 variable2 , ... , typeN variableN): returnType
        /// For example:
        /// max(int x,int y):int
        /// </summary>
        /// <returns>List of raw function strings in predefined format.</returns>
        List<string> GetRawFunctions();

        /// <summary>
        /// Get raw structures list of strings
        /// Structure has name and list of properties.
        /// Structures has to be in format:
        /// Struct1{Struct2 s2, Struct3 s3, ... , StructN N}
        /// For example:
        /// Europe{Country details, string name}
        /// Pay attention that property could be other structures or could value types.
        /// </summary>
        /// <returns></returns>
        List<string> GetRawStructures();

        /// <summary>
        /// Get raw variables list of strings
        /// Varalbles has name and type.
        /// Type could be structure or value type
        /// Variable format:
        /// Variable:type
        /// for example:
        /// FirstName:string
        /// </summary>
        /// <returns></returns>
        List<string> GetRawVariables();

        /// <summary>
        /// Array of operators which can be used in suggestion.
        /// </summary>
        /// <returns></returns>
        char[] GetOperators();
    }
}
