using System;
using System.Linq;
using FluentValidation;

namespace SharedModels
{
    public class AdresModel
    {
        public int Id { get; set; }
        public string Straat { get; set; }
        public string AdresRegel1 { get; set; }
        public string AdresRegel2 { get; set; }
        public string PostCode { get; set; }
        public int Huisnummer { get; set; }
        public string HuisnummerToevoeging { get; set; }
        public string Gemeente { get; set; }
        public string Land { get; set; }
        public bool IsBinnenlands => string.IsNullOrEmpty(Land) || Land.ToLower() == "nederland";
    }

    public class AdresValidator : AbstractValidator<AdresModel>
    {
        public AdresValidator()
        {
            RuleFor(a => a.Huisnummer).NotEmpty().ExclusiveBetween(0, 100000);
            RuleFor(a => a.HuisnummerToevoeging).MaximumLength(10);
            RuleFor(a => a.PostCode).NotEmpty().Must(BeAValidPostcode).WithMessage(a => $"Gebruik een voor {(string.IsNullOrEmpty(a.Land) ? "Nederland" : a.Land)} geldige postcode.");
            RuleFor(a => a.Straat).MaximumLength(255).Must(BeUsedInNederland).WithMessage("Straat is verplicht.");
            RuleFor(a => a.AdresRegel1).MaximumLength(255).Must(BeValidAdresRegel1ForLand).WithMessage("Eerste adresregel is verplicht.");
            RuleFor(a => a.AdresRegel2).MaximumLength(255);
            RuleFor(a => a.Gemeente).MaximumLength(255);
        }

        private bool BeAValidPostcode(AdresModel adres, string postcode)
        {
            return adres.Land?.ToLower() switch
            {
                null => IsNederlandsePostcode(postcode),
                "" => IsNederlandsePostcode(postcode),
                "nederland" => IsNederlandsePostcode(postcode),
                "duitsland" => IsDuitsePostcode(postcode),
                _ => IsWereldPostcode(postcode)
            };
        }

        private bool BeUsedInNederland(AdresModel adres, string straat)
        {
            if (string.IsNullOrEmpty(adres.Land) || adres.Land.ToLower() == "nederland")
            {
                // De kortste straatnamen zijn de A en de B in het Zuid-Hollandse Ottoland
                return !string.IsNullOrEmpty(straat); // rare jongens, die Ottolanders
            }

            return true;
        }

        private bool BeValidAdresRegel1ForLand(AdresModel adres, string adresRegel1)
        {
            if (string.IsNullOrEmpty(adres.Land) || adres.Land.ToLower() == "nederland")
            {
                return true;
            }

            return !string.IsNullOrEmpty(adresRegel1);
        }

        // hier schrijf je zo een unittest tegen natuurlijk
        private bool IsNederlandsePostcode(string postcode)
        {
            if (postcode == null) return false;
            var pc = postcode.Replace(" ", "");
            if (pc.Length != 6) return false;
            var cijfers = pc.Substring(0, 4);
            var letters = pc.Substring(4, 2);
            return cijfers.All(char.IsDigit) && letters.All(char.IsLetter);
        }

        private bool IsDuitsePostcode(string postcode)
        {
            var pc = postcode.Replace(" ", "");
            return pc.Length == 5 && postcode.All(char.IsDigit);
        }

        private bool IsWereldPostcode(string postcode)
        {
            return postcode.Length <= 15;
        }
    }
}
