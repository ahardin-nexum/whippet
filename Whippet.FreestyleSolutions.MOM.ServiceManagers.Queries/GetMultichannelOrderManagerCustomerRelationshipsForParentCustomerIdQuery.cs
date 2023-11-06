﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet.FreestyleSolutions.MultichannelOrderManager;

namespace Athi.Whippet.FreestyleSolutions.MultichannelOrderManager.ServiceManagers.Queries
{
    /// <summary>
    /// Retrieves all <see cref="IMultichannelOrderManagerCustomerRelationship"/> objects for a parent <see cref="IMultichannelOrderManagerCustomer"/>.
    /// </summary>
    public class GetMultichannelOrderManagerCustomerRelationshipsForParentCustomerIdQuery : WhippetQuery<MultichannelOrderManagerCustomerRelationship>, IWhippetQuery<MultichannelOrderManagerCustomerRelationship>
    {
        /// <summary>
        /// Gets or sets the customer ID of the parent <see cref="IMultichannelOrderManagerCustomer"/> object.
        /// </summary>
        public long CustomerId
        { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMultichannelOrderManagerCustomerRelationshipsForParentCustomerIdQuery"/> class with no arguments.
        /// </summary>
        private GetMultichannelOrderManagerCustomerRelationshipsForParentCustomerIdQuery()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMultichannelOrderManagerCustomerRelationshipsForParentCustomerIdQuery"/> class with the specified customer ID.
        /// </summary>
        /// <param name="customerId">ID of the parent <see cref="IMultichannelOrderManagerCustomer"/>.</param>
        public GetMultichannelOrderManagerCustomerRelationshipsForParentCustomerIdQuery(long customerId)
            : this()
        {
            CustomerId = customerId;
        }

        /// <summary>
        /// Gets an <see cref="IReadOnlyDictionary{TKey, TValue}"/> indexed by the query parameters and their associated values in the object's current state. This method must be overridden.
        /// </summary>
        /// <returns><see cref="IReadOnlyDictionary{TKey, TValue}"/> object.</returns>
        protected override IReadOnlyDictionary<string, object> GetQueryParametersAndValues()
        {
            return new Dictionary<string, object>(new[] { new KeyValuePair<string, object>(nameof(CustomerId), CustomerId) });
        }
    }
}
