﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Data.CQRS;

namespace Athi.Whippet.Localization.Addressing.ServiceManagers.Queries
{
    /// <summary>
    /// Represents a query that retrieves an <see cref="InvariantAddress"/> by its ID. This class cannot be inherited.
    /// </summary>
    public sealed class GetInvariantAddressByIdQuery : WhippetQuery<InvariantAddress>, IWhippetQuery<InvariantAddress>
    {
        /// <summary>
        /// Gets the ID of the <see cref="InvariantAddress"/> to retrieve. This property is read-only.
        /// </summary>
        public Guid ID
        { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetInvariantAddressByIdQuery"/> class with no arguments.
        /// </summary>
        private GetInvariantAddressByIdQuery()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetInvariantAddressByIdQuery"/> class with the specified ID.
        /// </summary>
        /// <param name="id">ID of the <see cref="InvariantAddress"/> to retrieve.</param>
        public GetInvariantAddressByIdQuery(Guid id)
            : this()
        {
            ID = id;
        }

        /// <summary>
        /// Gets an <see cref="IReadOnlyDictionary{TKey, TValue}"/> indexed by the query parameters and their associated values in the object's current state.
        /// </summary>
        /// <returns><see cref="IReadOnlyDictionary{TKey, TValue}"/> object.</returns>
        protected override IReadOnlyDictionary<string, object> GetQueryParametersAndValues()
        {
            return new Dictionary<string, object>(new[] { new KeyValuePair<string, object>(nameof(ID), ID) });
        }
    }
}
