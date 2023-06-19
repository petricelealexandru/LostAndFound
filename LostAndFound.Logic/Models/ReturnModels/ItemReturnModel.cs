namespace LostAndFound.Logic.Models.PostModels
{
    public class ItemReturnModel
    {
        public Guid Id { get; set; }
        public string ItemType { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Color { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string DateAndTime { get; set; }
        public bool? Reward { get; set; }
        public double? Cost { get; set; }
    }
}