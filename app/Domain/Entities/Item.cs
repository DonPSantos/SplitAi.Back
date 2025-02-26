using System.ComponentModel;

namespace Domain.Entities
{
    public class Item : BaseEntity
    {
        public Item()
        {
        }

        /// <summary>
        /// Descrição do item
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Valor unitário
        /// </summary>
        public decimal UnitPrice { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// O que foi consumido desse item
        /// </summary>
        public ICollection<Consumption> Consumptions { get; set; }

        /// <summary>
        /// Propriedade de chave estrangeira da mesa
        /// </summary>
        public Guid TableId { get; set; }

        /// <summary>
        /// Propriedade de navegação da mesa
        /// </summary>
        public Table Table { get; set; }

        /// <summary>
        /// Construtor do item
        /// </summary>
        /// <param name="description">Descrição do item</param>
        /// <param name="unitPrice">Valor unitário</param>
        public Item(Guid tableId, string description, decimal unitPrice)
        {
            TableId = tableId;
            Description = description;
            UnitPrice = unitPrice;
            CreatedAt = DateTimeOffset.UtcNow;
            Consumptions = new List<Consumption>();
        }

        /// <summary>
        /// Atualiza o registro existente com um novo consumo
        /// </summary>
        /// <param name="quantity">Quantidade à adicionar.</param>
        /// <param name="participants">Pessoas que consumiram</param>
        public void AddConsumption(int quantity, List<Person> participants)
        {
            Consumptions.Add(new Consumption(quantity, participants));
        }

        /// <summary>
        /// Unidades consumidas do item
        /// </summary>
        /// <returns>Quantidade em inteira</returns>
        public int TotalQuantity()
        {
            return Consumptions.Sum(c => c.Quantity);
        }

        /// <summary>
        /// Calcula o custo total do item, somando os registros
        /// </summary>
        /// <returns>Valor total do item.</returns>
        public decimal TotalCost()
        {
            return Consumptions.Sum(c => c.Cost(UnitPrice));
        }

        /// <summary>
        /// Distribui cada registro de consumo entre os participantes correspondentes.
        /// Distribui os custos do item em um dicionário (chave: Pessoa, valor: total na mesa).
        /// </summary>
        /// <param name="contas">Dicionario de <Person,Valora a calcular></param>
        public void DistributeForAll(Dictionary<Person, decimal> contas)
        {
            foreach (var consumption in Consumptions)
            {
                decimal custoPorPessoa = consumption.CostPerPerson(UnitPrice);
                foreach (var pessoa in consumption.Participants)
                {
                    contas[pessoa] += custoPorPessoa;
                }
            }
        }

        public decimal DistributeForOne(Person person)
        {
            decimal total = 0m;
            foreach (var consumption in Consumptions.Where(x => x.Participants.Contains(person)))
            {
                total += consumption.CostPerPerson(UnitPrice);
            }
            return total;
        }
    }
}