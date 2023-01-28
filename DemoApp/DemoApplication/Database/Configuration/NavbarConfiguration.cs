using DemoApplication.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApplication.Database.Configuration
{
    public class NavbarConfiguration : IEntityTypeConfiguration<Navbar>
    {
    
        public void Configure(EntityTypeBuilder<Navbar> builder)
        {
            builder
               .ToTable("Navbars");
        }
    }
}
