using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedModels;
using ValidatieBackend.data;

namespace ValidatieBackend.services
{
    public class AdresService : IAdresService
    {
        private ValidatieContext Db { get; set; }

        public AdresService(ValidatieContext db)
        {
            Db = db;
        }

        public AdresModel GetAdresById(int id)
        {
            var adres = Db.Adressen.Find(id);
            if (adres == null) return null;

            return new AdresModel
            {
                AdresRegel1 = adres.AdresRegel1,
                AdresRegel2 = adres.AdresRegel2,
                Gemeente = adres.Gemeente,
                Huisnummer = adres.Huisnummer,
                HuisnummerToevoeging = adres.HuisnummerToevoeging,
                Land = adres.Land,
                PostCode = adres.PostCode,
                Straat = adres.Straat
            };
        }

        public async Task<AdresModel> CreateAdres(AdresModel model)
        {
            var adres = new Adres
            {
                AdresRegel1 = model.AdresRegel1,
                AdresRegel2 = model.AdresRegel2,
                Gemeente = model.Gemeente,
                Huisnummer = model.Huisnummer,
                HuisnummerToevoeging = model.HuisnummerToevoeging,
                Land = model.Land,
                PostCode = model.PostCode,
                Straat = model.Straat
            };
            Db.Adressen.Add(adres);
            await Db.SaveChangesAsync();

            model.Id = adres.Id;
            return model;
        }
    }
}