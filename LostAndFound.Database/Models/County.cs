#nullable disable

namespace LostAndFound.Database.Models
{
    public partial class County
    {
        public County()
        {
            Items = new HashSet<Item>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
