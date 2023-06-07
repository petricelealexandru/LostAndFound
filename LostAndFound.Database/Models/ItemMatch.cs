#nullable disable

namespace LostAndFound.Database.Models
{
    public partial class ItemMatch
    {
        public Guid Id { get; set; }
        public Guid ItemFoundId { get; set; }
        public Guid ItemLostId { get; set; }
        public DateTime MatchedAt { get; set; }

        public virtual ItemFound ItemFound { get; set; }
        public virtual ItemLost ItemLost { get; set; }
    }
}
