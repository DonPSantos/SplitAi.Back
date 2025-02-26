using Application.Features.Queries.GetPersonByEmail.Result;
using Application.Features.Queries.GetPersonTables.Result;
using Application.Features.Queries.GetPersonWithTotals.Person;
using Application.Features.Queries.GetTableWithTotals.Table;
using AutoMapper;
using Domain.Entities;

namespace Application.Configurations.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Table, PersonTableResult>();
            CreateMap<Table, TableWithDetailsResult>();
            CreateMap<Table, PersonTableResult>();

            CreateMap<Person, PersonResult>();
            CreateMap<Person, TablePerson>();
            CreateMap<Person, PersonWithTotalsResult>();

            CreateMap<Item, TablePerson>();
            CreateMap<Item, PersonItemsResult>();
            CreateMap<Item, TableItem>();
        }
    }
}