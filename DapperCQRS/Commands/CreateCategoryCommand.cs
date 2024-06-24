namespace DapperCQRS.Commands
{
    public class CreateCategoryCommand
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
    }
}
