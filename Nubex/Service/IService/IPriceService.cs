
using Nubex_Models;

namespace Nubex.Service.IService
{
    public interface IPriceService
    {
        Task<golds> GetPrice();
    }
}
