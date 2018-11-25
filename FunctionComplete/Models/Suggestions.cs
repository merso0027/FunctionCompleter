﻿using System.Collections.Generic;

namespace FunctionComplete.Models
{
    /// <summary>
    /// Suggestion based on token.
    /// </summary>
    public class Suggestions
    {
        public Suggestions()
        {
            CompleteFunctions = new List<string>();
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

        public List<string> CompleteStructures { get; set; }

        public List<string> CompleteVariables { get; set; }

        /// <summary>
        /// Signatures of functions. 
        /// It can be more that once since function can be overloaded.
        /// </summary>
        public List<string> Signatures { get; set; }
    }
}
