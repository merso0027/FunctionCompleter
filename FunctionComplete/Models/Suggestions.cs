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

        /// <summary>
        /// Text before current function.
        /// </summary>
        public string TokenToCurrent { get; set; }

        /// <summary>
        /// Suggestions for user how to complete function.
        /// </summary>
        public List<string> Complete { get; set; }

        /// <summary>
        /// Signatures of functions. 
        /// It can be more that once since function can be overloaded.
        /// </summary>
        public List<string> Signatures { get; set; }
    }
}
