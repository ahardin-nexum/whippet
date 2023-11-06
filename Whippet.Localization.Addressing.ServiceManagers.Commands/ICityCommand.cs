﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Data.CQRS;

namespace Athi.Whippet.Localization.Addressing.ServiceManagers.Commands
{
    /// <summary>
    /// Provides support for all commands that act upon <see cref="ICity"/> objects.
    /// </summary>
    public interface ICityCommand : IWhippetCommand
    {
        /// <summary>
        /// Gets the <see cref="ICity"/> instance to create or act upon in the data store. This property is read-only.
        /// </summary>
        ICity City
        { get; }
    }
}
