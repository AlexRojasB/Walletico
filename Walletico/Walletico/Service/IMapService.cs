using System.Collections.Generic;
using System.Threading.Tasks;
using Walletico.MapBoxResponse;
using Walletico.Shared.BoundaryHelper;

namespace Walletico.Service
{
    public interface IMapService
    {
        Task<IEnumerable<Feature>> GetPlacesNearby(MapPoint location, double distance);
    }
}
