namespace UploadClient
{
    public class ConsoleApplication
    {
        private readonly IGetToken getToken;
        private readonly IAppFiles appFiles;

        private enum ActionRequired { None, AddFile };
        public ConsoleApplication(IGetToken getToken, IAppFiles appFiles)
        {
            this.getToken = getToken;
            this.appFiles = appFiles;
        }
        public void Run(string[] args)
        {
            ActionRequired actionRequired = ActionRequired.None;
            foreach (string cmd in args)
            {
                if (cmd.StartsWith("/"))
                {
                    switch (cmd.Substring(1))
                    {
                        case "f":
                            actionRequired = ActionRequired.AddFile;
                            break;
                        default:
                            break;
                    }
                }
            }
            switch (actionRequired)
            {
                case ActionRequired.AddFile:
                    getToken.Action();
                    appFiles.Action(getToken.UploadHandler.Token);
                    break;
                default:
                    break;
            }
        }
    }
}
