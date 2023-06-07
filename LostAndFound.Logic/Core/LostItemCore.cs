using LostAndFound.Database.Base;
using LostAndFound.Logic.Base;
using LostAndFound.Logic.Models.PostModels;
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
            {
                var responseCreateItem = ItemCore.CreateItem(model, itemRepo);
                if (responseCreateItem == null)
                {
                    unitOfWork.RollbackTransaction();
                    return CustomResponse.Error();
                }

                var responseCreateLostItem = CreateLostEntry(model, itemLostRepo, responseCreateItem.Id);
                if (CustomResponse.IsSuccessful(responseCreateLostItem))
                {
                    unitOfWork.RollbackTransaction();
                    return CustomResponse.Error();
                }

                unitOfWork.CommitTransaction();
                return CustomResponse.Success();
            }
        }

        #endregion

        #region private

        public static CustomResponse CreateLostEntry(ItemCreateModel model,IRepository<DatabaseModel.ItemLost> itemLostRepo, Guid itemId)
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