namespace Interfaces
{
    public interface IDatabaseService
    {
        Task<IEnumerable<T>> QueryAsync<T>(string storeProcedure, string connectionStringName ,Dictionary<string, object> @params);
        Task<T?> QueryScalar<T>(string storeProcedure, string connectionStringName, Dictionary<string, object> @params);
    }
}
