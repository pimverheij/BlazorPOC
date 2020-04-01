using System.Threading.Tasks;
using SharedModels;

namespace ValidatieBackend.services
{
    public interface IAdresService
    {
        Task<AdresModel> GetAdresById(int id);
        Task<int> CreateAdres(AdresModel model);
        Task<bool> UpdateAdres(AdresModel model);
        Task<bool> DeleteAdresById(int id);
    }
}