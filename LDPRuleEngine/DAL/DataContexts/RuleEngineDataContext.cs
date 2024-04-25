using LDPRuleEngine.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LDPRuleEngine.DAL.DataContexts
{
    public class RuleEngineDataContext : DbContext
    {
        public RuleEngineDataContext(DbContextOptions<RuleEngineDataContext> options) : base(options)
        {
        }
        public DbSet<LDPRule>? vm_Rules { get; set; }
        public DbSet<RuleCondition>? vm_Rule_Conditions { get; set; }
        public DbSet<RuleConditionValue>? vm_Rules_Condition_Valuess { get; set; }
        public DbSet<RulesEngineMasterData>? vm_RulesEngine_Masterdata { get; set; }
        public DbSet<RuleAction>? vm_rule_Actions { get; set; }

        public DbSet<PlayBook>? vm_Play_Books { get; set; }

        public DbSet<PlayBookDtl>? vm_Play_Book_dtls { get; set; }

    }
}
