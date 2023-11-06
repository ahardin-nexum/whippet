﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet.Logging.Serilog.ServiceManagers.Queries;

namespace Athi.Whippet.Logging.Serilog.ServiceManagers.Handlers.Queries
{
    /// <summary>
    /// Query handler for <see cref="GetEarliestSerilogLogEntryQuery"/> objects.
    /// </summary>
    public class GetEarliestSerilogLogEntryQueryHandler : SerilogLogEntryQueryHandlerBase<GetEarliestSerilogLogEntryQuery>, ISerilogLogEntryQueryHandler<GetEarliestSerilogLogEntryQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEarliestSerilogLogEntryQueryHandler"/> class with the specified <see cref="IWhippetQueryRepository{TEntity}"/>.
        /// </summary>
        /// <param name="repository"><see cref="IWhippetQueryRepository{TEntity}"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public GetEarliestSerilogLogEntryQueryHandler(IWhippetQueryRepository<SerilogLogEntry> repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified query asynchronously.
        /// </summary>
        /// <param name="query"><see cref="IWhippetQuery{TEntity}"/> object to handle.</param>
        /// <returns><see cref="WhippetResultContainer{T}"/> object.</returns>
        /// <exception cref="ArgumentNullException" />
        public override async Task<WhippetResultContainer<IEnumerable<SerilogLogEntry>>> HandleAsync(GetEarliestSerilogLogEntryQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            else
            {
                WhippetResultContainer<SerilogLogEntry> queryResult = await Repository.GetEarliestEntryAsync();
                return queryResult.ToEnumerableResult();
            }
        }
    }
}
