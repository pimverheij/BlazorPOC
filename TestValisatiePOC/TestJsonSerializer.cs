using Newtonsoft.Json;
using NUnit.Framework;
using SharedModels;

namespace TestValisatiePOC
{
    public class TestJsonSerializer
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var json = "{\"id\":0,\"straat\":\"adassdas\",\"adresRegel1\":null,\"adresRegel2\":null,\"postCode\":\"1122tr\",\"huisnummer\":14,\"huisnummerToevoeging\":\"a\",\"gemeente\":\"flunky\",\"land\":\"Nederland\",\"isBinnenlands\":true}";
            // deze werkt dus niet, geen idee waarom...
            //var adres = System.Text.Json.JsonSerializer.Deserialize<AdresModel>(json);
            //Assert.That(adres.Huisnummer, Is.EqualTo(14));
            var adres = JsonConvert.DeserializeObject<AdresModel>(json);
            Assert.That(adres.Huisnummer, Is.EqualTo(14));
        }
    }
}