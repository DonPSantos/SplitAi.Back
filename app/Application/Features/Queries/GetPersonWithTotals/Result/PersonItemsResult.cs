using System.ComponentModel;

namespace Application.Features.Queries.GetPersonWithTotals.Person
{
    public class PersonItemsResult
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}