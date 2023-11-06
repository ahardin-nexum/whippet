﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Data.CQRS;

namespace Athi.Whippet.Localization.Addressing.ServiceManagers.Commands
{
    /// <summary>
    /// Command that deletes an existing <see cref="PostalCode"/> object in the data store. This class cannot be inherited.
    /// </summary>
    public sealed class DeletePostalCodeCommand : PostalCodeCommandBase, IWhippetCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePostalCodeCommand"/> class with no arguments.
        /// </summary>
        private DeletePostalCodeCommand()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePostalCodeCommand"/> class with the specified <see cref="PostalCode"/>.
        /// </summary>
        /// <param name="postalCode"><see cref="PostalCode"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DeletePostalCodeCommand(PostalCode postalCode)
            : base(postalCode)
        {
            if (postalCode == null)
            {
                throw new ArgumentNullException(nameof(postalCode));
            }
        }
    }
}
