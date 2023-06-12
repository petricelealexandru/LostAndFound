using LostAndFound.Database.Base;
using LostAndFound.Logic.Base;
using LostAndFound.Logic.Models.PostModels;
using DatabaseModel = LostAndFound.Database.Models;

namespace LostAndFound.Logic.Core
{
    public class ItemCore
    {
        public static DatabaseModel.Item CreateItem(ItemCreateModel model, IRepository<DatabaseModel.Item> itemRepo)
        {
            var itemDAL = new DatabaseModel.Item()
            {
                Id = Guid.NewGuid(),
                ItemTypeId = model.ItemTypeId,
                Address = model.Address,
                CityId = model.CityId,
                CountyId = model.CountyId,
                Color = model.Color,
                Description =model.Description,
                ContactNumber = model.ContactNumber,
                ContactEmail = model.ContactEmail,
                Reward = model.Reward,
                Cost = model.Cost
            };

            itemDAL = itemRepo.Create(itemDAL);
            return itemDAL;
        }
    }
}