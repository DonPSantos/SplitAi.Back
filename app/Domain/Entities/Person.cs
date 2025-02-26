namespace Domain.Entities
{
    public class Person : BaseEntity
    {
        public Person()
        {
        }

        /// <summary>
        /// Nome da pessoa
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Email da pessoa
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Propriedade de navegação para a mesa
        /// </summary>
        public ICollection<Table> Tables { get; set; }

        /// <summary>
        /// Propriedade de navegação para os consumidos pela pessoa
        /// </summary>
        public ICollection<Consumption> Consumptions { get; set; }

        public Person(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}