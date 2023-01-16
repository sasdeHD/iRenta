using System.ComponentModel.DataAnnotations.Schema;

namespace Test_iRenta.Data.Models.EntityModel
{
    public class OrderProduct
    {
        public byte Id { get; set; }
        public short OrderId { get; set; }
        public sbyte ProductId { get; set; }
        public byte Count { get; set; }


        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
