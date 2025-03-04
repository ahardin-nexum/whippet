﻿using System;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Cache.ServiceManagers.Queries;

namespace Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Cache.ServiceManagers.Handlers.Queries
{
    /// <summary>
    /// Query handler for <see cref="GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryByRecordNumberQuery"/> objects.
    /// </summary>
    public class GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryByRecordNumberQueryHandler : MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryQueryHandlerBase<GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryByRecordNumberQuery>, IMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryQueryHandler<GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryByRecordNumberQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryByIdQueryHandler"/> class with the specified <see cref="IWhippetQueryRepository{TEntity}"/>.
        /// </summary>
        /// <param name="repository"><see cref="IWhippetQueryRepository{TEntity}"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryByRecordNumberQueryHandler(IWhippetQueryRepository<MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntry> repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified query asynchronously.
        /// </summary>
        /// <param name="query"><see cref="IWhippetQuery{TEntity}"/> object to handle.</param>
        /// <returns><see cref="WhippetResultContainer{T}"/> object.</returns>
        /// <exception cref="ArgumentNullException" />
        public override async Task<WhippetResultContainer<IEnumerable<MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntry>>> HandleAsync(GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryByRecordNumberQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            else
            {
                WhippetResultContainer<MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntry> result = await Repository.GetEntryByRecordNumberAsync(query.Cache, query.RecordNumber);
                return new WhippetResultContainer<IEnumerable<MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntry>>(result.Result, new[] { result.Item });
            }
        }
    }
}
