using System.Collections.Generic;

namespace Walletico.MapBoxResponse
{
    public class Feature
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public List<string> Place_type { get; set; }
        public int Relevance { get; set; }
        public Properties Properties { get; set; }
        public string Text { get; set; }
        public string Place_name { get; set; }
        public List<double> Center { get; set; }
        public Geometry Geometry { get; set; }
        public List<Context> Context { get; set; }
        public List<double?> Bbox { get; set; }
    }
}