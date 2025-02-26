using System.ComponentModel;
using System.Text;

namespace Domain.Entities
{
    public class Table : BaseEntity
    {
        public Table()
        {
        }

        /// <summary>
        /// Nome da mesa
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Couvert artístico
        /// </summary>
        public decimal Couvert { get; private set; }

        /// <summary>
        /// Taxa de serviço
        /// </summary>
        public decimal ServiceFee { get; private set; }

        /// <summary>
        /// Código da mesa
        /// </summary>
        public string TableCode { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// Pessoas da mesa
        /// </summary>
        public List<Person> People { get; set; }

        /// <summary>
        /// Itens ta mesa
        /// </summary>
        public List<Item> Items { get; set; }

        public Table(string name, decimal serviceFee, decimal couvert)
        {
            Name = name;
            ServiceFee = serviceFee;
            Couvert = couvert;
            CreatedAt = DateTimeOffset.UtcNow;
            People = new List<Person>();
            Items = new List<Item>();
        }

        /// <summary>
        /// Adiciona um consumo para um item. Se o item já existir, atualiza seu registro
        /// </summary>
        /// <param name="description">Descrição do item</param>
        /// <param name="unitPrice">Preço unitário</param>
        /// <param name="quantity">Quantidade</param>
        /// <param name="participants">Pessoas que consumiram</param>
        public void AddConsumption(string description, decimal unitPrice, int quantity, List<Person> participants)
        {
            var item = Items.FirstOrDefault(i => i.Description.Trim().Equals(description, StringComparison.OrdinalIgnoreCase)
                                                   && i.UnitPrice == unitPrice);
            if (item == null)
            {
                item = new Item(Id, description.Trim(), unitPrice);
                Items.Add(item);
            }
            item.AddConsumption(quantity, participants);
        }

        /// <summary>
        /// Calcula o total dos consumos
        /// </summary>
        /// <returns>O valor consumido</returns>
        public decimal CalculateTotalConsumption()
        {
            return Items.Sum(item => item.TotalCost());
        }

        /// <summary>
        /// Retorna o valor itens consumidos por pessoa
        /// </summary>
        /// <returns></returns>
        public Dictionary<Person, decimal> GetListPersonConsumption()
        {
            var contas = People.ToDictionary(p => p, p => 0.0m);
            foreach (var item in Items)
            {
                item.DistributeForAll(contas);
            }
            return contas;
        }

        public decimal GetPersonConsumption(Person person)
        {
            decimal total = 0m;
            foreach (var item in Items)
            {
                total += item.DistributeForOne(person);
            }
            return total;
        }

        /// <summary>
        /// Retorna o valor itens consumidos por pessoa + couvert
        /// </summary>
        /// <returns></returns>
        public Dictionary<Person, decimal> GetListPersonConsumptionWithCouvert()
        {
            var contas = GetListPersonConsumption();
            foreach (var pessoa in People)
            {
                contas[pessoa] += Couvert;
            }
            return contas;
        }

        public decimal GetPersonConsumptionWithCouvert(Person person)
        {
            decimal total = GetPersonConsumption(person);

            return total += Couvert;
        }

        /// <summary>
        /// Retorna o valor itens consumidos por pessoa + couvert + taxa de serviço
        /// </summary>
        /// <returns></returns>
        public Dictionary<Person, decimal> GetListPersonConsumptionWithCouvertAndFee()
        {
            // Obtemos o consumo apenas dos itens.
            var billingItems = GetListPersonConsumption();
            decimal totalItens = billingItems.Values.Sum();
            // Calcula a taxa de serviço sobre o total dos itens.
            decimal taxaServicoTotal = totalItens * (ServiceFee / 100.0m);

            // Cria um dicionário para os valores finais.
            Dictionary<Person, decimal> contasFinais = new Dictionary<Person, decimal>();
            foreach (var pessoa in People)
            {
                decimal consumo = billingItems[pessoa];
                decimal taxaPessoa = (totalItens > 0) ? (consumo / totalItens) * taxaServicoTotal : 0.0m;
                // Primeiro o consumo acrescido da taxa de serviço e depois o couvert, sem incidência de taxa.
                contasFinais[pessoa] = consumo + taxaPessoa + Couvert;
            }
            return contasFinais;
        }

        public decimal GetPersonConsumptionWithCouvertAndFee(Person person)
        {
            // Obtemos o consumo apenas dos itens.
            decimal totalItens = GetPersonConsumption(person);
            // Calcula a taxa de serviço sobre o total dos itens.
            decimal taxaServicoTotal = totalItens * (ServiceFee / 100.0m);

            return totalItens + taxaServicoTotal + Couvert;
        }

        /// <summary>
        /// Gera o código da mesa
        /// </summary>
        public void GenerateTableCode()
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 6; i++)
            {
                int index = random.Next(caracteres.Length);
                result.Append(caracteres[index]);
            }

            TableCode = result.ToString();
        }
    }
}