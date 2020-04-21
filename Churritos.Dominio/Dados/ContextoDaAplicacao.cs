using Churritos.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Churritos.Dominio.Dados
{
    public class ContextoDaAplicação : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Cobertura> Coberturas { get; set; }
        public DbSet<Recheio> Recheios { get; set; }
        public DbSet<Item> Itens { get; set; }
        
        public ContextoDaAplicação(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) =>
            builder.ApplyConfigurationsFromAssembly(typeof(ContextoDaAplicação).Assembly);
    }
}