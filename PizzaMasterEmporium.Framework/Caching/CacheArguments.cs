namespace PizzaMasterEmporium.Framework.Caching
{
    public class CacheArguments
    {
        public int CacheMinutes { get; set; }
        public string Key { get; set; }

        public CacheArguments()
        {
            CacheMinutes = 1;
        }
    }
}