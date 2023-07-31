using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Person
    {
        private const string nameValidationString = "Name Cannot be null or empty.";

        public Person() { }
        public Person(string? name, string cpf, string uf, DateTime birthdate)
        {
            Name = name ?? throw new ArgumentNullException(nameValidationString, nameof(name));
            Cpf = cpf;
            Uf = uf;
            Birthdate = birthdate;
        }

        [Key]
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;
        public string Cpf { get; private set; } = string.Empty;
        public string Uf { get; private set; } = string.Empty;
        public DateTime Birthdate { get; private set; }

        public void UpdatePerson(string name, string cpf, string uf, DateTime birthdate)
        {
            Name = name ?? throw new ArgumentNullException(nameValidationString, nameof(name));
            Cpf = cpf;
            Uf = uf;
            Birthdate = birthdate;
        }
    }
}