using Nubex_Models;

namespace Nubex_Client.Service.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetAll();
        public Task<ProductDTO> GetById(int productId);
    }
}
