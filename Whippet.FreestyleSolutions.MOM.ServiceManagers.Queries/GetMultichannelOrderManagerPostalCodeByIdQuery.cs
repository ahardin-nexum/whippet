﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Data.CQRS;

namespace Athi.Whippet.FreestyleSolutions.MultichannelOrderManager.ServiceManagers.Queries
{
    /// <summary>
    /// Represents a query that retrieves a <see cref="GetMultichannelOrderManagerPostalCodeByIdQuery"/> object by its ID. This class cannot be inherited.
    /// </summary>
    public class GetMultichannelOrderManagerPostalCodeByIdQuery : WhippetQuery<MultichannelOrderManagerPostalCode>, IWhippetQuery<MultichannelOrderManagerPostalCode>
    {
        /// <summary>
        /// Gets or sets the ID to query by.
        /// </summary>
        public long ID
        { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMultichannelOrderManagerPostalCodeByIdQuery"/> class with no arguments.
        /// </summary>
        private GetMultichannelOrderManagerPostalCodeByIdQuery()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMultichannelOrderManagerPostalCodeByIdQuery"/> class with the specified ID.
        /// </summary>
        /// <param name="id">ID of the <see cref="GetMultichannelOrderManagerPostalCodeByIdQuery"/> object to retrieve.</param>
        public GetMultichannelOrderManagerPostalCodeByIdQuery(long id)
            : this()
        {
            ID = id;
        }

        /// <summary>
        /// Gets an <see cref="IReadOnlyDictionary{TKey, TValue}"/> indexed by the query parameters and their associated values in the object's current state. This method must be overridden.
        /// </summary>
        /// <returns><see cref="IReadOnlyDictionary{TKey, TValue}"/> object.</returns>
        protected override IReadOnlyDictionary<string, object> GetQueryParametersAndValues()
        {
            return new Dictionary<string, object>(new[] { new KeyValuePair<string, object>(nameof(ID), ID) });
        }
    }
}
