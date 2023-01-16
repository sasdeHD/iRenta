using System.ComponentModel.DataAnnotations.Schema;

namespace Test_iRenta.Data.Models.EntityModel
{
    public class Product
    {
        public sbyte Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
