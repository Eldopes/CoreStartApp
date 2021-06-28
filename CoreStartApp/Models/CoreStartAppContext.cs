using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace CoreStartApp.Models
{
    public sealed class CoreStartAppContext : DbContext
    {
        public Microsoft.EntityFrameworkCore.DbSet<UserInfo> UserInfos { get; set; }

        public CoreStartAppContext(DbContextOptions<CoreStartAppContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserInfo>().ToTable("UserInfos");
        }
    }
}