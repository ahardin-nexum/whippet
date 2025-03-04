﻿using System;
using Athi.Whippet.FreestyleSolutions.MultichannelOrderManager.ServiceManagers.Commands;
using Athi.Whippet.FreestyleSolutions.MultichannelOrderManager.Repositories;

namespace Athi.Whippet.FreestyleSolutions.MultichannelOrderManager.ServiceManagers.Handlers.Commands
{
    /// <summary>
    /// Command handler for <see cref="UpdateMultichannelOrderManagerRestEndpointCommand"/> objects.
    /// </summary>
    public class UpdateMultichannelOrderManagerRestEndpointCommandHandler : MultichannelOrderManagerRestEndpointCommandHandlerBase<UpdateMultichannelOrderManagerRestEndpointCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMultichannelOrderManagerRestEndpointCommandHandler"/> class with the specified <see cref="IMultichannelOrderManagerRestEndpointRepository"/> object.
        /// </summary>
        /// <param name="repository"><see cref="IMultichannelOrderManagerRestEndpointRepository"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public UpdateMultichannelOrderManagerRestEndpointCommandHandler(IMultichannelOrderManagerRestEndpointRepository repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified command asynchronously.
        /// </summary>
        /// <param name="command"><see cref="IMultichannelOrderManagerRestEndpointCommand"/> object to handle.</param>
        /// <returns><see cref="WhippetResult"/> which contains the result of the command.</returns>
        /// <exception cref="ArgumentNullException" />
        public override async Task<WhippetResult> HandleAsync(UpdateMultichannelOrderManagerRestEndpointCommand command)
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
                    result = await Repository.UpdateAsync(command.RestEndpoint);
                }

                return result;
            }
        }

        /// <summary>
        /// Validates the specified <see cref="UpdateMultichannelOrderManagerRestEndpointCommand"/> object.
        /// </summary>
        /// <param name="command"><see cref="UpdateMultichannelOrderManagerRestEndpointCommand"/> object to validate.</param>
        /// <returns><see cref="WhippetResult"/> object.</returns>
        protected override WhippetResult Validate(UpdateMultichannelOrderManagerRestEndpointCommand command)
        {
            WhippetResult result = WhippetResult.Success;

            if (command == null || command.RestEndpoint == null)
            {
                result = new WhippetResult(new ArgumentNullException(nameof(command)));
            }

            return result;
        }
    }
}
