using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nubex_Business.Repository.IRepository;
using Nubex_DataAccess;
using Nubex_DataAccess.Data;
using Nubex_Models;

namespace Nubex_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public ApplicationDbContext applicationDbContext { get; }
        public IMapper _mapper { get; set; }
        public async Task<CategoryDTO> Create(CategoryDTO objDTO)
        {
           var category = _mapper.Map<CategoryDTO, Category>(objDTO);
            category.CreatedDate = DateTime.Now;
            var obj = applicationDbContext.Categories.Add(category);
            await applicationDbContext.SaveChangesAsync();

            return _mapper.Map<Category, CategoryDTO>(obj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var result = await applicationDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (result != null)
            {
                applicationDbContext.Categories.Remove(result);
                return await applicationDbContext.SaveChangesAsync();
            }
            return 0;

        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {

          return _mapper.Map<IEnumerable<Category>,IEnumerable<CategoryDTO>>(applicationDbContext.Categories);
      

        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var result = await applicationDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (result != null)
            {
               return _mapper.Map<Category,CategoryDTO>(result);
            }
            return new CategoryDTO();
        }

        public async Task<CategoryDTO> Update(CategoryDTO categoryDTO)
        {
            var result = applicationDbContext.Categories.FirstOrDefault(c => c.Id == categoryDTO.Id);
            if (result!= null)
            {
                result.Name = categoryDTO.Name;
                applicationDbContext.Update(result);
                applicationDbContext.SaveChanges();
                return _mapper.Map<Category,CategoryDTO>(result);
            }
            return categoryDTO;
        }
    }
}
