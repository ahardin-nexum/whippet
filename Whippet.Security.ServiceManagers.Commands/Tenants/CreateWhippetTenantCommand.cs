﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Security.Tenants;

namespace Athi.Whippet.Security.Tenants.ServiceManagers.Commands
{
    /// <summary>
    /// Command for creating new <see cref="WhippetTenant"/> objects in the data store. This class cannot be inherited.
    /// </summary>
    public sealed class CreateWhippetTenantCommand : WhippetTenantCommandBase, IWhippetTenantCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateWhippetTenantCommand"/> class with the specified <see cref="WhippetTenant"/> object.
        /// </summary>
        /// <param name="tenant"><see cref="WhippetTenant"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public CreateWhippetTenantCommand(WhippetTenant tenant)
            : base(tenant)
        { }
    }
}
