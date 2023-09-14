using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public partial interface IRulesMemberRepository : IRepository<RulesMember, Guid>
    {
        Task<RulesMemberWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<RulesMemberWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string? description = null,
            Guid? rulesGroupId = null,
            Guid? ruleId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<RulesMember>> GetListAsync(
                    string? filterText = null,
                    int? sequenceMin = null,
                    int? sequenceMax = null,
                    string? description = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string? description = null,
            Guid? rulesGroupId = null,
            Guid? ruleId = null,
            CancellationToken cancellationToken = default);
    }
}