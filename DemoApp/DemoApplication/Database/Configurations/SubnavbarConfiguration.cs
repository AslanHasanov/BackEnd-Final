using DemoApplication.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApplication.Database.Configurations
{
    public class SubNavbarConfiguration : IEntityTypeConfiguration<SubNavbar>
    {
    
        public void Configure(EntityTypeBuilder<SubNavbar> builder)
        {
            builder
               .ToTable("SubNavbars");
        }
    }
}
