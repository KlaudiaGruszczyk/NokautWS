using Nokaut.Models;

namespace Nokaut.Service
{
    public interface IProductListScrapper
    {
        Task<ICollection<ProductModel>> GetProducts(string queryPhrase, CancellationToken cancellationToken = default);
    }
}