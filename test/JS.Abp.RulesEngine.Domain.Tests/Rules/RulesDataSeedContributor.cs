using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using JS.Abp.RulesEngine.Rules;

namespace JS.Abp.RulesEngine.Rules
{
    public class RulesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IRuleRepository _ruleRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public RulesDataSeedContributor(IRuleRepository ruleRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _ruleRepository = ruleRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _ruleRepository.InsertAsync(new Rule
            (
                id: Guid.Parse("60342e5a-dc76-47a3-9cb1-8a1eb19341cb"),
                ruleCode: "69747db33e3d424aa65964940d1afd97d3139a61f6d84b3bb88bfb459f16562f033ef90b48184c278633791417c19dd090f629b0bcd7469f9a75b7254ce7d9c8",
                successEvent: "ae018d211e39465aa851f6e3576d3fd74eae4b0696674b4dbf1cf37ac231be2f3e7932fe731d45a3af7134d2e88a5b0473e6b500132d491fb751fe6af367840d",
                errorMessage: "ce0ca056ba1c4c74bc614bd8b6975fe4a13e92941cb14fe38b5cff7bac98f2538616724344ec4769941a5f3b684d4954a5d96820751d428bb47d9d671ad2dcb6548e8e89dcd24ce19582b40e9048bd54954ac6eb90b34af79c4e4d06efc6f5685deed753ced64067827bc24c6cb6c566e62e3b7efe034303a19671ae052150eea757c92fc6f9488396e2a5971695b723e6f1f26e0f394dd3a9cd8c7437444d976a35c8ddfc31459e9e867dc5d3675f9d82a46d4c4f1e47ab813c50c9518a7f60a8e9864348fe4dcf8b69297c57d52553ddb4476955a34fa8a9fe04de8ea24f3de3a3487da4694e418fd7541dc6c197b9553a4e9fc0df440c9eade44813aa3105",
                errorType: default,
                ruleExpressionType: default,
                expression: "90ef5c5",
                description: "68844d3c3b3b"
            ));

            await _ruleRepository.InsertAsync(new Rule
            (
                id: Guid.Parse("4e3b6ec6-1ed3-4e5c-a057-d863159f7cb2"),
                ruleCode: "a303313313c24143aa5cad785bf3752f55b47ad5b3f84cc1a5be43b75956e480a64222aaf9ba48cead08680267754d6558b8bf202d1f45a9bc561eb5e88e765a",
                successEvent: "9ef14ced8e3345e38d9a336dda3aeb762923e864b58a4a16b49acaf12ae3e7f8a4e08eddc9314e609ad4169338ab8384e049ba1be71344d5b06027724a629e1e",
                errorMessage: "dc4821421af948a4953dbefe7c3d284472492ea29cc7493fb52ad0e663162ed57b948392154b434ca29846aceb13a670111383ea33d24a979f891e482c7294a7e1839b5b575743e7b66f776b3bd7e128aa0b14ffa6354dca9a40b4a5ad9ad6c3d842267f485547969b0edd54b259b5647c7fd37b7f8d42fabd8a135c845736855cddb848dc564e4aae2250bcd4ec6e1f7068fe95c494433d938290f44f70c6a208f31d203ab34463b220aff1788490028e5c48a8abae47cb906331db43dd9d155407fd7321794d72935bd22e958c7cb55fac006c5ba64da98ed0fec7e93dd145ced804777b7e4b03a7630eec5a250315d298b2a3982340a9ae2779dac9027b48",
                errorType: default,
                ruleExpressionType: default,
                expression: "2ce7913f56e1481ba67b10bf2eef7b19dd8a1e7fc87c4d55",
                description: "2d475fcff0a6432491c5671a4f"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}