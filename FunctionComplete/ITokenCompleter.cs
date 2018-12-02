namespace FunctionComplete
{
    public interface ITokenCompleter
    {
        /// <summary>
        /// Get suggestions for current typing text.
        /// </summary>
        /// <param name="token">Typing text</param>
        /// <returns>Suggestions object with all necessary details for suggestions.</returns>
        Suggestions Run(string token);

        /// <summary>
        /// Set new fresh data for variables, functions and structures.
        /// In case of new structure, variable or functions is added in meantime.
        /// </summary>
        void RefreshSignatures();
    }
}
