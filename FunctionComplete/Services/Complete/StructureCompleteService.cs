using FunctionComplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionComplete.Services
{
    public class StructureCompleteService
    {
        /// <summary>
        /// All suggestions
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="structures">All functions</param>
        /// <returns></returns>
        internal List<String> GetStructureComplete(string token, List<Structure> structures, List<Variable> variables)
        {
            string[] tokenSplit = token.Split('.');
            string root = tokenSplit[0];
            Variable rootVariable = variables.FirstOrDefault(t => t.Name == root);
            // if variable with root name not exist:
            if (rootVariable == null)
            {
                return new List<string>();
            }
            Structure structType = structures.FirstOrDefault(r => r.Name == rootVariable.Type);

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
            var properties = structType.Properties.Where(t => t.Name.StartsWith(tokenSplit.Last())).Select(g => g.Name);
            return properties.ToList();
        }
    }
}
