namespace AdminPanel.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        
        public int? CompanyId { get; set; }//foreign key 
        public Company? Company { get; set; }//навигационное поле

    }
}
