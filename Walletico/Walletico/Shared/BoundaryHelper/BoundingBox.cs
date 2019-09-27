using static Walletico.Shared.BoundaryHelper.BoundaryHelper;

namespace Walletico.Shared.BoundaryHelper
{
    public class BoundingBox
    {
        public MapPoint MinPoint { get; set; }
        public MapPoint MaxPoint { get; set; }
    }

}
