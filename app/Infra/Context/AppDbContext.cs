using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Consumption> Consumption { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Verifica se a entidade herda da classe base (substitua 'SuaClasseBase' pelo nome real)
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    // Ignora a propriedade 'IsValid' para esta entidade
                    modelBuilder.Entity(entityType.ClrType).Ignore(nameof(BaseEntity.Valid));
                    modelBuilder.Entity(entityType.ClrType).Ignore(nameof(BaseEntity.Invalid));
                    modelBuilder.Entity(entityType.ClrType).Ignore(nameof(BaseEntity.ValidationResult));
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public void InitializeDatabase()
        {
            // Verifica se o banco de dados existe e cria, se necessário
            this.Database.EnsureCreated();

            // Aplica as migrações pendentes
            this.Database.Migrate();
        }
    }
}