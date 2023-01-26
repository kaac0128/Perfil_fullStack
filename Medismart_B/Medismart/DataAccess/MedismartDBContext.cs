using Medismart.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Medismart.DataAccess
{
    public class MedismartDBContext : DbContext
    {

        private readonly ILoggerFactory _loggerFactory;
        public MedismartDBContext(DbContextOptions<MedismartDBContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }
        public DbSet<User>? Users { get; set; }

    }
}
