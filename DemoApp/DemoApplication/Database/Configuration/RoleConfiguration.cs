using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DemoApplication.Database.Models;

namespace DemoApplication.Database.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
               .ToTable("Roles");
        }
    }
}
