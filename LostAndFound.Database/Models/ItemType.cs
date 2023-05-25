namespace LostAndFound.Database.Models
{
    public partial class ItemType
    {
        public ItemType()
        {
            Items = new HashSet<Item>();
        }

        public Guid Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}