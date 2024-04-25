namespace LDP.Common.Services.DrataIntegration
{
    public class DrataOperationsBL : IDrataOperationsBL
    {
        IDrataIntegrationService _service;
        public DrataOperationsBL(IDrataIntegrationService service)
        {
            _service = service;
        }

        public GetControlsResponse GetControls(GetControlsRequest request)
        {
           return  _service.GetControls(request).Result;
        }
    }
}
