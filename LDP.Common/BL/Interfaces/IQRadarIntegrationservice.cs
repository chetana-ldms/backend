using LDP_APIs.Models;

namespace LDP_APIs.Interfaces
{
    public interface IQRadarIntegrationservice
    {
        Task<getOffenseResponse> Getoffenses(GetOffenseDTO dto);
    }
}
