using Test_iRenta.Data.Models.Enums;

namespace Test_iRenta.Data.Models.EntityModel
{
    public class Order
    {
        public short Id { get; set; }
        public string ClientName { get; set; }
        public StateOrder StateOrder { get; set; }
        public DateTime Created { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}
