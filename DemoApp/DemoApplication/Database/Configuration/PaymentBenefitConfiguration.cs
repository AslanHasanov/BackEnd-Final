using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DemoApplication.Database.Models;

namespace DemoApplication.Database.Configuration
{
    public class PaymentBenefitConfiguration : IEntityTypeConfiguration<PaymentBenefit>
    {
        public void Configure(EntityTypeBuilder<PaymentBenefit> builder)
        {
            builder
               .ToTable("PaymentBenefits");
        }
    }
}
