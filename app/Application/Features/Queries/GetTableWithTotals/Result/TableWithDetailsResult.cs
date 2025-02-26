using Domain.Entities;
using System.ComponentModel;

namespace Application.Features.Queries.GetTableWithTotals.Table
{
    public class TableWithDetailsResult
    {
        public Guid Id { get; set; }

        [Description("Nome da mesa")]
        public string Name { get; set; }

        [Description("Código da mesa")]
        public string TableCode { get; set; }

        [Description("Taxa de serviço")]
        public decimal ServiceFee { get; set; }

        [Description("Valor do couvert artístico individual")]
        public decimal Couvert { get; set; }

        [Description("Valor apenas dos itens consumidos")]
        public decimal TotalPartial { get; set; }

        [Description("Valor dos itens consumidos + couvert da mesa inteira")]
        public decimal TotalPartialWithCoverCharge { get; set; }

        [Description("Total com serviço e couvert")]
        public decimal TotalWithServiceAndCoverCharge { get; set; }

        [Description("Pessoas da mesa")]
        public List<TablePerson> People { get; set; } = new List<TablePerson>();

        [Description("Itens da mesa")]
        public List<TableItem> Items { get; set; } = new List<TableItem>();
    }
}