using System.Threading.Tasks;
using SharedModels;

namespace ValidatieBackend.services
{
    public interface IAdresService
    {
        AdresModel GetAdresById(int id);
        Task<AdresModel> CreateAdres(AdresModel model);
    }
}