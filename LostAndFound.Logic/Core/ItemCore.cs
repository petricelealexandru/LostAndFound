using LostAndFound.Database.Base;
using LostAndFound.Logic.Base;
using LostAndFound.Logic.Models.PostModels;
using DatabaseModel = LostAndFound.Database.Models;

namespace LostAndFound.Logic.Core
{
    public class ItemCore
    {
        public static CustomResponse CreateItem(ItemCreateModel model)
        {
            using (var unitOfWork = new RepoUnitOfWork(beginTransaction: true))
            using (var itemRepo = unitOfWork.Repository<DatabaseModel.Item>())
            {
                var itemDAL = new DatabaseModel.Item()
                {
                    Id = Guid.NewGuid(),
                    TypeId = model.TypeId,
                    CreatedAt = DateTime.Now,
                };

                itemDAL = itemRepo.Create(itemDAL);
                if (itemDAL == null)
                {
                    unitOfWork.RollbackTransaction();
                    return CustomResponse.Error();
                }

                unitOfWork.CommitTransaction();
                return CustomResponse.Success(itemDAL);
            }
        }
    }
}