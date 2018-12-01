using FunctionComplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionComplete.Services
{
    internal class StructureCompleteService
    {
        /// <summary>
        /// All suggestions
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="structures">All functions</param>
        /// <returns></returns>
        internal List<String> GetStructureComplete(string token, string functionName, List<Structure> structures, List<Variable> variables, List<FunctionSignature> functions)
        {
            string[] tokenSplit = token.Split('.');
            string root = tokenSplit[0];
            Structure structType = null;
            // If is function than get root type from function return types:
            if (string.IsNullOrWhiteSpace(functionName))
            {
                Variable rootVariable;
                rootVariable = variables.FirstOrDefault(t => t.Name == root);
                if (rootVariable == null)
                {
                    return new List<string>();
                }
                structType = structures.FirstOrDefault(r => r.Name == rootVariable.Type);
            }
            // If root is variable than get type from list of variables:
            else
            {
                var rootFunction = functions.FirstOrDefault(t => t.FunctionName == functionName);
                if (rootFunction == null)
                {
                    return new List<string>();
                }
                structType = structures.FirstOrDefault(r => r.Name == rootFunction.ReturnType);
            }

            // Root is function or variable, but everything after root value is structure property.
            // Loop through all strustrue properties
            for (int i = 1; i < tokenSplit.Count() - 1; i++)
            {
                if (structType == null)
                {
                    return new List<string>();
                }
                var structProperty = structType.Properties.FirstOrDefault(t => t.Name == tokenSplit[i]);
                if (structProperty == null)
                {
                    return new List<string>();
                }
                structType = structures.FirstOrDefault(r => r.Name == structProperty.Type);
            }
            if (structType == null)
            {
                return new List<string>();
            }
            // Get properties for last structure in array. 
            // This properties will exist only if everything before this are valid values
            var properties = structType.Properties.Where(t => t.Name.StartsWith(tokenSplit.Last())).Select(g => g.Name);
            return properties.ToList();
        }
    }
}
