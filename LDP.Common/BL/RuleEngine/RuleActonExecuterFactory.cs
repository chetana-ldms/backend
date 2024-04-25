namespace LDPRuleEngine.BL.Framework.Actions
{
    public class RuleActonExecuterFactory
    {

        private readonly IServiceProvider _serviceProvider;

        public   RuleActonExecuterFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
       private static baseRuleActionExecuter Instance = null;

        public baseRuleActionExecuter GetInstance(int ToolTypeID)
        {
            switch (ToolTypeID)
            {
                case (30):
                    Instance=(baseRuleActionExecuter)_serviceProvider.GetService(typeof(TicketManagementRuleActionExecuter));
                    break;


            }
            return Instance;

           // return Instance;
        }



    }
}
