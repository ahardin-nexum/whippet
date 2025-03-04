﻿using System;
using Athi.Whippet.Jobs;
using Athi.Whippet.Jobs.ServiceManagers.Commands;
using Athi.Whippet.Jobs.ServiceManagers.Handlers.Commands;
using Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Jobs.Repositories;
using Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Jobs.ServiceManagers.Commands;

namespace Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Jobs.ServiceManagers.Handlers.Commands
{
    /// <summary>
    /// Command handler for <see cref="UpdateMultichannelOrderManagerMagentoTaxSynchronizationSourceServerParameterCommand"/> objects.
    /// </summary>
    public class UpdateMultichannelOrderManagerMagentoTaxSynchronizationSourceServerParameterCommandHandler : UpdateJobParameterCommandHandler<MultichannelOrderManagerMagentoTaxSynchronizationSourceServerParameter, MultichannelOrderManagerMagentoTaxSynchronizationJob>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMultichannelOrderManagerMagentoTaxSynchronizationSourceServerParameterCommandHandler"/> class with the specified <see cref="IMultichannelOrderManagerMagentoTaxSynchronizationSourceServerParameterRepository"/> object.
        /// </summary>
        /// <param name="repository"><see cref="IMultichannelOrderManagerMagentoTaxSynchronizationSourceServerParameterRepository"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public UpdateMultichannelOrderManagerMagentoTaxSynchronizationSourceServerParameterCommandHandler(IMultichannelOrderManagerMagentoTaxSynchronizationSourceServerParameterRepository repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified command asynchronously.
        /// </summary>
        /// <param name="command"><see cref="IJobCommand"/> object to handle.</param>
        /// <returns><see cref="WhippetResult"/> which contains the result of the command.</returns>
        /// <exception cref="ArgumentNullException" />
        public async Task<WhippetResult> HandleAsync(UpdateMultichannelOrderManagerMagentoTaxSynchronizationSourceServerParameterCommand command)
        {
            return await base.HandleAsync(command);
        }
    }
}
