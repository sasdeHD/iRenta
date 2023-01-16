using Test_iRenta.Data.Models.EntityModel;
using Test_iRenta.Data.Models.ViewModel;

namespace Test_iRenta.Data.Interfaces
{
    public interface IOrders
    {
        public Task<List<Order>> GetOrders();
        public Task<List<Order>> GetOrders(DateTime date);
        public Task<Order> GetOrder(int id);
        public Task<string> CreateOrder(Order order);
        public Task<string> EdtiOrder(Order order);
        public Task<bool> DeleteOrder(int id);
    }
}
