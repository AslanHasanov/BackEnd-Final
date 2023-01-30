using DemoApplication.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApplication.Database.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
    
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
               .ToTable("Orders");
        }
    }
}
