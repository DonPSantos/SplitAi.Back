using System.ComponentModel;

namespace Api.Requests.Table
{
    public class CreateTableRequest
    {
        [Description("Id do criador da mesa")]
        public Guid CreatorId { get; set; }

        [Description("Nome da mesa")]
        public string Name { get; set; }

        [Description("Valor da taxa de serviço")]
        public decimal ServiceFee { get; set; }

        [Description("Valor do couvert artístico")]
        public decimal Couvert { get; set; }
    }
}