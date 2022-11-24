using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Models
{
    public class AllNews
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Pictures1> Pictures1 { get; set; }//обратная связь один ко многим
        public AllNews()
        {
            Pictures1 = new List<Pictures1>();
        }
        public int Date { get; set; }

    }
}
