using FunctionComplete.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FunctionComplete.Services
{
    internal class StructureParserService
    {
        private List<string> rawStructures;
        public StructureParserService(List<string> rawStructures)
        {
            this.rawStructures = rawStructures;
        }

        public List<Structure> GetAllStructures()
        {
            var cleanRaw = cleanRawStructures(rawStructures);
            var res = parseStructures(cleanRaw);
            return res;
        }

        private List<string> cleanRawStructures(List<string> rawStructures)
        {
            var res = new List<string>();
            foreach (var item in rawStructures)
            {
                string cleanItem = item.Trim();
                cleanItem = Regex.Replace(cleanItem, @"\s+", " ");
                cleanItem = cleanItem.Replace("{ ", "{");
                cleanItem = cleanItem.Replace(" {", "{");
                cleanItem = cleanItem.Replace("} ", "}");
                cleanItem = cleanItem.Replace(" }", "}");
                cleanItem = cleanItem.Replace(", ", ",");
                cleanItem = cleanItem.Replace(" ,", ",");
                res.Add(cleanItem);
            }
            return res;
        }

        private List<Structure> parseStructures(List<string> signatrues)
        {
            var res = new List<Structure>();
            foreach (var item in signatrues)
            {
                res.Add(parseVariable(item));
            }
            return res;
        }

        private Structure parseVariable(string signature)
        {
            string structureName = signature.Split('{')[0];
            int structureStartIndex = signature.IndexOf("{");
            int structureEndIndex = signature.IndexOf("}");
            string allProperties = signature.Substring(structureStartIndex + 1, structureEndIndex - structureStartIndex - 1);

            var arguments = new List<Argument>();
            string[] functionParameters = allProperties.Split(',');
            foreach (var item in functionParameters)
            {
                var type = item.Split(' ')[0];
                var name = item.Split(' ')[1];
                arguments.Add(new Argument()
                {
                    Name = name,
                    Type = type
                });
            }

            return new Structure()
            {
                Name = structureName,
                Properties = arguments
            };
        }

    }
}
