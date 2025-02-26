using System.ComponentModel;

namespace Application.Features.Queries.GetTableWithTotals.Table
{
    public class TablePerson
    {
        [Description("Id da pessoa")]
        public Guid Id { get; set; }

        [Description("Nome da pessoa")]
        public string Name { get; set; }

        [Description("Valor dos itens consumidos")]
        public decimal ConsumptionValue { get; set; }

        [Description("Valor dos itens consumidos + couvert")]
        public decimal ConsumptionValueWithCouvert { get; set; }

        [Description("Valor dos itens consumidos + taxa de serviço + couvert")]
        public decimal ConsumptionValueWithCouvertAndFee { get; set; }
    }
}