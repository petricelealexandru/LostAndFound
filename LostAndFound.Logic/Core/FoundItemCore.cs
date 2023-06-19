using LostAndFound.Database.Base;
using LostAndFound.Database.Models;
using LostAndFound.Logic.Base;
using LostAndFound.Logic.Models.PostModels;
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

                var responseCreateFoundItem = CreateFoundEntry(itemFoundRepo, responseCreateItem.Id);
                if (!CustomResponse.IsSuccessful(responseCreateFoundItem))
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
                var list = itemFoundRepo.GetListQuery(item => item.Id != Guid.Empty)
                                        .OrderByDescending(item => item.FoundAt)
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
                                            DateAndTime = entity.FoundAt.ToString("dd-MM-yyyy HH:mm")
                                        }).ToList();

                return CustomResponse.Success(list);
            }
        }

        #endregion

        #region private

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

        #endregion
    }

}
