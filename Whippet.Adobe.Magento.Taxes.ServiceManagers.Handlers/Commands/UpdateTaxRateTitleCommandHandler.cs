﻿using System;
using Athi.Whippet.Data;
using Athi.Whippet.Adobe.Magento.Taxes.ServiceManagers.Commands;
using Athi.Whippet.Adobe.Magento.Taxes.Repositories;

namespace Athi.Whippet.Adobe.Magento.Taxes.ServiceManagers.Handlers.Commands
{
    /// <summary>
    /// Command handler for <see cref="UpdateTaxRateTitleCommand"/> objects.
    /// </summary>
    public class UpdateTaxRateTitleCommandHandler : TaxRateTitleCommandHandlerBase<UpdateTaxRateTitleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTaxRateTitleCommandHandler"/> class with the specified <see cref="ITaxRateTitleRepository"/> object.
        /// </summary>
        /// <param name="repository"><see cref="ITaxRateTitleRepository"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public UpdateTaxRateTitleCommandHandler(ITaxRateTitleRepository repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified command asynchronously.
        /// </summary>
        /// <param name="command"><see cref="ITaxRateTitleCommand"/> object to handle.</param>
        /// <returns><see cref="WhippetResult"/> which contains the result of the command.</returns>
        /// <exception cref="ArgumentNullException" />
        public override async Task<WhippetResult> HandleAsync(UpdateTaxRateTitleCommand command)
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
                    result = await ((IWhippetRepository<TaxRateTitle, int>)(Repository)).UpdateAsync(command.TaxRateTitle);
                }

                return result;
            }
        }

        /// <summary>
        /// Validates the specified <see cref="UpdateTaxRateTitleCommand"/> object.
        /// </summary>
        /// <param name="command"><see cref="UpdateTaxRateTitleCommand"/> object to validate.</param>
        /// <returns><see cref="WhippetResult"/> object.</returns>
        protected override WhippetResult Validate(UpdateTaxRateTitleCommand command)
        {
            WhippetResult result = WhippetResult.Success;

            if (command == null || command.TaxRateTitle == null)
            {
                result = new WhippetResult(new ArgumentNullException(nameof(command)));
            }

            return result;
        }
    }
}