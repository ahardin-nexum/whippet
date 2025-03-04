﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet.Security.ServiceManagers.Commands;
using Athi.Whippet.Security.Repositories;

namespace Athi.Whippet.Security.ServiceManagers.Handlers.Commands
{
    /// <summary>
    /// Command handler for <see cref="UpdateWhippetUserCommand"/> objects.
    /// </summary>
    public class UpdateWhippetUserCommandHandler : WhippetUserCommandHandlerBase<UpdateWhippetUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateWhippetUserCommandHandler"/> class with the specified <see cref="IWhippetUserRepository"/> object.
        /// </summary>
        /// <param name="repository"><see cref="IWhippetUserRepository"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public UpdateWhippetUserCommandHandler(IWhippetUserRepository repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified command asynchronously.
        /// </summary>
        /// <param name="command"><typeparamref name="TCommand"/> object to handle.</param>
        /// <returns><see cref="WhippetResult"/> which contains the result of the command.</returns>
        /// <exception cref="ArgumentNullException" />
        public override async Task<WhippetResult> HandleAsync(UpdateWhippetUserCommand command)
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
                    result = await Repository.UpdateAsync(command.User);
                }

                return result;
            }
        }

        /// <summary>
        /// Validates the specified <see cref="UpdateWhippetUserCommand"/> object.
        /// </summary>
        /// <param name="command"><see cref="UpdateWhippetUserCommand"/> object to validate.</param>
        /// <returns><see cref="WhippetResult"/> object.</returns>
        protected override WhippetResult Validate(UpdateWhippetUserCommand command)
        {
            WhippetResult result = WhippetResult.Success;

            if (command == null || command.User == null)
            {
                result = new WhippetResult(new ArgumentNullException(nameof(command)));
            }
            else
            {
                if (String.IsNullOrWhiteSpace(command.User.UserName))
                {
                    result = new WhippetResult(new ArgumentNullException(nameof(command.User.UserName)));
                }
                else if (String.IsNullOrWhiteSpace(command.User.Password))
                {
                    result = new WhippetResult(new ArgumentNullException(nameof(command.User.Password)));
                }
                else if (String.IsNullOrWhiteSpace(command.User.UserName))
                {
                    result = new WhippetResult(new ArgumentNullException(nameof(command.User.UserName)));
                }
            }

            return result;
        }
    }
}
