using Newtonsoft.Json.Linq;

namespace UploadClient
{
    public interface IUploadHandler
    {
        JToken JsonData { get; set; }

        string Token { get; set; }
    }
}