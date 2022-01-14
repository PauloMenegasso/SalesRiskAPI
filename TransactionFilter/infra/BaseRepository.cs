using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace TransactionFilter.infra;

public interface IBaseRepository
{
    Task<int> InsertAsync<T>(T entity) where T : class;
    Task<bool> UpdateAsync<T>(T entity) where T : class;
    Task<T> GetAsync<T>(int id) where T : class;
    Task<bool> DeleteAsync<T>(T entity) where T : class;
}

public class BaseRepository : IBaseRepository
{
    private readonly IDbProvider dbProvider;
    protected SqlConnection sqlConnection;

    public BaseRepository(IDbProvider dbProvider)
    {
        this.dbProvider = dbProvider;
        sqlConnection = dbProvider.GetConnection();
    }

    public async Task<bool> DeleteAsync<T>(T entity) where T : class
    {
        return await sqlConnection.DeleteAsync(entity);
    }

    public async Task<T> GetAsync<T>(int id) where T : class
    {
        return await sqlConnection.GetAsync<T>(id);
    }

    public async Task<int> InsertAsync<T>(T entity) where T : class
    {
        return await sqlConnection.InsertAsync(entity);
    }

    public async Task<bool> UpdateAsync<T>(T entity) where T : class
    {
        return await sqlConnection.UpdateAsync(entity);
    }
}