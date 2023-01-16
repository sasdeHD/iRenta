using Test_iRenta.Data.Models.EntityModel;

namespace Test_iRenta.Data.Interfaces
{
    public interface IProducts
    {
        public Task<List<Product>> Goods();
        public Task<Product> GetProduct(int id);
    }
}
