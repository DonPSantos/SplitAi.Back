using System.ComponentModel;

namespace Api.Requests.Person
{
    public class CreatePersonRequest
    {
        [Description("Nome da pessoa")]
        public string Name { get; set; }

        [Description("Email da pessoa")]
        public string Email { get; set; }
    }
}