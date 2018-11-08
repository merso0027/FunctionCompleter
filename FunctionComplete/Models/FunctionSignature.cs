using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionComplete.Models
{
    public class FunctionSignature
    {
        public FunctionSignature()
        {
            Arguments = new List<Argument>();
        }

        public string Name { get; set; }

        public string Signature { get; set; }

        public IList<Argument> Arguments { get; set; }
    }
}
