using FluentValidation.TestHelper;
using NUnit.Framework;
using SharedModels;

namespace TestSharedModels
{
    public class Tests
    {
        private AdresValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new AdresValidator();
        }

        [Test]
        public void TestNederlandsePostcodes()
        {
            var adres = new AdresModel{Land = "nederland", PostCode = "12345"};
            validator.ShouldHaveValidationErrorFor(a => a.PostCode, adres);
            adres.PostCode = "1234ab";
            validator.ShouldNotHaveValidationErrorFor(a => a.PostCode, adres);
            adres.PostCode = "1234 ab";
            validator.ShouldNotHaveValidationErrorFor(a => a.PostCode, adres);
            adres.PostCode = "1234Ab";
            validator.ShouldNotHaveValidationErrorFor(a => a.PostCode, adres);
            adres.PostCode = "12ab34";
            validator.ShouldHaveValidationErrorFor(a => a.PostCode, adres);
            adres.PostCode = "";
            validator.ShouldHaveValidationErrorFor(a => a.PostCode, adres);
            adres.PostCode = null;
            validator.ShouldHaveValidationErrorFor(a => a.PostCode, adres);
        }
    }
}