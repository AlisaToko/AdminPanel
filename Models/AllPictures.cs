using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Models
{
    public class AllPictures : Controller
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int Pictures1Id { get; set; } //foreign key
        public Pictures1? Pictures1 { get; set; } //навигац поле

        public string? Path { get; set; }
    }
}
