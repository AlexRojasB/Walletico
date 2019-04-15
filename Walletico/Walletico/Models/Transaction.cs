using System;
using Walletico.Models.Base;

namespace Walletico.Models
{
    public class Transaction : SelectableModel
    {
        public Category Category { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public byte TransType { get; set; }
    }
}
