using Dapper;
using DapperCQRS.Data;
using DapperCQRS.Models;

namespace DapperCQRS.Queries.Handlers
{
    public class GetAllCategoriesHandler
    {
        private readonly DbConnectionFactory _connectionFactory;
        public GetAllCategoriesHandler(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Category>> HandleAsync(GetAllCategoriesQuery query)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();

                var sqlQuery = "SELECT * FROM [Category]";
                var categories = await connection.QueryAsync<Category>(sqlQuery);

                if (!categories.Any())
                {
                    throw new InvalidOperationException("O Banco de dados está vazio");
                }

                return categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter categorias: {ex.Message}");
                throw;
            }
        }


    }
}
