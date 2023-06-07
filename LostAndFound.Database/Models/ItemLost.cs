#nullable disable

namespace LostAndFound.Database.Models
{
    public partial class ItemLost
    {
        public ItemLost()
        {
            ItemMatches = new HashSet<ItemMatch>();
        }

        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public DateTime LostAt { get; set; }

        public virtual Item Item { get; set; }
        public virtual ICollection<ItemMatch> ItemMatches { get; set; }
    }
}
