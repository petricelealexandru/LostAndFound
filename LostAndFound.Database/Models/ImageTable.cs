#nullable disable

namespace LostAndFound.Database.Models
{
    public partial class ImageTable
    {
        public Guid Id { get; set; }
        public string ImageData { get; set; }
        public Guid ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
