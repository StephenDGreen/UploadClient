using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UploadClient
{
    public class GetToken : IGetToken
    {
        private readonly IConfigurationRoot configuration;
        private readonly IUploadDAL uploadDAL;
        private readonly IUploadHandler uploadHandler;
        private readonly ILogin login;

        public IUploadHandler UploadHandler
        {
            get { return uploadHandler; }
        }
        public GetToken(IConfigurationRoot configuration, IUploadDAL uploadDAL, IUploadHandler uploadHandler, ILogin login)
        {
            this.configuration = configuration;
            this.uploadDAL = uploadDAL;
            this.uploadHandler = uploadHandler;
            this.login = login;
        }

        #region GetToken Members

        public void Action()
        {
            uploadDAL.GetPostResponse(configuration["GetTokenEndpoint"], GetJsonInString());
            uploadHandler.Token = JObject.Parse(uploadDAL.ResponseBody).SelectToken("token").Value<string>();
        }

        private string GetJsonInString()
        {
            return JsonConvert.SerializeObject(login);
        }

        #endregion
    }
}
