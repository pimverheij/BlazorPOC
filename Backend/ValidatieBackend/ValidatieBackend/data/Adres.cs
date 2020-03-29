using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ValidatieBackend.data
{
    [Table("Adres")]
    public class Adres
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Straat { get; set; }

        [MaxLength(255)]
        public string AdresRegel1 { get; set; }

        [MaxLength(255)]
        public string AdresRegel2 { get; set; }

        [MaxLength(15)]
        public string PostCode { get; set; }

        [Range(0, 100000)]
        public int Huisnummer { get; set; }

        [MaxLength(10)]
        public string HuisnummerToevoeging { get; set; }

        [MaxLength(255)]
        public string Gemeente { get; set; }

        [MaxLength(255)]
        public string Land { get; set; }
    }
}
