using JS.Abp.RulesEngine.OperatorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace JS.Abp.RulesEngine.RulesGroups
{
    public abstract class RulesGroupManagerBase : DomainService
    {
        protected IRulesGroupRepository _rulesGroupRepository;

        public RulesGroupManagerBase(IRulesGroupRepository rulesGroupRepository)
        {
            _rulesGroupRepository = rulesGroupRepository;
        }

        public virtual async Task<RulesGroup> CreateAsync(
        string groupName, OperatorType operatorType, string description)
        {
            Check.NotNullOrWhiteSpace(groupName, nameof(groupName));
            Check.Length(groupName, nameof(groupName), RulesGroupConsts.GroupNameMaxLength);
            Check.NotNull(operatorType, nameof(operatorType));

            var rulesGroup = new RulesGroup(
             GuidGenerator.Create(),
             groupName, operatorType, description
             );

            return await _rulesGroupRepository.InsertAsync(rulesGroup);
        }

        public virtual async Task<RulesGroup> UpdateAsync(
            Guid id,
            string groupName, OperatorType operatorType, string description, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(groupName, nameof(groupName));
            Check.Length(groupName, nameof(groupName), RulesGroupConsts.GroupNameMaxLength);
            Check.NotNull(operatorType, nameof(operatorType));

            var rulesGroup = await _rulesGroupRepository.GetAsync(id);

            rulesGroup.GroupName = groupName;
            rulesGroup.OperatorType = operatorType;
            rulesGroup.Description = description;

            rulesGroup.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _rulesGroupRepository.UpdateAsync(rulesGroup);
        }

    }
}