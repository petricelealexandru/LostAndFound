#nullable disable

namespace LostAndFound.Database.Models
{
    public partial class Item
    {
        public Item()
        {
            ImageTables = new HashSet<ImageTable>();
            ItemFounds = new HashSet<ItemFound>();
            ItemLosts = new HashSet<ItemLost>();
        }

        public Guid Id { get; set; }
        public Guid ItemTypeId { get; set; }
        public Guid CityId { get; set; }
        public Guid CountyId { get; set; }
        public string Color { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public bool Reward { get; set; }
        public double Cost { get; set; }

        public virtual City City { get; set; }
        public virtual County County { get; set; }
        public virtual ItemType ItemType { get; set; }
        public virtual ICollection<ImageTable> ImageTables { get; set; }
        public virtual ICollection<ItemFound> ItemFounds { get; set; }
        public virtual ICollection<ItemLost> ItemLosts { get; set; }
    }
}
