﻿using System;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Cache;
using Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Cache.ServiceManagers.Handlers.Queries;
using Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Cache.ServiceManagers.Queries;

namespace Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Cache.ServiceManagers.Handlers.Queries
{
    /// <summary>
    /// Query handler for <see cref="GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntriesForCacheQuery"/> objects.
    /// </summary>
    public class GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntriesForCacheQueryHandler : MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryQueryHandlerBase<GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntriesForCacheQuery>, IMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryQueryHandler<GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntriesForCacheQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntriesForCacheQueryHandler"/> class with the specified <see cref="IWhippetQueryRepository{TEntity}"/>.
        /// </summary>
        /// <param name="repository"><see cref="IWhippetQueryRepository{TEntity}"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntriesForCacheQueryHandler(IWhippetQueryRepository<MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntry> repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified query asynchronously.
        /// </summary>
        /// <param name="query"><see cref="IWhippetQuery{TEntity}"/> object to handle.</param>
        /// <returns><see cref="WhippetResultContainer{T}"/> object.</returns>
        /// <exception cref="ArgumentNullException" />
        public override async Task<WhippetResultContainer<IEnumerable<MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntry>>> HandleAsync(GetMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntriesForCacheQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            else
            {
                WhippetResultContainer<IEnumerable<MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntry>> result = await Repository.GetAsync(query.Cache);
                return new WhippetResultContainer<IEnumerable<MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntry>>(result.Result, result.Item);
            }
        }
    }
}