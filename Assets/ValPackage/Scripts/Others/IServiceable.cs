namespace ValeryPopov.Common
{
    /// <summary>
    /// Check serviceable classes on scene by <see cref="ServiceableChecker.CheckServiceable"/>.
    /// Use snippet "checkserviceable"
    /// </summary>
    public interface IServiceable
    {
        /// <summary>
        /// Check data (fields, any conditions).
        /// Use snippet "checkserviceable" or "ifeditor"
        /// <code>
        ///public string CheckServiceable()
        ///{
        ///    if (errorCondition)
        ///        return "Error message";
        ///
        ///    return null; // all correct
        ///}
        /// </code>
        /// </summary>
        /// <returns>Error if return not null</returns>
        string CheckServiceable();
    }
}