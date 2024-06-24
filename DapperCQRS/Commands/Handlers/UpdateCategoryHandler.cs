using Dapper;
using DapperCQRS.Data;
using DapperCQRS.Models;
using Microsoft.Data.SqlClient;

namespace DapperCQRS.Commands.Handlers
{
    public class UpdateCategoryHandler
    {
        private readonly DbConnectionFactory _connectionFactory;
        public UpdateCategoryHandler(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task HandleAsync(UpdateCategoryCommand command)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();

                var existingQuery = await connection.QueryFirstOrDefaultAsync<Category>(
                    "SELECT * FROM [Category] WHERE Id=@Id", new { command.Id });

                if (existingQuery == null)
                {
                    throw new KeyNotFoundException($"Categoria com ID {command.Id} não foi encontrado");
                }

                var query = @"
                    UPDATE [Category]
                    SET Title = @Title, Slug = @Slug, Description = @Description
                    WHERE Id=@Id
                ";

                await connection.ExecuteAsync(query, command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar a categoria: {ex.Message}");
                throw;
            }
        }
    }
}
