using LostAndFound.Database.Base;
using LostAndFound.Logic.Base;
using LostAndFound.Logic.Models.ReturnModels;
using DatabaseModel = LostAndFound.Database.Models;

namespace LostAndFound.Logic.Core
{
    public class CountyCore
    {
        public static CustomResponse GetCounties()
        {
            using (var unitOfWork = new RepoUnitOfWork())
            using (var itemFoundRepo = unitOfWork.Repository<DatabaseModel.County>())
            {
                var list = itemFoundRepo.GetListQuery(item => item.Id != Guid.Empty)
                                        .Select(entity => new CountyReturnModel()
                                        {
                                            Id = entity.Id,
                                            Text = entity.Name
                                        }).ToList();

                return CustomResponse.Success(list);
            }
        }
    }
}