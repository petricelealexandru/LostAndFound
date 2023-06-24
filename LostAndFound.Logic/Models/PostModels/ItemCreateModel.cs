namespace LostAndFound.Logic.Models.PostModels
{
    public class ItemCreateModel
    {
        public Guid ItemTypeId { get; set; }
        public string City { get; set; }
        public Guid CountyId { get; set; }
        public string Color { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public bool? Reward { get; set; }
        public double? Cost { get; set; }
        public string PictureContent { get; set; }
        public string DateAndTime { get; set; }
    }
}