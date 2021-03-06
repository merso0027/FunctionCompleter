﻿using System.Collections.Generic;

namespace FunctionComplete.Models
{
    /// <summary>
    /// Function signature
    /// </summary>
    internal class FunctionSignature
    {
        public FunctionSignature()
        {
            Arguments = new List<Argument>();
        }

        /// <summary>
        /// Whole function signature
        /// </summary>
        public string SignatureText { get; set; }

        /// <summary>
        /// Function name
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// Return type
        /// </summary>
        public string ReturnType { get; set; }
        /// <summary>
        /// All function arguments
        /// </summary>
        public List<Argument> Arguments { get; set; }
    }
}
