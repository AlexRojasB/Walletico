using System;
using Walletico.Models.Base;

namespace Walletico.Models
{
    public class Transaction : SelectableModel
    {
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Amount { get; set; }
        public byte TransType { get; set; }
        public Category Category { get; set; }
        public Geoplace Location { get; set; }
    }
}
