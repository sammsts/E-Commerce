using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Infra.Data.Context
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Enderecos> Enderecos { get; set; }
        public virtual DbSet<Produtos> Produtos { get; set; }
        public virtual DbSet<Carrinho> Carrinho { get; set; }
        public virtual DbSet<Pedidos> Pedidos { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcommerceContext).Assembly);

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.Usu_id);
            });

            modelBuilder.Entity<Enderecos>(entity =>
            {
                entity.HasKey(e => e.End_id);
            });

            modelBuilder.Entity<Produtos>(entity =>
            {
                entity.HasKey(e => e.Prd_id);
            });

            modelBuilder.Entity<Pedidos>(entity =>
            {
                entity.HasKey(e => e.Ped_Id);
            });

            modelBuilder.Entity<Carrinho>(entity =>
            {
                entity.HasKey(e => e.Car_Id);
            });
        }
    }
}
