using System.ComponentModel;

namespace Api.Requests.Table
{
    public class UpdateTableItemRequest
    {
        [Description("Descrição do item")]
        public string? Description { get; set; }

        [Description("Valor unitário")]
        public decimal? UnitPrice { get; set; }
    }
}