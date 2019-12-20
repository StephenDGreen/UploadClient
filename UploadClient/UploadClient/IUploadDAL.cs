namespace UploadClient
{
    public interface IUploadDAL
    {
        string ResponseBody { get; }

        void GetPostResponse(string requestEndpoint, string jsonInString);
        void GetResponse(string requestEndpoint, string token);
    }
}