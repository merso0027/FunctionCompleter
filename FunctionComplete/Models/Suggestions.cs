using System.Collections.Generic;

namespace FunctionComplete.Models
{
    /// <summary>
    /// Suggestion based on token.
    /// </summary>
    public class Suggestions
    {
        public Suggestions()
        {
            Complete = new List<string>();
            Signatures = new List<string>();
        }

        /// <summary>
        /// Text of token before current typing function.
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
