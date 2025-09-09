using System;

namespace PlaywrightSnipeIT.Helpers
{
    public static class RandomDataHelper
    {
        private static readonly Random _random = new Random();

        public static string RandomAssetTag() => $"ASSET-{_random.Next(10000, 99999)}";
    }
}
