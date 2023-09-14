using JS.Abp.RulesEngine.OperatorTypes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace JS.Abp.RulesEngine.RulesGroups
{
    public partial interface IRulesGroupRepository : IRepository<RulesGroup, Guid>
    {
        Task<List<RulesGroup>> GetListAsync(
            string? filterText = null,
            string? groupName = null,
            OperatorType? operatorType = null,
            string? description = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? groupName = null,
            OperatorType? operatorType = null,
            string? description = null,
            CancellationToken cancellationToken = default);
    }
}