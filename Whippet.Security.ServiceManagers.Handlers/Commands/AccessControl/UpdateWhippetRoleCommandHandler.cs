﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet.Security.AccessControl.Repositories;
using Athi.Whippet.Security.AccessControl.ServiceManagers.Commands;

namespace Athi.Whippet.Security.AccessControl.ServiceManagers.Handlers.Commands
{
    /// <summary>
    /// Command handler for <see cref="UpdateWhippetRoleCommand"/> objects.
    /// </summary>
    public class UpdateWhippetRoleCommandHandler : WhippetRoleCommandHandlerBase<UpdateWhippetRoleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateWhippetRoleCommandHandler"/> class with the specified <see cref="IWhippetRoleRepository"/> object.
        /// </summary>
        /// <param name="repository"><see cref="IWhippetRoleRepository"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public UpdateWhippetRoleCommandHandler(IWhippetRoleRepository repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified command asynchronously.
        /// </summary>
        /// <param name="command"><typeparamref name="TCommand"/> object to handle.</param>
        /// <returns><see cref="WhippetResult"/> which contains the result of the command.</returns>
        /// <exception cref="ArgumentNullException" />
        public override async Task<WhippetResult> HandleAsync(UpdateWhippetRoleCommand command)
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
                    result = await Repository.UpdateAsync(command.Role);
                }

                return result;
            }
        }

        /// <summary>
        /// Validates the specified <see cref="UpdateWhippetRoleCommand"/> object.
        /// </summary>
        /// <param name="command"><see cref="UpdateWhippetRoleCommand"/> object to validate.</param>
        /// <returns><see cref="WhippetResult"/> object.</returns>
        protected override WhippetResult Validate(UpdateWhippetRoleCommand command)
        {
            WhippetResult result = WhippetResult.Success;

            if (command == null || command.Role == null)
            {
                result = new WhippetResult(new ArgumentNullException(nameof(command)));
            }
            else
            {
                if (String.IsNullOrWhiteSpace(command.Role.Name))
                {
                    result = new WhippetResult(new ArgumentNullException(nameof(command.Role.Name)));
                }
            }

            return result;
        }
    }
}
