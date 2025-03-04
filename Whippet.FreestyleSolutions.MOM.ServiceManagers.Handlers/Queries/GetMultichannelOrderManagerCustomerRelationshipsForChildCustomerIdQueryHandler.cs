﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Extensions.Threading.Tasks;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet;
using Athi.Whippet.FreestyleSolutions.MultichannelOrderManager.ServiceManagers.Queries;

namespace Athi.Whippet.FreestyleSolutions.MultichannelOrderManager.ServiceManagers.Handlers.Queries
{
    /// <summary>
    /// Query handler for <see cref="GetMultichannelOrderManagerCustomerRelationshipsForChildCustomerIdQuery"/> objects.
    /// </summary>
    public class GetMultichannelOrderManagerCustomerRelationshipsForChildCustomerIdQueryHandler : MultichannelOrderManagerCustomerRelationshipQueryHandlerBase<GetMultichannelOrderManagerCustomerRelationshipsForChildCustomerIdQuery>, IMultichannelOrderManagerCustomerRelationshipQueryHandler<GetMultichannelOrderManagerCustomerRelationshipsForChildCustomerIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMultichannelOrderManagerCustomerRelationshipsForChildCustomerIdQueryHandler"/> class with the specified <see cref="IWhippetQueryRepository{TEntity}"/>.
        /// </summary>
        /// <param name="repository"><see cref="IWhippetQueryRepository{TEntity}"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public GetMultichannelOrderManagerCustomerRelationshipsForChildCustomerIdQueryHandler(IWhippetQueryRepository<MultichannelOrderManagerCustomerRelationship> repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified query asynchronously.
        /// </summary>
        /// <param name="query"><see cref="IWhippetQuery{TEntity}"/> object to handle.</param>
        /// <returns><see cref="WhippetResultContainer{T}"/> object.</returns>
        /// <exception cref="ArgumentNullException" />
        public override async Task<WhippetResultContainer<IEnumerable<MultichannelOrderManagerCustomerRelationship>>> HandleAsync(GetMultichannelOrderManagerCustomerRelationshipsForChildCustomerIdQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            else
            {
                WhippetResultContainer<IEnumerable<MultichannelOrderManagerCustomerRelationship>> result = await Repository.GetCustomerRelationshipsForChildCustomerAsync(query.CustomerId);
                return new WhippetResultContainer<IEnumerable<MultichannelOrderManagerCustomerRelationship>>(result.Result, result.Item);
            }
        }
    }
}
