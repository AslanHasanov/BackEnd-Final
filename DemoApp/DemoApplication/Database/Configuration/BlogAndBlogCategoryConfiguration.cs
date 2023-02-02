using DemoApplication.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApplication.Database.Configuration
{
    public class BlogAndBlogCategoryConfiguration : IEntityTypeConfiguration<BlogAndBlogCategory>
    {
        public void Configure(EntityTypeBuilder<BlogAndBlogCategory> builder)
        {
            builder
                .ToTable("BlogAndBlogCategories");
        }
    }
}
