namespace LostAndFound.Logic.Models.PostModels
{
    public class CreateMatchModel
    {
        public Guid LostItemId { get; set; }
        public Guid FoundItemId { get; set; }
    }
}