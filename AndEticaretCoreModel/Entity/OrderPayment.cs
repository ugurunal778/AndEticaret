using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndEticaretCoreModel.Entity
{
    public class OrderPayment : EntityBase
    {
        public int OrderId { get; set; }
        public int OrderType { get; set; }
        public decimal Price { get; set; }
        public string Bank { get; set; }
    }
    public enum OrderType
    {
        Havale = 0,
        KrediKArtı = 1
    }
}
