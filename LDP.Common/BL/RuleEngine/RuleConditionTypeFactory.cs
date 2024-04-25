using LDPRuleEngine.BL.Interfaces;
using static LDPRuleEngine.BL.Framework.Constants;

namespace LDPRuleEngine.BL.Framework
{
    public class RuleConditionTypeFactory
    {
        private RuleConditionTypeFactory()
        {

        }
       public static IRuleConditionTypeExecuter GetInstance(RuleConditionTypes ruleConditionTypevalue )
        {
            IRuleConditionTypeExecuter instance = null;

            switch (ruleConditionTypevalue)
            {
                case RuleConditionTypes.TextContainRuleType:
                {
                    instance = new TextContainsRuleTypeExecuter();
                    break;
                }
                case RuleConditionTypes.RegexPatternRuleType:
                    {
                        instance = new RegexPatternsRuleTypeExecuter();
                        break;
                    }
                case RuleConditionTypes.EqualsRuleType:
                    {
                        instance = new EqualsRuleTypeExecuter();
                        break;
                    }
                default:
                    break;

            }

            return instance;
        }
    }
}
