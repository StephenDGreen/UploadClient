namespace UploadClient
{
    public interface IGetToken
    {
        IUploadHandler UploadHandler { get; }

        void Action();
    }
}