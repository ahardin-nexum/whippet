﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet.Salesforce.ServiceManagers.Commands;
using Athi.Whippet.Salesforce.Repositories;

namespace Athi.Whippet.Salesforce.ServiceManagers.Handlers.Commands
{
    /// <summary>
    /// Base class for <see cref="ISalesforceAccountCommand"/> command handlers. This class must be inherited.
    /// </summary>
    /// <typeparam name="TCommand"><see cref="ISalesforceAccountCommand"/> type to handle.</typeparam>
    public abstract class SalesforceAccountCommandHandlerBase<TCommand> : WhippetCommandHandler<TCommand>, IWhippetCommandHandler<TCommand>, ISalesforceAccountCommandHandler<TCommand>
        where TCommand : ISalesforceAccountCommand
    {
        /// <summary>
        /// Gets the internal <see cref="ISalesforceAccountRepository"/> to execute the commands against. This property is read-only.
        /// </summary>
        protected ISalesforceAccountRepository Repository
        { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesforceAccountCommandHandlerBase{TCommand}"/> class with the specified <see cref="ISalesforceAccountRepository"/> object.
        /// </summary>
        /// <param name="repository"><see cref="ISalesforceAccountRepository"/> to initialize with.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SalesforceAccountCommandHandlerBase(ISalesforceAccountRepository repository)
            : base()
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            else
            {
                Repository = repository;
            }
        }

        /// <summary>
        /// Validates the specified <typeparamref name="TCommand"/> object.
        /// </summary>
        /// <param name="command"><typeparamref name="TCommand"/> object to validate.</param>
        /// <returns><see cref="WhippetResult"/> object.</returns>
        protected override WhippetResult Validate(TCommand command)
        {
            WhippetResult result = WhippetResult.Success;

            if (command == null || command.Account == null)
            {
                result = new WhippetResult(new ArgumentNullException(nameof(command)));
            }

            return result;
        }
    }
}
