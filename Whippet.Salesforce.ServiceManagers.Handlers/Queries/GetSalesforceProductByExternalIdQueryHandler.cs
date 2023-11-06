﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Extensions.Threading.Tasks;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet;
using Athi.Whippet.Salesforce.ServiceManagers.Queries;

namespace Athi.Whippet.Salesforce.ServiceManagers.Handlers.Queries
{
    /// <summary>
    /// Query handler for <see cref="GetSalesforceProductByExternalIdQuery"/> objects.
    /// </summary>
    public class GetSalesforceProductByExternalIdQueryHandler : SalesforceProductQueryHandlerBase<GetSalesforceProductByExternalIdQuery>, ISalesforceProductQueryHandler<GetSalesforceProductByExternalIdQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSalesforceProductByExternalIdQueryHandler"/> class with the specified <see cref="IWhippetQueryRepository{TEntity}"/>.
        /// </summary>
        /// <param name="repository"><see cref="IWhippetQueryRepository{TEntity}"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public GetSalesforceProductByExternalIdQueryHandler(IWhippetQueryRepository<SalesforceProduct> repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified query asynchronously.
        /// </summary>
        /// <param name="query"><see cref="IWhippetQuery{TEntity}"/> object to handle.</param>
        /// <returns><see cref="WhippetResultContainer{T}"/> object.</returns>
        /// <exception cref="ArgumentNullException" />
        public override async Task<WhippetResultContainer<IEnumerable<SalesforceProduct>>> HandleAsync(GetSalesforceProductByExternalIdQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            else
            {
                WhippetResultContainer<IEnumerable<SalesforceProduct>> queryResult = await Repository.GetByExternalIDAsync(query.ExternalId);
                return new WhippetResultContainer<IEnumerable<SalesforceProduct>>(queryResult.Result, queryResult.Item);
            }
        }
    }
}
