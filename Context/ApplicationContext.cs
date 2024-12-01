using Microsoft.EntityFrameworkCore;
using mindEaseAPI.Models;

namespace mindEaseAPI.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { 
        }

        public DbSet<booking_information> booking_information { get; set;}
        public DbSet<client_information> client_information { get; set;}
    }
}
