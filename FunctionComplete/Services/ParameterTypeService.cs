using FunctionComplete.Helpers;
using FunctionComplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FunctionComplete.Services
{
    public class ParameterTypeService
    {
        public List<String> GetAllowedTypes(string token, string lastWholeFunctionName, List<FunctionSignature> functions)
        {
            if (string.IsNullOrWhiteSpace(lastWholeFunctionName))
            {
                return functions.Select(t => t.ReturnType)
                                .Distinct()
                                .ToList();
            }

            var parameterOrder = GetParameterOrder(token, lastWholeFunctionName);
            var allowedTypes = functions.Where(f => f.Arguments.Count > parameterOrder
                                                 && f.FunctionName == lastWholeFunctionName)
                                              .Select(t => t.Arguments[parameterOrder].Type)
                                              .Distinct()
                                              .ToList();
            return allowedTypes;
        }

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
            int parameterCount = withoutNestedBrackets.Count(f => f == ',');
            return parameterCount;
        }
    }
}
