namespace PizzaMasterEmporium.Framework.DataAccess
{
    public interface IDatabaseConnectionStringProvider
    {
        string ConnectionString { get; }
    }
}