using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data
{
    public class AppSecondDBContent : DbContext
    {
        public DbSet<isCopiedPhoto> isCopiedPhotos { get; set; }
        public AppSecondDBContent(DbContextOptions<AppSecondDBContent> options)
            : base(options)
        {

        }
    }
}
