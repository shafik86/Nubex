using Newtonsoft.Json;
using Nubex_Client.Service.IService;
using Nubex_Models;

namespace Nubex_Client.Service
{
    public class ProductService : IProductService
    {
        private IConfiguration _configuration;
        private string BaseServerUrl;
        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public readonly HttpClient _httpClient;

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/product");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
                foreach (var prod in products)
                {
                    prod.Image1 = BaseServerUrl+prod.Image1;
                    prod.Image2 = BaseServerUrl+prod.Image2;
                    prod.Image3 = BaseServerUrl+prod.Image3;    
                }
                return products;
            }
            return new List<ProductDTO>();
        }

        public async Task<ProductDTO> GetById(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/product/{productId}");

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDTO>(content);
                product.Image1 = BaseServerUrl + product.Image1;
                product.Image2 = BaseServerUrl + product.Image2;
                product.Image3 = BaseServerUrl + product.Image3;
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
