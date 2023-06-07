#nullable disable

namespace LostAndFound.Database.Models
{
    public partial class ItemFound
    {
        public ItemFound()
        {
            ItemMatches = new HashSet<ItemMatch>();
        }

        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public DateTime FoundAt { get; set; }

        public virtual Item Item { get; set; }
        public virtual ICollection<ItemMatch> ItemMatches { get; set; }
    }
}
