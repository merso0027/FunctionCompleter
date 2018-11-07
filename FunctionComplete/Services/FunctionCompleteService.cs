using FunctionComplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionComplete.Services
{
    public class FunctionCompleteService
    {
        public bool HasFunction(string token)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentFunction(string token)
        {
            throw new NotImplementedException();
        }

        public List<String> GetFunctionComplete(string functionName)
        {
            throw new NotImplementedException();
        }

        //public IList<string> GetSuggestions(string text)
        //{
        //    string relativeText = text.Trim();
        //    var functionParser = new FunctionRepository();
        //    var allFunctions = functionParser.GetCleanFunctions();
        //    if (relativeText.EndsWith("(") || relativeText.EndsWith(",") || relativeText.EndsWith("="))
        //    {
        //        relativeText = "";
        //    }

        //    var lastIndexOfZagrada = text.LastIndexOf("(");
        //    string doZagrade = text.Substring(0, lastIndexOfZagrada).Trim();


        //    var lastIndexOfSpace = doZagrade.LastIndexOf(" ");
        //    var lastIndexOfZarez = doZagrade.LastIndexOf(",");
        //    var lastIndexOfJednako = doZagrade.LastIndexOf("=");
        //    int max3 = Math.Max(lastIndexOfSpace, Math.Max(lastIndexOfZarez, lastIndexOfJednako));
        //    string nazivPoslednjeCele = doZagrade.Substring(max3 == -1 ? 0 : max3, doZagrade.Length - max3).Trim();

        //    List<string> res = allFunctions.Where(f => f.Name.StartsWith(relativeText)).Select(r => r.Name).ToList();
        //    return res;
        //}


    }
}
