using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace UploadClient
{
    public class AppFiles : IAppFiles
    {
        private readonly IConfigurationRoot configuration;
        private readonly IUploadDAL uploadDAL;
        private readonly IUploadHandler uploadHandler;

        public IUploadHandler Upload
        {
            get { return uploadHandler; }
        }
        public AppFiles(IConfigurationRoot configuration, IUploadDAL uploadDAL, IUploadHandler uploadHandler)
        {
            this.configuration = configuration;
            this.uploadDAL = uploadDAL;
            this.uploadHandler = uploadHandler;
        }

        #region AppFiles Members

        public void Action(string token)
        {
            uploadDAL.GetResponse(configuration["GetAppFilesEndpoint"], token);
            var jobj = JArray.Parse(uploadDAL.ResponseBody);
        }

        #endregion
    }
}
