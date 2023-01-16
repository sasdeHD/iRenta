using Microsoft.EntityFrameworkCore;
using Test_iRenta.Data.Interfaces;
using Test_iRenta.Data.Models.EntityModel;
using WorldMafia.Infrastructure;

namespace Test_iRenta.Data.Service
{
    public class ProductService : IProducts
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> Goods()
        {
            return await context.Products.AsNoTracking().ToListAsync();
        }
    }
}
