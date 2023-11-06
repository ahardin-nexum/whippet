﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Extensions.Threading.Tasks;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet.Security.AccessControl.Repositories;
using Athi.Whippet.Security.AccessControl.ServiceManagers.Commands;

namespace Athi.Whippet.Security.AccessControl.ServiceManagers.Handlers.Commands
{
    /// <summary>
    /// Base class for <see cref="IWhippetRoleUserAssignmentCommand"/> command handlers. This class must be inherited.
    /// </summary>
    /// <typeparam name="TCommand"><see cref="IWhippetRoleUserAssignmentCommand"/> type to handle.</typeparam>
    public abstract class WhippetRoleUserAssignmentCommandHandlerBase<TCommand> : WhippetCommandHandler<TCommand>, IWhippetCommandHandler<TCommand>, IWhippetRoleUserAssignmentCommandHandler<TCommand>
        where TCommand : IWhippetRoleUserAssignmentCommand
    {
        /// <summary>
        /// Gets or sets the internal <see cref="IWhippetRoleUserAssignmentRepository"/> to execute the commands against.
        /// </summary>
        protected IWhippetRoleUserAssignmentRepository Repository
        { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhippetRoleUserAssignmentCommandHandlerBase{TCommand}"/> class with the specified <see cref="IWhippetRoleUserAssignmentRepository"/> object.
        /// </summary>
        /// <param name="repository"><see cref="IWhippetRoleUserAssignmentRepository"/> to initialize with.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected WhippetRoleUserAssignmentCommandHandlerBase(IWhippetRoleUserAssignmentRepository repository)
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
    }
}
