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
        public ApplicationDbContext DbContext { get; }
        public IMapper _mapper { get; set; }
        public ProductRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Create(ProductDTO objDTO)
        {
            var product = _mapper.Map<ProductDTO, Product>(objDTO);

            var obj = DbContext.Products.Add(product);
            await DbContext.SaveChangesAsync();
            var final = _mapper.Map<Product, ProductDTO>(obj.Entity);
            return final;
        }

        public async Task<int> Delete(int id)
        {
            var result = await DbContext.Products.FirstOrDefaultAsync(c => c.ProductId == id);
            if (result != null)
            {
                DbContext.Products.Remove(result);
                return await DbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            //IEnumerable<Product> prod = new List<Product>();
            //prod = await DbContext.Products
            //    .Include(u => u.Category)
            //    .Include(c => c.ProductPrices)
            //    .ToListAsync();

            //if (prod != null)
            //{
            //    return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(prod);
            //}
            //return new List<ProductDTO>();

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(await DbContext.Products
                    .Include(u => u.Category)
                    .Include(c => c.ProductPrices)
                    .ToListAsync());
        }

        public async Task<ProductDTO> GetById(int id)
        {
            try
            {
                var result = await DbContext.Products.Include(u => u.Category).Include(c => c.ProductPrices).FirstOrDefaultAsync(c => c.ProductId == id);
                if (result != null)
                {
                    return _mapper.Map<Product, ProductDTO>(result);
                }
            }
            catch (Exception)
            {

                return new ProductDTO();
            }

            return new ProductDTO();
        }

        public async Task<ProductDTO> Update(ProductDTO objDTO)
        {
            var result = await DbContext.Products.FirstOrDefaultAsync(c => c.ProductId == objDTO.ProductId);
            if (result != null)
            {
                result.ProductSKU = objDTO.ProductSKU;
                result.ProductName = objDTO.ProductName;
                result.Description = objDTO.Description;
                result.Detail = objDTO.Detail;
                result.MetalWeight = objDTO.MetalWeight;
                result.MetalBrand = objDTO.MetalBrand;
                result.IsHighlighted = objDTO.IsHighlighted;
                result.Weight = objDTO.Weight;
                result.Purify = objDTO.Purify;
                result.Manufacture = objDTO.Manufacture;
                result.Certificate = objDTO.Certificate;
                result.IsTax = objDTO.IsTax;
                result.Featured = objDTO.Featured;
                result.Color = objDTO.Color;
                result.Size = objDTO.Size;
                result.ProductTag = objDTO.ProductTag;
                result.Image1 = objDTO.Image1;
                result.Image2 = objDTO.Image2;
                result.Image3 = objDTO.Image3;
                result.remark_1 = objDTO.remark_1;
                result.remark_2 = objDTO.remark_2;
                result.remark_3 = objDTO.remark_3;
                result.ModifiedOn = DateTime.Now;
                result.ModifiedBy = objDTO.ModifiedBy;
                result.CategoryId = objDTO.CategoryId;

                DbContext.Update(result);
                DbContext.SaveChanges();
                return _mapper.Map<Product, ProductDTO>(result);
            }
            return objDTO;
        }
    }
}
