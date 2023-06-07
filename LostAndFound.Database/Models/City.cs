#nullable disable

namespace LostAndFound.Database.Models
{
    public partial class City
    {
        public City()
        {
            Items = new HashSet<Item>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
