using FunctionComplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionComplete.Services
{
    internal class VariableCompleteService
    {
        /// <summary>
        /// All suggestions
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="functions">All functions</param>
        /// <returns></returns>
        internal List<String> GetVariableComplete(string token, List<Variable> functions, List<string> allowedTypes)
        {
            return functions.Where(t => t.Name.ToUpper().StartsWith(token.ToUpper()))
                .Where(t=>allowedTypes.Contains(t.Type))
                .Select(b => b.Name)
                .Distinct()
                .ToList();
        }
    }
}
