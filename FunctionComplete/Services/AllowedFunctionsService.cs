using FunctionComplete.Helpers;
using FunctionComplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FunctionComplete.Services
{
    internal class AllowedFunctionsService
    {
        /// <summary>
        /// Get allowed function types based on current function parameter type.
        /// </summary>
        /// <param name="token">token value</param>
        /// <param name="lastWholeFunctionName">last whole function name</param>
        /// <param name="functions">All functions</param>
        /// <param name="variables">All varables</param>
        /// <param name="structures">All structures</param>
        /// <returns></returns>
        public List<String> GetAllowedFunctionTypes(string token, string lastWholeFunctionName,
             List<FunctionSignature> functions, List<Variable> variables,List<Structure> structures)
        {
            // If this is no whole function. For example since this is first function.
            if (string.IsNullOrWhiteSpace(lastWholeFunctionName))
            {
                var func = functions.Select(t => t.ReturnType)
                                .Distinct()
                                .ToList();

                var vars = variables.Select(t => t.Type)
                .Distinct()
                .ToList();
                //Get all possible types form return types and from varables.
                return vars.Concat(func).Distinct().ToList();
            }

            var parameterOrder = GetParameterOrder(token, lastWholeFunctionName);
            var allowedTypes = functions.Where(f => f.Arguments.Count > parameterOrder
                                                 && f.FunctionName == lastWholeFunctionName)
                                              .Select(t => t.Arguments[parameterOrder].Type)
                                              .Distinct()
                                              .ToList();
            var structureNames = structures.Select(g => g.Name);
            return allowedTypes.Concat(structureNames).ToList();
        }

        /// <summary>
        /// Get parameter order in current typing function.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="lastWholeFunctionName"></param>
        /// <returns></returns>
        private int GetParameterOrder(string token, string lastWholeFunctionName)
        {
            Regex regexObj = new Regex(
                @"\(              # Match an opening parenthesis.
                  (?>             # Then either match (possessively):
                   [^()]+         #  any characters except parentheses
                  |               # or
                   \( (?<Depth>)  #  an opening paren (and increase the parens counter)
                  |               # or
                   \) (?<-Depth>) #  a closing paren (and decrease the parens counter).
                  )*              # Repeat as needed.
                 (?(Depth)(?!))   # Assert that the parens counter is at zero.
                 \)               # Then match a closing parenthesis.",
                RegexOptions.IgnorePatternWhitespace);
            
            var withoutNestedBrackets = regexObj.Replace(token, string.Empty);
            // Count parameters is actually count of comma',' without nested brackets.
            int parameterCount = withoutNestedBrackets.Count(f => f == ',');
            return parameterCount;
        }
    }
}
