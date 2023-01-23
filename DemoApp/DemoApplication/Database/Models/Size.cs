
using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Size : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public List<ProductSize>? ProductSizes { get; set; }


    }
}
