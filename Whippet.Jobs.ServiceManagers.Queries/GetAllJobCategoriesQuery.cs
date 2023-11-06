﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Data.CQRS;

namespace Athi.Whippet.Jobs.ServiceManagers.Queries
{
    /// <summary>
    /// Represents a query that retrieves all <see cref="JobCategory"/> objects in the system. This class cannot be inherited.
    /// </summary>
    public sealed class GetAllJobCategoriesQuery : WhippetQuery<JobCategory>, IWhippetQuery<JobCategory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllJobCategoriesQuery"/> class with no arguments.
        /// </summary>
        public GetAllJobCategoriesQuery()
            : base()
        { }

        /// <summary>
        /// Gets an <see cref="IReadOnlyDictionary{TKey, TValue}"/> indexed by the query parameters and their associated values in the object's current state.
        /// </summary>
        /// <returns><see cref="IReadOnlyDictionary{TKey, TValue}"/> object.</returns>
        protected override IReadOnlyDictionary<string, object> GetQueryParametersAndValues()
        {
            return NoParameters;
        }
    }
}
