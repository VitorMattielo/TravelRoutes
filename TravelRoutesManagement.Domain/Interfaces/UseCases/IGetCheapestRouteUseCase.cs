namespace TravelRoutesManagement.Domain.Interfaces.UseCases
{
    public interface IGetCheapestRouteUseCase
    {
        Task<string> GetCheapestRoute(int idOrigin, int idDestination);
    }
}
