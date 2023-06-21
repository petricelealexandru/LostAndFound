using LostAndFound.Database.Base;
using LostAndFound.Logic.Base;
using LostAndFound.Logic.Models.PostModels;
using DatabaseModel = LostAndFound.Database.Models;

namespace LostAndFound.Logic.Core
{
    public class MatchItemCore
    {
        public static CustomResponse GetMatchItems()
        {
            using (var unitOfWork = new RepoUnitOfWork())
            using (var itemMatchRepo = unitOfWork.Repository<DatabaseModel.ItemMatch>())
            {
                var list = itemMatchRepo.GetListQuery(item => item.Id != Guid.Empty)
                                        .OrderByDescending(item => item.MatchedAt)
                                        .Select(entity => new ItemReturnModel()
                                        {
                                            Id = entity.Id,
                                            ItemType = entity.ItemLost.Item.ItemType.Type,
                                            City = entity.ItemLost.Item.City,
                                            County = entity.ItemLost.Item.County.Name,
                                            Color = entity.ItemLost.Item.Color,
                                            Address = entity.ItemLost.Item.Address,
                                            Description = entity.ItemLost.Item.Description,
                                            ContactNumber = entity.ItemLost.Item.ContactNumber,
                                            ContactEmail = entity.ItemLost.Item.ContactEmail,
                                            Reward = entity.ItemLost.Item.Reward,
                                            Cost = entity.ItemLost.Item.Cost,
                                            DateAndTime = entity.MatchedAt.ToString("dd-MM-yyyy HH:mm"),
                                            PictureContent = entity.ItemLost.Item.ImageTables.First() != null ?
                                                             entity.ItemLost.Item.ImageTables.First().ImageData :
                                                             String.Empty,
                                        }).ToList();

                return CustomResponse.Success(list);
            }
        }

        public static CustomResponse CreateMatch(CreateMatchModel model)
        {
            using (var unitOfWork = new RepoUnitOfWork(beginTransaction: true))
            using (var itemMatchRepo = unitOfWork.Repository<DatabaseModel.ItemMatch>())
            {
                var itemMatchDAL = new DatabaseModel.ItemMatch()
                {
                    Id = Guid.NewGuid(),
                    ItemFoundId = model.FoundItemId,
                    ItemLostId = model.LostItemId,
                    MatchedAt = DateTime.Now
                };

                itemMatchDAL = itemMatchRepo.Create(itemMatchDAL);
                if (itemMatchDAL == null)
                {
                    unitOfWork.RollbackTransaction();
                    return CustomResponse.Error();
                }

                unitOfWork.CommitTransaction();
                return CustomResponse.Success();
            }
        }
    }
}