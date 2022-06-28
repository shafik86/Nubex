using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nubex_Business.Repository.IRepository;
using Nubex_DataAccess;
using Nubex_DataAccess.Data;
using Nubex_Models;

namespace Nubex_Business.Repository
{
    public class ProductPremiumRepository : IProductPremiumRepository
    {

        public ProductPremiumRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public ApplicationDbContext applicationDbContext { get; }
        public IMapper _mapper { get; set; }
        public async Task<ProductPremiumDTO> Create(ProductPremiumDTO objDTO)
        {
            var product = _mapper.Map<ProductPremiumDTO, ProductPremium>(objDTO);
            
            var obj = applicationDbContext.ProductPremiums.Add(product);
            await applicationDbContext.SaveChangesAsync();

            return _mapper.Map<ProductPremium, ProductPremiumDTO>(obj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var result = await applicationDbContext.ProductPremiums.FirstOrDefaultAsync(c => c.Id == id);
            if (result != null)
            {
                applicationDbContext.ProductPremiums.Remove(result);
                return await applicationDbContext.SaveChangesAsync();
            }
            return 0;

        }

        public async Task<IEnumerable<ProductPremiumDTO>> GetAll(int? id=null)
        {
            if (id!=null && id>0)
            {
                return _mapper.Map<IEnumerable<ProductPremium>, IEnumerable<ProductPremiumDTO>>
                    (applicationDbContext.ProductPremiums.Where(p=> p.ProductId == id));
            }
            else
            {
                return _mapper.Map<IEnumerable<ProductPremium>, IEnumerable<ProductPremiumDTO>>(applicationDbContext.ProductPremiums);
            }

        }

        public async Task<ProductPremiumDTO> GetById(int id)
        {
            var result = await applicationDbContext.ProductPremiums.FirstOrDefaultAsync(c => c.Id == id);
            if (result != null)
            {
                return _mapper.Map<ProductPremium, ProductPremiumDTO>(result);
            }
            return new ProductPremiumDTO();
        }

        public async Task<ProductPremiumDTO> Update(ProductPremiumDTO ProductPremiumDTO)
        {
            var result = applicationDbContext.ProductPremiums.FirstOrDefault(c => c.Id == ProductPremiumDTO.Id);
            if (result != null)
            {
                result.Price = ProductPremiumDTO.Price;
                //result.Size = ProductPremiumDTO.Size;
                result.Id = ProductPremiumDTO.Id;
                applicationDbContext.Update(result);
                applicationDbContext.SaveChanges();
                return _mapper.Map<ProductPremium, ProductPremiumDTO>(result);
            }
            return ProductPremiumDTO;
        }
    }
}
