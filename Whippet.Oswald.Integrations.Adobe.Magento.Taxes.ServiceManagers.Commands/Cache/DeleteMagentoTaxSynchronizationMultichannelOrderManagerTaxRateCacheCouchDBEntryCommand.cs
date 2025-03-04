﻿using System;
using Athi.Whippet.Data.CQRS;

namespace Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Cache.ServiceManagers.Commands
{
    /// <summary>
    /// Command that deletes an existing <see cref="MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntry"/> object in the data store. This class cannot be inherited.
    /// </summary>
    public sealed class DeleteMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryCommand : MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryCommandBase, IWhippetCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryCommand"/> class with no arguments.
        /// </summary>
        private DeleteMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryCommand()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryCommand"/> class with the specified <see cref="MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntry"/>.
        /// </summary>
        /// <param name="entry"><see cref="MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntry"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DeleteMagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntryCommand(MagentoTaxSynchronizationMultichannelOrderManagerTaxRateCacheCouchDBEntry entry)
            : base(entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
        }
    }
}
