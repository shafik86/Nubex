using Newtonsoft.Json;
using Nubex_Client.Service.IService;
using Nubex_Models;

namespace Nubex_Client.Service
{
    public class ProductPremiumService : IProductPremiumService
    {
        private IConfiguration _configuration;
        private string BaseServerUrl;
        public ProductPremiumService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public readonly HttpClient _httpClient;

        public async Task<IEnumerable<ProductPremiumDTO>> GetAll(int Id)
        {
            var response = await _httpClient.GetAsync($"/api/ProductPremium/all/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var productPremium = JsonConvert.DeserializeObject<IEnumerable<ProductPremiumDTO>>(content);
             
                return productPremium;
            }
            return new List<ProductPremiumDTO>();
        }

    }
}
