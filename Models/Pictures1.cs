using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Models
{
    public class Pictures1
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int AllNewsId { get; set; } //foreign key
        public AllNews? AllNews { get; set; } //навигац поле

        public string? Path { get; set; }

    }
}
