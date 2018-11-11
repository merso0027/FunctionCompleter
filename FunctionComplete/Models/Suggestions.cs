using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionComplete.Models
{
    public class Suggestions
    {
        public Suggestions()
        {
            Complete = new List<string>();
            Signatures = new List<string>();
        }
        public string TokenToCurrent { get; set; }

        public List<string> Complete { get; set; }

        public List<string> Signatures { get; set; }
    }
}
