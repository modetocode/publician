/// <summary>
/// Contains various constants and tweakables connected with the application.
/// </summary>
public static class Constants {
    public static class API {
        public const string UrlWithKey = "https://external.api.yle.fi/v1/programs/items.json?app_id=4151bf48&app_key=e4d4b7028cdcb0b39785b0ca078984c2";
        public const string QueryArgumentString = "&q=";
        public const string OffsetArgumentString = "&offset=";
        public const string LimitArgumentString = "&limit=";
    }

    public static class Search {
        public const int NumberOfItemsPerPage = 10;
    }

    public static class Math {
        public const float FloatPositiveEpsilon = 0.00001f;
    }
}