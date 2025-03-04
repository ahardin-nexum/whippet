﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet.Security.Tenants;

namespace Athi.Whippet.Salesforce.ServiceManagers.Queries
{
    /// <summary>
    /// Represents a query that retrieves all <see cref="SalesforcePriceBook"/> objects based on a specific name.
    /// </summary>
    public class GetAllSalesforcePriceBooksQuery : WhippetQuery<SalesforcePriceBook>, IWhippetQuery<SalesforcePriceBook>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllSalesforcePriceBooksQuery"/> class with no arguments.
        /// </summary>
        public GetAllSalesforcePriceBooksQuery()
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
