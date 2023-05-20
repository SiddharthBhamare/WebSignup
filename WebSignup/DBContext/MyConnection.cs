using Microsoft.EntityFrameworkCore;
using WebSignup.DAL;
using WebSignup.Models;
using WebSignup.RegistrationDAL;

namespace WebSignup.DBContext
{
    public class MyConnection : DbContext
    {
        public MyConnection(DbContextOptions<MyConnection> options) : base(options)
        {
        }
        public DbSet<UserEntity> _Users { get; set; }
        public DbSet<UsersCred> _UsersCred { get; set; }

    }
}
