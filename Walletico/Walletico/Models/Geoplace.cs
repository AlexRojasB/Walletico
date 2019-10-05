using Walletico.Models.Base;
using Walletico.Shared.BoundaryHelper;

namespace Walletico.Models
{
    public class Geoplace : SelectableModel
    {
        public string PlaceName { get; set; }
        public MapPoint Coordenates { get; set; }
    }
}
