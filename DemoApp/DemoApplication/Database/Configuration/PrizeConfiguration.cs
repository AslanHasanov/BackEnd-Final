using DemoApplication.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApplication.Database.Configuration
{
    public class PrizeConfiguration : IEntityTypeConfiguration<Prize>
    {
        public void Configure(EntityTypeBuilder<Prize> builder)
        {
            builder
                .ToTable("Prizes");
        }
    }
}
