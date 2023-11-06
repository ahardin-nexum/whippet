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
    /// Base class for <see cref="ISalesforceContactCommand"/> command handlers. This class must be inherited.
    /// </summary>
    /// <typeparam name="TCommand"><see cref="ISalesforceContactCommand"/> type to handle.</typeparam>
    public abstract class SalesforceContactCommandHandlerBase<TCommand> : WhippetCommandHandler<TCommand>, IWhippetCommandHandler<TCommand>, ISalesforceContactCommandHandler<TCommand>
        where TCommand : ISalesforceContactCommand
    {
        /// <summary>
        /// Gets the internal <see cref="ISalesforceContactRepository"/> to execute the commands against. This property is read-only.
        /// </summary>
        protected ISalesforceContactRepository Repository
        { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesforceContactCommandHandlerBase{TCommand}"/> class with the specified <see cref="ISalesforceContactRepository"/> object.
        /// </summary>
        /// <param name="repository"><see cref="ISalesforceContactRepository"/> to initialize with.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SalesforceContactCommandHandlerBase(ISalesforceContactRepository repository)
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

            if (command == null || command.Contact == null)
            {
                result = new WhippetResult(new ArgumentNullException(nameof(command)));
            }

            return result;
        }
    }
}
