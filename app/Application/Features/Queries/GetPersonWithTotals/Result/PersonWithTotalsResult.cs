using System.ComponentModel;

namespace Application.Features.Queries.GetPersonWithTotals.Person
{
    public class PersonWithTotalsResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        [Description("Total do couvert artístico da mesa")]
        public decimal CoverCharge { get; set; }

        [Description("Total dos itens consumidos")]
        public decimal TotalPartial { get; set; }

        [Description("Valor dos itens consumidos + valor total do couvert artístico")]
        public decimal TotalPartialWithCoverCharge { get; set; }

        [Description("Valor total com couvert artístico e taxa de serviço, lembrando que não é cobrada taxa de serviço encima do couvert")]
        public decimal TotalWithServiceAndCoverCharge { get; set; }

        public List<PersonItemsResult> Items { get; set; } = new List<PersonItemsResult>();
    }
}