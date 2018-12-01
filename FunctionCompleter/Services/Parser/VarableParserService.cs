using FunctionComplete.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FunctionComplete.Services
{
    internal class VarableParserService
    {
        private List<string> rawVariables;
        public VarableParserService(List<string> rawVariables)
        {
            this.rawVariables = rawVariables;
        }

        public List<Variable> GetAllVariables()
        {
            var cleanRaw = cleanRawVariables(rawVariables);
            var res = parseVariables(cleanRaw);
            return res;
        }

        private List<string> cleanRawVariables(List<string> rawVariables)
        {
            var res = new List<string>();
            foreach (var item in rawVariables)
            {
                string cleanItem = item.Trim();
                cleanItem = Regex.Replace(cleanItem, @"\s+", "");
                res.Add(cleanItem);
            }
            return res;
        }

        private List<Variable> parseVariables(List<string> signatrues)
        {
            var res = new List<Variable>();
            foreach (var item in signatrues)
            {
                res.Add(parseVariable(item));
            }
            return res;
        }

        private Variable parseVariable(string signature)
        {
            var splitSignature = signature.Split(':');
            var variableName = splitSignature[0];
            var variableType = splitSignature[1];
            return new Variable()
            {
                Name = variableName,
                Type = variableType
            };
        }
    }
}
