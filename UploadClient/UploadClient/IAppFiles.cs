namespace UploadClient
{
    public interface IAppFiles
    {
        IUploadHandler Upload { get; }

        void Action(string token);
    }
}