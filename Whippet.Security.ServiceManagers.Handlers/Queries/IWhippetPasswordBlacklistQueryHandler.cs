﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Athi.Whippet.Data.CQRS;

namespace Athi.Whippet.Security.ServiceManagers.Handlers.Queries
{
    /// <summary>
    /// Handles queries against <see cref="WhippetPasswordBlacklist"/> objects.
    /// </summary>
    /// <typeparam name="TQuery"><see cref="IWhippetQuery{TEntity}"/> type to handle.</typeparam>
    public interface IWhippetPasswordBlacklistQueryHandler<TQuery> : IWhippetQueryHandler<TQuery, WhippetPasswordBlacklist> where TQuery : IWhippetQuery<WhippetPasswordBlacklist>
    { }
}
