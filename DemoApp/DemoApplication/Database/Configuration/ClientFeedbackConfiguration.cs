using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DemoApplication.Database.Models;

namespace DemoApplication.Database.Configuration
{
    public class ClientFeedbackConfiguration : IEntityTypeConfiguration<ClientFeedback>
    {
        public void Configure(EntityTypeBuilder<ClientFeedback> builder)
        {
            builder
               .ToTable("ClientFeedbacks");
        }
    }
}
