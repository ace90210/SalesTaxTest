using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SalesTaxServices.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal  Price { get; set; }

        public bool Imported { get; set; }

        public int Type { get; set; }
    }
}
