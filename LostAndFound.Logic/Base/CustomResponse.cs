namespace LostAndFound.Logic.Base
{
    public class CustomResponse
    {
        public bool Status { get; set; }
        public object Data { get; set; }
        public int StatusCode { get; set; }

        public CustomResponse(bool status, object data, int statusCode)
        {
            Status = status;
            Data = data;
            StatusCode = statusCode;
        }

        public static CustomResponse Success(object data = null)
        {
            return new CustomResponse(true, data, default(int));
        }

        public static CustomResponse Error(int statusCode = 0)
        {
            return new CustomResponse(false, null, statusCode);
        }

        public static bool IsSuccessful(CustomResponse commonResponse)
        {
            return commonResponse.Status;
        }
    }
}
