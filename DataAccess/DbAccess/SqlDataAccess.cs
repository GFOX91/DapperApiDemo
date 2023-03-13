using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DbAccess;
/// <summary>
/// Used to access data from SQL DBs
/// </summary>
public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Load data, using a stored procedure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="storedProcedure">The stored procedure to run</param>
    /// <param name="parameters">The parameters to pass to the stored procedure</param>
    /// <param name="connectionId">The connectionId to retrieve the connection string from, 'Default' if no value provided</param>
    public async Task<IEnumerable<T>> LoadData<T, U>(
        string storedProcedure,
        U parameters,
        string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Save data, using a stored procedure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="storedProcedure">The stored procedure used to save the data</param>
    /// <param name="parameters">The parameters to pass to the stored procedure</param>
    /// <param name="connectionId">The connectionId to retrieve the connection string from, 'Default' if no value provided</param>
    public async Task SaveData<T>(
        string storedProcedure,
        T parameters,
        string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        await connection.ExecuteAsync(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }
}
