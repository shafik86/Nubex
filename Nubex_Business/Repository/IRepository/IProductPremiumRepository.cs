using Nubex_Models;


namespace Nubex_Business.Repository.IRepository
{
    public interface IProductPremiumRepository
    {
        public Task<ProductPremiumDTO> Create(ProductPremiumDTO objDTO);
        public Task<ProductPremiumDTO> Update(ProductPremiumDTO objDTO);
        public Task<int> Delete(int id);

        public Task<ProductPremiumDTO> GetById(int id);
        public Task<IEnumerable<ProductPremiumDTO>> GetAll(int? id=null);
    }
}
