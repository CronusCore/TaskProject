using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using Interfaces;

namespace Services
{
    public class DatabaseService : IDatabaseService
    {
        private IConfigurationSection _connectionStrings;

        public DatabaseService(IConfiguration connectionStringSection)
        {
            _connectionStrings = connectionStringSection.GetSection("ConnectionStrings"); 
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string storeProcedure, string connectionStringName , Dictionary<string, object> @params)
        {
            //implementar la lógica de DB
            List<T> result = new List<T>();

            using SqlConnection connection = new SqlConnection(_connectionStrings[connectionStringName]);
            using SqlCommand command = new SqlCommand(storeProcedure, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            if (@params != null)
            {
                foreach (var param in @params)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            
            var properties = typeof(T).GetProperties();
            
            while (await reader.ReadAsync())
            {
                var item = Activator.CreateInstance<T>();

                foreach (var property in properties)
                {
                    string propertyName = property.Name;
                    var type = property.PropertyType;
                    //verificar que la propiedad sea algun IENUMERABLE
                    if (type != typeof(string) && (type.IsArray || typeof(System.Collections.IEnumerable).IsAssignableFrom(type)))
                    {
                        //utilizar jsonConverter para establecer esa informacion
                        try
                        {
                            property.SetValue(item, JsonSerializer.Deserialize(reader.GetString(propertyName), type));

                        }
                        catch
                        {
                            property.SetValue(item, null);
                        }
                    }
                    else
                    {
                        property.SetValue(item, Convert.ChangeType(reader.GetValue(propertyName), property.PropertyType));

                    }

                }

                result.Add(item);
            }

            return result;
        }
    }
}
