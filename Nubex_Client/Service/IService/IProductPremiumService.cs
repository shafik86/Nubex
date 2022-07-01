using Nubex_Models;

namespace Nubex_Client.Service.IService
{
    public interface IProductPremiumService
    {
        public Task<IEnumerable<ProductPremiumDTO>> GetAll(int Id);

    }
}
