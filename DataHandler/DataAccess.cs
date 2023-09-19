using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataHandler;


public class DataAccess : IDataAccess
{
    public async Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString)
    {
        using (IDbConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var rows = await connection.QueryAsync<T>(sql, parameters);
			connection.Close();

			return rows.ToList();
        }


    }

    public async Task<int> SaveData<T>(string sql, T parameters, string connectionString)
    {
		using (IDbConnection connection = new MySqlConnection(connectionString))
		{
			connection.Open();

			var x = await connection.ExecuteAsync(sql, parameters);
            connection.Close();
			return x;
            
         
        }

    }


}