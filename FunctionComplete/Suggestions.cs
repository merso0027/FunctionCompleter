using System.Collections.Generic;

namespace FunctionComplete
{
    /// <summary>
    /// Suggestion based on token.
    /// </summary>
    public class Suggestions
    {
        public Suggestions()
        {
            CompleteFunctions = new List<string>();
            CompleteStructures = new List<string>();
            CompleteVariables = new List<string>();
            Signatures = new List<string>();
        }
        /// <summary>
        /// Text of token before current typing function.
        /// </summary>
        public string TokenToCurrent { get; set; }

        /// <summary>
        /// Suggestions for user how to complete function.
        /// </summary>
        public List<string> CompleteFunctions { get; set; }

        /// <summary>
        /// Suggestions for user how to complete structure.
        /// </summary>
        public List<string> CompleteStructures { get; set; }

        /// <summary>
        /// Suggestions for user how to complete variable.
        /// </summary>
        public List<string> CompleteVariables { get; set; }

        /// <summary>
        /// Signatures of current typing function. 
        /// It can be more that once since function can be overloaded.
        /// </summary>
        public List<string> Signatures { get; set; }
    }
}
