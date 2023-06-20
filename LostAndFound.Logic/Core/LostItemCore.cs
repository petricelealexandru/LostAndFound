using LostAndFound.Database.Base;
using LostAndFound.Logic.Base;
using LostAndFound.Logic.Models.PostModels;
using Microsoft.VisualBasic;
using DatabaseModel = LostAndFound.Database.Models;

namespace LostAndFound.Logic.Core
{
    public class LostItemCore
    {
        #region public

        public static CustomResponse CreateLostItem(ItemCreateModel model)
        {
            using (var unitOfWork = new RepoUnitOfWork(beginTransaction: true))
            using (var itemRepo = unitOfWork.Repository<DatabaseModel.Item>())
            using (var itemLostRepo = unitOfWork.Repository<DatabaseModel.ItemLost>())
            using (var imageTableRepo = unitOfWork.Repository<DatabaseModel.ImageTable>())
            {
                var responseCreateItem = ItemCore.CreateItem(model, itemRepo);
                if (responseCreateItem == null)
                {
                    unitOfWork.RollbackTransaction();
                    return CustomResponse.Error();
                }

                var responseImage = ImageTableCore.CreateImage(model.PictureContent, responseCreateItem.Id, imageTableRepo);
                if (responseImage == null)
                {
                    unitOfWork.RollbackTransaction();
                    return CustomResponse.Error();
                }

                var responseCreateLostItem = CreateLostEntry(itemLostRepo, responseCreateItem.Id);
                if (!CustomResponse.IsSuccessful(responseCreateLostItem))
                {
                    unitOfWork.RollbackTransaction();
                    return CustomResponse.Error();
                }

                unitOfWork.CommitTransaction();
                return CustomResponse.Success();
            }
        }

        public static CustomResponse GetLostItems()
        {
            using (var unitOfWork = new RepoUnitOfWork())
            using (var itemLostRepo = unitOfWork.Repository<DatabaseModel.ItemLost>())
            {
                var list = itemLostRepo.GetListQuery(item => item.Id != Guid.Empty)
                                       .OrderByDescending(item => item.LostAt)
                                       .Select(entity => new ItemReturnModel()
                                       {
                                           Id = entity.Id,
                                           ItemType = entity.Item.ItemType.Type,
                                           City = entity.Item.City,
                                           County = entity.Item.County.Name,
                                           Color = entity.Item.Color,
                                           Address = entity.Item.Address,
                                           Description = entity.Item.Description,
                                           ContactNumber = entity.Item.ContactNumber,
                                           ContactEmail = entity.Item.ContactEmail,
                                           Reward = entity.Item.Reward,
                                           Cost = entity.Item.Cost,
                                           DateAndTime = entity.LostAt.ToString("dd-MM-yyyy HH:mm"),
                                           PictureContent = entity.Item.ImageTables.First() != null ?
                                                             entity.Item.ImageTables.First().ImageData :
                                                             String.Empty,
                                       }).ToList();

                return CustomResponse.Success(list);
            }
        }

        #endregion

        #region private

        public static CustomResponse CreateLostEntry(IRepository<DatabaseModel.ItemLost> itemLostRepo, Guid itemId)
        {
            var itemLostDAL = new DatabaseModel.ItemLost()
            {
                Id = Guid.NewGuid(),
                ItemId = itemId,
                LostAt = DateTime.Now
            };

            itemLostDAL = itemLostRepo.Create(itemLostDAL);
            if (itemLostDAL == null)
            {
                return CustomResponse.Error();
            }

            return CustomResponse.Success();
        }

        #endregion
    }
}