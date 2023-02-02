using DemoApplication.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApplication.Database.Configuration
{
    public class BlogAndBlogTagConfiguration : IEntityTypeConfiguration<BlogAndBlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogAndBlogTag> builder)
        {
            builder
                .ToTable("BlogAndBlogTags");
        }
    }
}
