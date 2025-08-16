using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IProductService
    {
        public PaginationModel<Product> ListProducts(int page);
    }
}
