﻿using System;
using Athi.Whippet.Data.CQRS;

namespace Athi.Whippet.Adobe.Magento.Directory.ServiceManagers.Queries
{
    /// <summary>
    /// Represents a query that retrieves all <see cref="Region"/> objects in the system. This class cannot be inherited.
    /// </summary>
    public sealed class GetAllRegionsQuery : WhippetQuery<Region>, IWhippetQuery<Region>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllRegionsQuery"/> class with no arguments.
        /// </summary>
        public GetAllRegionsQuery()
            : base()
        { }

        /// <summary>
        /// Gets an <see cref="IReadOnlyDictionary{TKey, TValue}"/> indexed by the query parameters and their associated values in the object's current state. This method must be overridden.
        /// </summary>
        /// <returns><see cref="IReadOnlyDictionary{TKey, TValue}"/> object.</returns>
        protected override IReadOnlyDictionary<string, object> GetQueryParametersAndValues()
        {
            return NoParameters;
        }
    }
}
