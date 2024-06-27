using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using JS.Abp.RulesEngine.RulesMembers;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public class RulesMembersDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IRulesMemberRepository _rulesMemberRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RulesGroupsDataSeedContributor _rulesGroupsDataSeedContributor;

        private readonly RulesDataSeedContributor _rulesDataSeedContributor;

        public RulesMembersDataSeedContributor(IRulesMemberRepository rulesMemberRepository, IUnitOfWorkManager unitOfWorkManager, RulesGroupsDataSeedContributor rulesGroupsDataSeedContributor, RulesDataSeedContributor rulesDataSeedContributor)
        {
            _rulesMemberRepository = rulesMemberRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _rulesGroupsDataSeedContributor = rulesGroupsDataSeedContributor; _rulesDataSeedContributor = rulesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _rulesGroupsDataSeedContributor.SeedAsync(context);
            await _rulesDataSeedContributor.SeedAsync(context);

            await _rulesMemberRepository.InsertAsync(new RulesMember
            (
                id: Guid.Parse("f8402a52-f7d4-4f69-87ae-bb5df23f2348"),
                sequence: 558198274,
                description: "15204faafddb41b3b9fe23a4ab383b0623952a07039c47349941c05178d23295d2eacf3052294bc18476e9fec5",
                rulesGroupId: null,
                ruleId: null
            ));

            await _rulesMemberRepository.InsertAsync(new RulesMember
            (
                id: Guid.Parse("b42abf30-a721-493c-bcc5-3bf8c9316716"),
                sequence: 1023081237,
                description: "449631a835ba4d9e8fb4d87771f4774c5025daaf4251499cbb61b40b91edca21c1853f3dc",
                rulesGroupId: null,
                ruleId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}