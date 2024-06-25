using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Models
{
    public partial class EcommerceContext : DbContext
    {
        public EcommerceContext() { }

        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }

        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Enderecos> Enderecos { get; set; }
        public virtual DbSet<Produtos> Produtos { get; set; }
        public virtual DbSet<Carrinho> Carrinho { get; set; }
        public virtual DbSet<Pedidos> Pedidos { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.Usu_id);

                entity.HasMany(e => e.Enderecos)
                      .WithOne(e => e.Usuarios)
                      .HasForeignKey(e => e.Usu_id)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Enderecos>(entity =>
            {
                entity.HasKey(e => e.End_id);

                entity.HasOne(d => d.Usuarios)
                      .WithMany(p => p.Enderecos)
                      .HasForeignKey(d => d.Usu_id)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Pedidos>(entity =>
            {
                entity.HasKey(e => e.Ped_Id);

                entity.HasOne(d => d.Produto)
                      .WithMany()
                      .HasForeignKey(d => d.Prd_Id)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Usuario)
                      .WithMany()
                      .HasForeignKey(d => d.Usu_Id)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Endereco)
                      .WithMany()
                      .HasForeignKey(d => d.End_Id)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Carrinho>(entity =>
            {
                entity.HasKey(e => e.Car_Id);

                entity.HasOne(d => d.Pedido)
                      .WithMany()
                      .HasForeignKey(d => d.Ped_Id)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Produto)
                      .WithMany()
                      .HasForeignKey(d => d.Prd_Id)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Usuario)
                      .WithMany()
                      .HasForeignKey(d => d.Usu_Id)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Endereco)
                      .WithMany()
                      .HasForeignKey(d => d.End_Id)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
