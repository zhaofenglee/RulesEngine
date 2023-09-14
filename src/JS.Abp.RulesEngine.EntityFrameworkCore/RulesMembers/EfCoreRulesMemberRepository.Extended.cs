using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using JS.Abp.RulesEngine.EntityFrameworkCore;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public class EfCoreRulesMemberRepository : EfCoreRulesMemberRepositoryBase
    {
        public EfCoreRulesMemberRepository(IDbContextProvider<RulesEngineDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}