using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Walletico.MapBoxResponse;
using Walletico.Shared;
using Walletico.Shared.BoundaryHelper;

namespace Walletico.Service
{
    public class MapService : IMapService
    {
        public async Task<IEnumerable<Feature>> GetPlacesNearby(MapPoint location, double distance)
        {
            var box = BoundaryHelper.GetBoundingBox(new MapPoint { Latitude = location.Latitude, Longitude = location.Longitude }, distance);
            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };
            string url = $"https://api.mapbox.com/geocoding/v5/mapbox.places/restaurant.json?bbox={box.MinPoint.Longitude.ToString("N", nfi)},{box.MinPoint.Latitude.ToString("N", nfi)},{box.MaxPoint.Longitude.ToString("N", nfi)},{box.MaxPoint.Latitude.ToString("N", nfi)}&access_token=pk.eyJ1IjoiemFva3kiLCJhIjoiY2sxMWI0NnBtMDVnczNtcjZ2OW84ajg1MyJ9.2llZQl32ShwZlKnG-9jb3A";
            var httpResponse = await HttpClientSingleton.HttpClient.GetAsync(url);
            var httpContent = await httpResponse.Content.ReadAsStringAsync();
            var mapBoxResponse= JsonConvert.DeserializeObject<MapBoxResponse.MapBoxResponse>(httpContent);
            return mapBoxResponse.Features;
        }
    }
}
