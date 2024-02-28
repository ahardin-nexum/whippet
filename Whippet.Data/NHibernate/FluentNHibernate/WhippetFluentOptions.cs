namespace Athi.Whippet.Data.NHibernate.FluentNHibernate
{
    /// <summary>
    /// Provides configuration options to Whippet's NHibernate Fluent mappings. This class cannot be inherited.
    /// </summary>
    public static class WhippetFluentOptions
    {
        /// <summary>
        /// If <see langword="true"/>, all domain tables will be decorated with square brackets. Otherwise, no decoration will be applied.
        /// </summary>
        /// <remarks>By default, this is set to <see langword="true"/>.</remarks>
        public static bool UseSquareBracketDecoration
        { get; set; } = true;

        /// <summary>
        /// If <see langword="true"/>, all domain tables with their pseudo-schema will be separated by a period ('.'). Otherwise, an underscore will be supplied.
        /// </summary>
        /// <remarks>By default, this is set to <see langword="true"/>.</remarks>
        public static bool UseDotSeperator
        { get; set; } = true;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetFluentOptions"/> class with no arguments.
        /// </summary>
        static WhippetFluentOptions()
        { }
    }
}
