using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ProductService : BasePaginateDataService<Product>
    {
        private readonly TestDbContext _context;

        public ProductService(TestDbContext dbContext)
        {
            _context = dbContext;
        }

        public PaginationModel<Product> ListProducts(int page)
        {
            //Considerando que a primeira pagina seja a 1
            //Busca 11 itens para evitar realizar count no banco causando possível sobrecarga
            var products = _context.Products.Skip((page - 1) * 10).Take(11).ToList();

            return CreatePaginatedResponse(products);
        }

    }
}
