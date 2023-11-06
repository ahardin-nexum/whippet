﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Extensions.Threading.Tasks;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet.Security.AccessControl.ServiceManagers.Commands;

namespace Athi.Whippet.Security.AccessControl.ServiceManagers.Handlers.Commands
{
    /// <summary>
    /// Handles commands for <see cref="IWhippetRoleCommand"/> objects.
    /// </summary>
    /// <typeparam name="TCommand"><see cref="IWhippetRoleCommand"/> object to handle.</typeparam>
    public interface IWhippetRoleCommandHandler<TCommand> : IWhippetCommandHandler<TCommand> where TCommand : IWhippetRoleCommand
    { }
}

