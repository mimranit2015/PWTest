namespace PlaywrightSnipeIT.Helpers
{
    public static class UrlHelper
    {
        public static string BaseUrl => "https://demo.snipeitapp.com";
        public static string LoginUrl => $"{BaseUrl}/login";
        public static string AssetsUrl => $"{BaseUrl}/hardware";
    }
}
