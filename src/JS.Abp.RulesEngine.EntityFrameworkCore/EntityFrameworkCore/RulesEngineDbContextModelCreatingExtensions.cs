using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.RulesGroups;
using Volo.Abp.EntityFrameworkCore.Modeling;
using JS.Abp.RulesEngine.Rules;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace JS.Abp.RulesEngine.EntityFrameworkCore;

public static class RulesEngineDbContextModelCreatingExtensions
{
    public static void ConfigureRulesEngine(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(RulesEngineDbProperties.DbTablePrefix + "Questions", RulesEngineDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        builder.Entity<Rule>(b =>
    {
        b.ToTable(RulesEngineDbProperties.DbTablePrefix + "Rules", RulesEngineDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Rule.TenantId));
        b.Property(x => x.RuleCode).HasColumnName(nameof(Rule.RuleCode)).IsRequired().HasMaxLength(RuleConsts.RuleCodeMaxLength);
        b.Property(x => x.SuccessEvent).HasColumnName(nameof(Rule.SuccessEvent)).IsRequired().HasMaxLength(RuleConsts.SuccessEventMaxLength);
        b.Property(x => x.ErrorMessage).HasColumnName(nameof(Rule.ErrorMessage)).IsRequired().HasMaxLength(RuleConsts.ErrorMessageMaxLength);
        b.Property(x => x.ErrorType).HasColumnName(nameof(Rule.ErrorType));
        b.Property(x => x.RuleExpressionType).HasColumnName(nameof(Rule.RuleExpressionType));
        b.Property(x => x.Expression).HasColumnName(nameof(Rule.Expression));
        b.Property(x => x.Description).HasColumnName(nameof(Rule.Description));
    });
        builder.Entity<RulesGroup>(b =>
    {
        b.ToTable(RulesEngineDbProperties.DbTablePrefix + "RulesGroups", RulesEngineDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(RulesGroup.TenantId));
        b.Property(x => x.GroupName).HasColumnName(nameof(RulesGroup.GroupName)).IsRequired().HasMaxLength(RulesGroupConsts.GroupNameMaxLength);
        b.Property(x => x.OperatorType).HasColumnName(nameof(RulesGroup.OperatorType));
        b.Property(x => x.Description).HasColumnName(nameof(RulesGroup.Description));
    });
        builder.Entity<RulesMember>(b =>
    {
        b.ToTable(RulesEngineDbProperties.DbTablePrefix + "RulesMembers", RulesEngineDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(RulesMember.TenantId));
        b.Property(x => x.Sequence).HasColumnName(nameof(RulesMember.Sequence));
        b.Property(x => x.Description).HasColumnName(nameof(RulesMember.Description));
        b.HasOne<RulesGroup>().WithMany().HasForeignKey(x => x.RulesGroupId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Rule>().WithMany().HasForeignKey(x => x.RuleId).OnDelete(DeleteBehavior.NoAction);
    });
    }
}