using System.Collections.Generic;

namespace FunctionComplete.Models
{
    /// <summary>
    /// Variables
    /// </summary>
    internal class Structure
    {
        /// <summary>
        /// Structure name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of properties
        /// </summary>
        public List<Argument> Properties {get;set;}
    }
}
