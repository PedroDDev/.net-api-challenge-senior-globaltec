namespace Domain.DTOs
{
    public class PersonDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
    }
}