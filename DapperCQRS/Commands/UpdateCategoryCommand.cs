﻿namespace DapperCQRS.Commands
{
    public class UpdateCategoryCommand
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
    }
}
