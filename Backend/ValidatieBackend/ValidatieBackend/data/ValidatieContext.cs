using Microsoft.EntityFrameworkCore;

namespace ValidatieBackend.data
{
    public class ValidatieContext : DbContext
    {
        public ValidatieContext(DbContextOptions<ValidatieContext> options)
            : base(options)
        {
        }

        public DbSet<Adres> Adressen { get; set; }
    }
}