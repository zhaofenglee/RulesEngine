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

namespace JS.Abp.RulesEngine.RulesGroups
{
    public class EfCoreRulesGroupRepository : EfCoreRulesGroupRepositoryBase
    {
        public EfCoreRulesGroupRepository(IDbContextProvider<RulesEngineDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}