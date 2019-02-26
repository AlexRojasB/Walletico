using System;
using Walletico.Models.Base;

namespace Walletico.Models
{
    public class Transaction : SelectableModelBase
    {

        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Amount { get; set; }
    }
}
