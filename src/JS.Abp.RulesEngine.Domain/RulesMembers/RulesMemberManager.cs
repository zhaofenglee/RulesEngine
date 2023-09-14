using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public abstract class RulesMemberManagerBase : DomainService
    {
        protected IRulesMemberRepository _rulesMemberRepository;

        public RulesMemberManagerBase(IRulesMemberRepository rulesMemberRepository)
        {
            _rulesMemberRepository = rulesMemberRepository;
        }

        public virtual async Task<RulesMember> CreateAsync(
        Guid? rulesGroupId, Guid? ruleId, int sequence, string description)
        {

            var rulesMember = new RulesMember(
             GuidGenerator.Create(),
             rulesGroupId, ruleId, sequence, description
             );

            return await _rulesMemberRepository.InsertAsync(rulesMember);
        }

        public virtual async Task<RulesMember> UpdateAsync(
            Guid id,
            Guid? rulesGroupId, Guid? ruleId, int sequence, string description, [CanBeNull] string? concurrencyStamp = null
        )
        {

            var rulesMember = await _rulesMemberRepository.GetAsync(id);

            rulesMember.RulesGroupId = rulesGroupId;
            rulesMember.RuleId = ruleId;
            rulesMember.Sequence = sequence;
            rulesMember.Description = description;

            rulesMember.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _rulesMemberRepository.UpdateAsync(rulesMember);
        }

    }
}