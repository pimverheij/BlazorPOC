using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValidatieBackend.data
{
    public class Adres
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
    }
}
