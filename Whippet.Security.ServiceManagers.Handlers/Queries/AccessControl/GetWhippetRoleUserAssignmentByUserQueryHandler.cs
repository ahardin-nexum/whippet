﻿using System;
using Athi.Whippet.Data.CQRS;
using Athi.Whippet.Security.AccessControl.ServiceManagers.Queries;

namespace Athi.Whippet.Security.AccessControl.ServiceManagers.Handlers.Queries
{
    /// <summary>
    /// Query handler for <see cref="GetWhippetRoleUserAssignmentByUserQuery"/> objects.
    /// </summary>
    public class GetWhippetRoleUserAssignmentByUserQueryHandler : WhippetRoleUserAssignmentQueryHandlerBase<GetWhippetRoleUserAssignmentByUserQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetWhippetRoleUserAssignmentByUserQueryHandler"/> class with the specified <see cref="IWhippetQueryRepository{TEntity}"/>.
        /// </summary>
        /// <param name="repository"><see cref="IWhippetQueryRepository{TEntity}"/> object to initialize with.</param>
        /// <exception cref="ArgumentNullException" />
        public GetWhippetRoleUserAssignmentByUserQueryHandler(IWhippetQueryRepository<WhippetRoleUserAssignment> repository)
            : base(repository)
        { }

        /// <summary>
        /// Handles the specified query asynchronously.
        /// </summary>
        /// <param name="query"><see cref="IWhippetQuery{TEntity}"/> object to handle.</param>
        /// <returns><see cref="WhippetResultContainer{T}"/> object.</returns>
        /// <exception cref="ArgumentNullException" />
        public override async Task<WhippetResultContainer<IEnumerable<WhippetRoleUserAssignment>>> HandleAsync(GetWhippetRoleUserAssignmentByUserQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            else
            {
                WhippetResultContainer<IEnumerable<WhippetRoleUserAssignment>> result = await Repository.GetByUserAsync(query.User);
                return new WhippetResultContainer<IEnumerable<WhippetRoleUserAssignment>>(result.Result, result.Item);
            }
        }
    }
}
