using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrsCleanCapstone.Data;

namespace MrsCleanCapstone.Models
{
    public class ProductRepository : InterfaceProductRepo
    {
        private ProductDbContext context;

        public ProductRepository(ProductDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;
    }
}
