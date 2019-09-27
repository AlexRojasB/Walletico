using System;
using System.Collections.Generic;
using System.Text;

namespace Walletico.MapBoxResponse
{
    public class MapBoxResponse
    {
        public string Type { get; set; }
        public dynamic Query { get; set; }
        public List<Feature> Features { get; set; }
        public string Attribution { get; set; }
    }
}
