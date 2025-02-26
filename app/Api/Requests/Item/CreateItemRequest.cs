using System.ComponentModel;

namespace Api.Requests.Item
{
    public class CreateItemRequest
    {
        [Description("Descrição do item")]
        public string Name { get; set; }

        [Description("Valor unitário do item")]
        public decimal Value { get; set; }

        [Description("Quantidade do item")]
        public int Quantity { get; set; }

        [Description("Lista de IDs dos consumidores")]
        public List<Guid> ConsumersIds { get; set; } = new List<Guid>();
    }
}