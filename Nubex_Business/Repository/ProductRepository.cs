using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nubex_Business.Repository.IRepository;
using Nubex_DataAccess;
using Nubex_DataAccess.Data;
using Nubex_Models;

namespace Nubex_Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext, IMapper mapper )
        {
            DbContext = dbContext;
            _mapper = mapper;
        }

        public ApplicationDbContext DbContext { get; }
        public IMapper _mapper { get; set; }
        public async Task<ProductDTO> Create(ProductDTO objDTO)
        {
            var product = _mapper.Map<ProductDTO, Product>(objDTO);
            try
            {
            var obj = DbContext.Products.Add(product);
            await DbContext.SaveChangesAsync();

            return _mapper.Map<Product, ProductDTO>(obj.Entity);
        }
            catch (Exception)
            {

                return null;
            }
            
        }

        public  async Task<int> Delete(int id)
        {
            var result = await DbContext.Products.FirstOrDefaultAsync(c => c.ProductId == id);
            if (result != null)
            {
                DbContext.Products.Remove(result);
                return await DbContext.SaveChangesAsync();
            }
            return 0;
        }

        public  async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(DbContext.Products.Include(c=> c.Category));
        }

        public  async Task<ProductDTO> GetById(int id)
        {
            var result = await DbContext.Products.Include(c=>c.Category).FirstOrDefaultAsync(c => c.ProductId == id);
            if (result != null)
            {
                return _mapper.Map<Product, ProductDTO>(result);
            }
            return new ProductDTO();
        }

        public  async Task<ProductDTO> Update(ProductDTO objDTO)
        {
            var result = DbContext.Products.FirstOrDefault(c => c.ProductId == objDTO.ProductId);
            if (result != null)
            {
                result.ProductName = objDTO.ProductName;
                result.Description = objDTO.Description;
                result.Image1 = objDTO.Image1;
                result.CategoryId = objDTO.CategoryId;
                result.Color = objDTO.Color;
                result.MetalWeight = objDTO.MetalWeight;
                result.MetalBrand = objDTO.MetalBrand;

                DbContext.Update(result);
                DbContext.SaveChanges();
                return _mapper.Map<Product, ProductDTO>(result);
            }
            return objDTO;
        }
    }
}
