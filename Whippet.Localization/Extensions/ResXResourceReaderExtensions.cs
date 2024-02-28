using System;
using System.Collections;
using System.Resources.NetStandard;

namespace Athi.Whippet.Localization.Extensions
{
    /// <summary>
    /// Provides extension methods to <see cref="System.Resources.NetStandard.ResXResourceReader"/> objects. This class cannot be inherited.
    /// </summary>
    public static class ResXResourceReaderExtensions
    {
        /// <summary>
        /// Retrieves the resource with the specified key from the <see cref="ResXResourceReader"/> instance.
        /// </summary>
        /// <param name="reader"><see cref="ResXResourceReader"/> instance.</param>
        /// <param name="resourceName">Name of the resource to search for.</param>
        /// <returns>Resource value.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetResource(this ResXResourceReader reader, string resourceName)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            else if (String.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentNullException(nameof(resourceName));
            }
            else
            {
                string contents = null;
                
                foreach (DictionaryEntry d in reader)
                {
                    if (d.Key.ToString().Equals(resourceName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        contents = d.Value.ToString();
                        break;
                    }
                }

                return contents;
            }
        }
    }
}
