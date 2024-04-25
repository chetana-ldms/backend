using LDP_APIs.Interfaces;
using LDP_APIs.Services;

namespace LDP_APIs.BL.Factories
{
    public class TicketManagementFactory
    {
        private TicketManagementFactory()
        {

        }
        private static IIncidentManagementService? Instance;

        public static IIncidentManagementService GetInstance(int ToolID)
        {
            switch(ToolID)
            {
                case (2):
                    Instance = new FreshDesk_IncidentManagementService();
                    break;
            }
            return Instance;
        }
    }
}
