using LostAndFound.Database.Base;
using LostAndFound.Logic.Base;
using LostAndFound.Logic.Models.PostModels;
using System.Security.Cryptography.X509Certificates;
using DatabaseModel = LostAndFound.Database.Models;

namespace LostAndFound.Logic.Core
{
    public class FoundItemCore
    {
        #region public

        public static CustomResponse CreateFoundItem(ItemCreateModel model)
        {
            using (var unitOfWork = new RepoUnitOfWork(beginTransaction: true))
            using (var itemRepo = unitOfWork.Repository<DatabaseModel.Item>())
            using (var itemFoundRepo = unitOfWork.Repository<DatabaseModel.ItemFound>())
            {
                var responseCreateItem = ItemCore.CreateItem(model, itemRepo);
                if (responseCreateItem == null)
                {
                    unitOfWork.RollbackTransaction();
                    return CustomResponse.Error();
                }

                //
                var responseCreateFoundItem = CreateFoundEntry(itemFoundRepo, responseCreateItem.Id);
                if (CustomResponse.IsSuccessful(responseCreateFoundItem))
                {
                    unitOfWork.RollbackTransaction();
                    return CustomResponse.Error();
                }

                unitOfWork.CommitTransaction();
                return CustomResponse.Success();
            }
        }

        public static CustomResponse GetFoundItems()
        {
            using (var unitOfWork = new RepoUnitOfWork())
            using (var itemFoundRepo = unitOfWork.Repository<DatabaseModel.ItemFound>())
            {
                var list = itemFoundRepo.GetListQuery(item=> item.Id != Guid.Empty)
                                       .Select(entity => new ItemReturnModel()
                                       {
                                           Id = entity.Id,
                                           ItemType = entity.Item.ItemType.Type,
                                           City = entity.Item.City.Name,
                                           County = entity.Item.County.Name,
                                           Color = entity.Item.Color,
                                           Address = entity.Item.Address,
                                           Description = entity.Item.Description,
                                           ContactNumber = entity.Item.ContactNumber,
                                           ContactEmail = entity.Item.ContactEmail,
                                       }).ToList();
                return CustomResponse.Success(list);
            }
        }

        #endregion

        #region private

        //de ce e aici?
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

        public static CustomResponse CreateFoundEntry(IRepository<DatabaseModel.ItemFound> itemFoundRepo, Guid itemId)
        {
            var itemFoundDAL = new DatabaseModel.ItemFound()
            {
                Id = Guid.NewGuid(),
                ItemId = itemId,
                FoundAt = DateTime.Now
            };

            itemFoundDAL = itemFoundRepo.Create(itemFoundDAL);
            if (itemFoundDAL == null)
            {
                return CustomResponse.Error();
            }
            return CustomResponse.Success();
        }

    }

}
