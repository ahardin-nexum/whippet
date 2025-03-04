﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet.Localization.Addressing.ServiceManagers.Commands;
using Athi.Whippet.Localization.Addressing.Repositories;

namespace Athi.Whippet.Localization.Addressing.ServiceManagers.Handlers.Commands
{
    /// <summary>
    /// Command handler for <see cref="DeleteInvariantAddressCommand"/> objects.
    /// </summary>
    public class DeleteInvariantAddressCommandHandler : InvariantAddressCommandHandlerBase<DeleteInvariantAddressCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteInvariantAddressCommandHandler"/> class with the specified <see cref="IInvariantAddressRepository"/> object.
        /// </summary>
        /// <param name="repository"><see cref="IInvariantAddressRepository"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public DeleteInvariantAddressCommandHandler(IInvariantAddressRepository repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified command asynchronously.
        /// </summary>
        /// <param name="command"><see cref="IInvariantAddressCommand"/> object to handle.</param>
        /// <returns><see cref="WhippetResult"/> which contains the result of the command.</returns>
        /// <exception cref="ArgumentNullException" />
        public override async Task<WhippetResult> HandleAsync(DeleteInvariantAddressCommand command)
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
                    result = await Repository.DeleteAsync(command.Address);
                }

                return result;
            }
        }
    }
}
