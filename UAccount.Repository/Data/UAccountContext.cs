using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UAccount.Domain.Identity;
using UAccount.Domain.Models;

namespace UAccount.Repository.Data
{
      public class UAccountContext : IdentityDbContext<User, Role, int,
                                                    IdentityUserClaim<int>, UserRole,  IdentityUserLogin<int>, 
                                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public UAccountContext(DbContextOptions<UAccountContext> options) : base(options){}

        public DbSet<Conta> Contas { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
           base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole => 
                {
                    userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                    userRole.HasOne(ur => ur.Role)
                            .WithMany(r => r.UserRoles)
                            .HasForeignKey(ur => ur.RoleId)
                            .IsRequired();

                    userRole.HasOne(ur => ur.User)
                            .WithMany(r => r.UserRoles)
                            .HasForeignKey(ur => ur.UserId)
                            .IsRequired();
                }
            );
        }
    }
}