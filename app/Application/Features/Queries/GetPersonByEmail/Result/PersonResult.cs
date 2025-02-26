namespace Application.Features.Queries.GetPersonByEmail.Result
{
    public class PersonResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}