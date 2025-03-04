﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Data.CQRS;

namespace Athi.Whippet.Security.ServiceManagers.Commands
{
    /// <summary>
    /// Represents a command that acts upon <see cref="IWhippetUser"/> objects.
    /// </summary>
    public interface IWhippetUserCommand : IWhippetCommand
    {
        /// <summary>
        /// Gets the <see cref="IWhippetUser"/> instance to create or act upon in the data store. This property is read-only.
        /// </summary>
        IWhippetUser User
        { get; }
    }
}
