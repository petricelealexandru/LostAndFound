namespace LostAndFound.Database.Models
{
    public partial class Item
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ItemType Type { get; set; }
    }
}