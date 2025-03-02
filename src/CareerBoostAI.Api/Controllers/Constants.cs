namespace CareerBoostAI.Api.Controllers;

public static class Constants
{

    public static class Route
    {
        public static class Candidate
        {
            private const string Root = "candidate";
            public const string CreateProfile = $"{Root}/CreateProfile";
            public const string UpdateCv = $"{Root}/UpdateCv";
            public const string ParseCv = $"{Root}/ParseCv";
            public const string UploadCv = $"{Root}/UploadCv";
        }
        
    }
    public static class Tag
    {
        public const string Health = "Health";
        public const string Candidate = "Candidate";
    }
}