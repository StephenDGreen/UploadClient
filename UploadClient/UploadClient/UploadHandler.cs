using Newtonsoft.Json.Linq;

namespace UploadClient
{
    public class UploadHandler : IUploadHandler
    {
        JToken jsonData;
        string token;
        public UploadHandler()
        {

        }

        public JToken JsonData
        {
            get { return jsonData; }
            set { jsonData = value; }
        }

        public string Token
        {
            get { return token; }
            set { token = value; }
        }

    }
}