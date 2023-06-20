using LostAndFound.Database.Base;
using LostAndFound.Logic.Base;
using DatabaseModel = LostAndFound.Database.Models;

namespace LostAndFound.Logic.Core
{
    public class ImageTableCore
    {
        public static CustomResponse CreateImage(string pictureContent, Guid createdItemId,IRepository<DatabaseModel.ImageTable> imageTableRepo)
        {
            var imageDAL = new DatabaseModel.ImageTable()
            {
                Id = Guid.NewGuid(),
                ImageData = pictureContent,
                ItemId = createdItemId
            };

            imageDAL = imageTableRepo.Create(imageDAL);
            if (imageDAL == null)
            {
                return CustomResponse.Error();
            }

            return CustomResponse.Success();
        }
    }
}