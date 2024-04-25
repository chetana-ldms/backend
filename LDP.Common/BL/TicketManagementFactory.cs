using AutoMapper;
using LDP_APIs.Interfaces;
using LDP_APIs.Services;

namespace LDP_APIs.BL.Factories
{
    public class TicketManagementFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IMapper _mapper;

        public TicketManagementFactory(IServiceProvider serviceProvider , IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;

        }

     
        //p//ublic  IIncidentManagementService? Instance;

        private IIncidentManagementService Instance = null;

        public  IIncidentManagementService GetInstance(int ToolID)
        {
            //if (Instance != null) 
            //{ 
            //    return Instance;
            //}
            switch(ToolID)
            {
                case (2):
                    Instance = new FreshDesk_IncidentManagementService(_mapper);

                   //Instance=(IIncidentManagementService)_serviceProvider.GetService(typeof(FreshDesk_IncidentManagementService));
                   //Instance= _serviceProvider.GetRequiredService<IIncidentManagementService>();
                    break;
            }
            return Instance;
        }
    }
}
