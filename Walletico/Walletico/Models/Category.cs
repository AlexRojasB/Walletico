using Walletico.Models.Base;

namespace Walletico.Models
{
    public class Category : BaseModel
    {
        public string Description { get; set; }
        public string IconCode { get; set; }
        public byte TransType { get; set; }

    }
}
