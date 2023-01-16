using Test_iRenta.Data.Models.Enums;

namespace Test_iRenta.Data.Models.ViewModel
{
    public class OrderModel
    {
        public short Id { get; set; }
        public string ClientName { get; set; }
        public StateOrder StateOrder { get; set; }
        public string Response { get; set; }

        public List<OrderProducts> Products { get; set; }
    }
    public class OrderProducts
    {
        public byte Id { get; set; }
        public byte Count { get; set; }
    }
}
