using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class FlunaControlFarmaciaContext : DbContext
{
    public FlunaControlFarmaciaContext()
    {
    }

    public FlunaControlFarmaciaContext(DbContextOptions<FlunaControlFarmaciaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetallesPedido> DetallesPedidos { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VJ5G6F0\\FLUNA; Database= FLunaControlFarmacia; Trusted_Connection=True; TrustServerCertificate=True; User ID=sa; Password=Password1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetallesPedido>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Detalles__E43646A5217BAE82");

            entity.ToTable("DetallesPedido");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Sku).HasColumnName("SKU");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdMedicamentoNavigation).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.IdMedicamento)
                .HasConstraintName("FK__DetallesP__IdMed__1B0907CE");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__DetallesP__IdPed__1A14E395");
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.IdMedicamento).HasName("PK__Medicame__AC96376E6AFBE0ED");

            entity.Property(e => e.IdMedicamento).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Sku).HasColumnName("SKU");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedidos__9D335DC354498B47");

            entity.Property(e => e.Cliente)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
