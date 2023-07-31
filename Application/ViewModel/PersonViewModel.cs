namespace Application.ViewModel
{
    public class PersonViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
    }
}