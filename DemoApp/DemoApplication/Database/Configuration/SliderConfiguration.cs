using DemoApplication.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApplication.Database.Configuration
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
    
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder
               .ToTable("Sliders");
        }
    }
}
