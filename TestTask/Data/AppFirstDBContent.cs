using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data
{
    public class AppFirstDBContent : DbContext
    {
        public DbSet<Photo> Photos { get; set; }
        public AppFirstDBContent(DbContextOptions<AppFirstDBContent> options)
            : base(options)
        {

        }
    }
}
