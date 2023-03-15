using System.Drawing;

namespace StyleSphere.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            sizeList = new List<SizesMaster>();
            ColorList = new List<ColorMaster>();
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int ColorCount { get; set; }
        public string ThumbnailImage { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Description { get; set; }
        public decimal ratings { get; set; }
        public int NoOfRatings { get; set; }

        public List<SizesMaster> sizeList { get; set; }

        public List<ColorMaster> ColorList { get; set; }
    }
}