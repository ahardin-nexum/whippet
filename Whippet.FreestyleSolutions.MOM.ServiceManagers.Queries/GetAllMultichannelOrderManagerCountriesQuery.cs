﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Data.CQRS;

namespace Athi.Whippet.FreestyleSolutions.MultichannelOrderManager.ServiceManagers.Queries
{
    /// <summary>
    /// Represents a query that retrieves all <see cref="GetAllMultichannelOrderManagerCountriesQuery"/> objects in the system. This class cannot be inherited.
    /// </summary>
    public class GetAllMultichannelOrderManagerCountriesQuery : WhippetQuery<MultichannelOrderManagerCountry>, IWhippetQuery<MultichannelOrderManagerCountry>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllMultichannelOrderManagerCountriesQuery"/> class with no arguments.
        /// </summary>
        public GetAllMultichannelOrderManagerCountriesQuery()
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
