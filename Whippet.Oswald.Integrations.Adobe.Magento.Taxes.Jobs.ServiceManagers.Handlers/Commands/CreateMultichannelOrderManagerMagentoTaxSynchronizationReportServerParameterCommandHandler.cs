﻿using System;
using Athi.Whippet.Jobs;
using Athi.Whippet.Jobs.ServiceManagers.Commands;
using Athi.Whippet.Jobs.ServiceManagers.Handlers.Commands;
using Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Jobs.Repositories;
using Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Jobs.ServiceManagers.Commands;

namespace Athi.Whippet.Oswald.Integrations.Adobe.Magento.Taxes.Jobs.ServiceManagers.Handlers.Commands
{
    /// <summary>
    /// Command handler for <see cref="CreateMultichannelOrderManagerMagentoTaxSynchronizationReportServerParameterCommand"/> objects.
    /// </summary>
    public class CreateMultichannelOrderManagerMagentoTaxSynchronizationReportServerParameterCommandHandler : CreateJobParameterCommandHandler<MultichannelOrderManagerMagentoTaxSynchronizationReportServerParameter, MultichannelOrderManagerMagentoTaxSynchronizationJob>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMultichannelOrderManagerMagentoTaxSynchronizationReportServerParameterCommandHandler"/> class with the specified <see cref="IMultichannelOrderManagerMagentoTaxSynchronizationReportServerParameterRepository"/> object.
        /// </summary>
        /// <param name="repository"><see cref="IMultichannelOrderManagerMagentoTaxSynchronizationReportServerParameterRepository"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public CreateMultichannelOrderManagerMagentoTaxSynchronizationReportServerParameterCommandHandler(IMultichannelOrderManagerMagentoTaxSynchronizationReportServerParameterRepository repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified command asynchronously.
        /// </summary>
        /// <param name="command"><see cref="IJobCommand"/> object to handle.</param>
        /// <returns><see cref="WhippetResult"/> which contains the result of the command.</returns>
        /// <exception cref="ArgumentNullException" />
        public async Task<WhippetResult> HandleAsync(CreateMultichannelOrderManagerMagentoTaxSynchronizationReportServerParameterCommand command)
        {
            return await base.HandleAsync(command);
        }
    }
}
