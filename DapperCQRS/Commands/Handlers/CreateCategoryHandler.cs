using Dapper;
using DapperCQRS.Data;

namespace DapperCQRS.Commands.Handlers
{
    public class CreateCategoryHandler
    {

        private readonly DbConnectionFactory _connectionFactory;
        public CreateCategoryHandler(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task HandleAsync(CreateCategoryCommand command)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();

                var query = "INSERT INTO [Category] VALUES(@Title, @Slug, @Description)";
                await connection.ExecuteAsync(query, new
                {
                    Title = command.Title,
                    Slug = command.Slug,
                    Description = command.Description,
                });

                Console.WriteLine("Criado com Sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar categoria: {ex.Message}");
            }
        }
    }
}
