using Dapper;
using DapperCQRS.Data;
using DapperCQRS.Models;

namespace DapperCQRS.Queries.Handlers
{
    public class GetCategoryByIdHandler
    {
        private readonly DbConnectionFactory _connectionFactory;

        public GetCategoryByIdHandler(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Category> HandleAsync(GetCategoryByIdQuery query)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();

                var sqlQuery = "SELECT * FROM [Category] WHERE Id=@Id";
                var category = await connection.QueryFirstOrDefaultAsync<Category>(sqlQuery, new { query.Id});

                if (category == null)
                {
                    throw new KeyNotFoundException($"O id {query.Id} não existe no banco de dados");
                }

                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter categoria: {ex.Message}");
                throw;
            }
        }
    }
}
