﻿using System;
using Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Cache.Repositories;
using Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Cache.ServiceManagers.Commands;

namespace Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Cache.ServiceManagers.Handlers.Commands
{
    /// <summary>
    /// Command handler for <see cref="CreateMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryCommand"/> objects.
    /// </summary>
    public class CreateMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryCommandHandler : MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryCommandHandlerBase<CreateMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryCommandHandler"/> class with the specified <see cref="IMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryRepository"/> object.
        /// </summary>
        /// <param name="repository"><see cref="IMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryRepository"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public CreateMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryCommandHandler(IMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryRepository repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified command asynchronously.
        /// </summary>
        /// <param name="command"><see cref="IMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryCommand"/> object to handle.</param>
        /// <returns><see cref="WhippetResult"/> which contains the result of the command.</returns>
        /// <exception cref="ArgumentNullException" />
        public override async Task<WhippetResult> HandleAsync(CreateMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            else
            {
                WhippetResult result = Validate(command);

                if (result.IsSuccess)
                {
                    result = await Repository.CreateAsync(command.Entry);
                }

                return result;
            }
        }

        /// <summary>
        /// Validates the specified <see cref="CreateMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryCommand"/> object.
        /// </summary>
        /// <param name="command"><see cref="CreateMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryCommand"/> object to validate.</param>
        /// <returns><see cref="WhippetResult"/> object.</returns>
        protected override WhippetResult Validate(CreateMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheEntryCommand command)
        {
            WhippetResult result = WhippetResult.Success;

            if (command == null || command.Entry == null)
            {
                result = new WhippetResult(new ArgumentNullException(nameof(command)));
            }

            return result;
        }
    }
}