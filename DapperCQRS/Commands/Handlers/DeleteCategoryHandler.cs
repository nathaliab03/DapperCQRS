using Dapper;
using DapperCQRS.Data;
using DapperCQRS.Models;

namespace DapperCQRS.Commands.Handlers
{
    public class DeleteCategoryHandler
    {
        private readonly DbConnectionFactory _connectionFactory;

        public DeleteCategoryHandler(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task HandleAsync(DeleteCategoryCommand command)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();

                var existingQuery = connection.QueryFirstOrDefaultAsync<Category>(
                    "SELECT * FROM [Category] WHERE Id=@id", new { command.Id});

                if (existingQuery == null)
                {
                    throw new KeyNotFoundException($"Categoria com ID {command.Id} não foi encontrado");
                }

                var query = @"
                    DELETE FROM [Category]
                    WHERE Id=@Id
                ";

                await connection.ExecuteAsync(query, new { command.Id });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar a categoria: {ex.Message}");
                throw;
            }
        }
    }
}
