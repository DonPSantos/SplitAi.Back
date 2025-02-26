namespace Domain.Entities
{
    public class Consumption : BaseEntity
    {
        public Consumption()
        {
        }

        /// <summary>
        /// Quantidade consumida
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Pessoas que consumiram
        /// </summary>
        public List<Person> Participants { get; set; }

        /// <summary>
        /// Propriedade de chave estrangeira do item
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// Propriedade de navegação do item
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// Construtor do consumo do item
        /// </summary>
        /// <param name="quantity">Quantidade consumida</param>
        /// <param name="participants">Pessoas que consumiram</param>
        public Consumption(int quantity, List<Person> participants)
        {
            Quantity = quantity;
            Participants = participants;
        }

        /// <summary>
        /// Calcula o custo deste registro com base no preço unitário.
        /// </summary>
        /// <param name="unitPrice">Valor unitário</param>
        /// <returns>Valor total</returns>
        public decimal Cost(decimal unitPrice)
        {
            return Quantity * unitPrice;
        }

        /// <summary>
        /// Distribui o custo deste registro por pessoa.
        /// </summary>
        /// <param name="unitPrice">Valor unitário</param>
        /// <returns>Valor total por pessoa</returns>
        public decimal CostPerPerson(decimal unitPrice)
        {
            return Cost(unitPrice) / Participants.Count;
        }
    }
}