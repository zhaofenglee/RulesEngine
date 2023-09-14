using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using JS.Abp.RulesEngine.RulesGroups;

namespace JS.Abp.RulesEngine.RulesGroups
{
    public class RulesGroupsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IRulesGroupRepository _rulesGroupRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public RulesGroupsDataSeedContributor(IRulesGroupRepository rulesGroupRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _rulesGroupRepository = rulesGroupRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _rulesGroupRepository.InsertAsync(new RulesGroup
            (
                id: Guid.Parse("15d16d9c-bb47-40e7-ad2d-60075e6c8b11"),
                groupName: "8494fd62e3034b47886008ddd887dcd530f9149091fa420bb495ba9331e94e61869773582a3145be92156d36a26efb69c247722db4344339a2b71c4d1e49c513",
                operatorType: default,
                description: "4f82387eb1554255a8d6469d53b3abc67abeabf839e9437ba069b2f8c726ba6556"
            ));

            await _rulesGroupRepository.InsertAsync(new RulesGroup
            (
                id: Guid.Parse("8e6af1ef-07c5-493f-b727-d5b2ac81c10c"),
                groupName: "51a5bd9c44304df182a6aecfe4942823695e14bc93de4ca5b9f0028ce8d7cd28c7e51ce679bc42dcbf44e956e9a2e1a6c55fa7d9afda4279abf7f9a3c1cf9a3e",
                operatorType: default,
                description: "c1d7737e8cf3439080"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}