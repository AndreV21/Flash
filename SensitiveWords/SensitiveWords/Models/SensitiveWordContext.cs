
using Microsoft.EntityFrameworkCore;

namespace SensitiveWords.Models
{
    public class SensitiveWordContext : DbContext
    {
        public SensitiveWordContext(DbContextOptions<SensitiveWordContext> options)
            : base(options) { }

        public DbSet<SensitiveWord> SensitiveWord { get; set; }
    }
}
