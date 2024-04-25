namespace LDP.Common.Services.DrataIntegration
{
    public interface IDrataIntegrationService
    {
        Task<GetControlsResponse> GetControls(GetControlsRequest request);

    }
}
