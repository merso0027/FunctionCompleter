using System.Collections.Generic;

namespace FunctionComplete.Models
{
    /// <summary>
    /// Function signature
    /// </summary>
    public class FunctionSignature
    {
        public FunctionSignature()
        {
            Arguments = new List<Argument>();
        }

        /// <summary>
        /// Function name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whole function signature
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// All function arguments
        /// </summary>
        public IList<Argument> Arguments { get; set; }
    }
}
