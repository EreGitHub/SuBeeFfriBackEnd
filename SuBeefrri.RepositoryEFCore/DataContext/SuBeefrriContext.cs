using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SuBeefrri.Core.Entities;

namespace SuBeefrri.Contexts.DataContext
{
    public partial class SuBeefrriContext : DbContext
    {
        public SuBeefrriContext()
        {
        }

        public SuBeefrriContext(DbContextOptions<SuBeefrriContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cobro> Cobros { get; set; } = null!;
        public virtual DbSet<DetallePedido> DetallePedidos { get; set; } = null!;
        public virtual DbSet<Entrega> Entregas { get; set; } = null!;
        public virtual DbSet<OrderPedido> OrderPedidos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;
        public virtual DbSet<Sucursal> Sucursals { get; set; } = null!;
        public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cobro>(entity =>
            {
                entity.HasKey(e => e.IdCobro)
                    .HasName("pk_Cobro");

                entity.ToTable("Cobro");

                entity.Property(e => e.FechaCobro).HasColumnType("datetime");

                entity.HasOne(d => d.IdEntregaNavigation)
                    .WithMany(p => p.Cobros)
                    .HasForeignKey(d => d.IdEntrega)
                    .HasConstraintName("fk_Entrega_Cobro");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Cobros)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("fk_Usuario_Cobro");
            });

            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.HasKey(e => e.IdDetalle)
                    .HasName("pk_DetallePedido");

                entity.ToTable("DetallePedido");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("fk_Pedido_DetallePedido");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("fk_Producto_DetallePedido");
            });

            modelBuilder.Entity<Entrega>(entity =>
            {
                entity.HasKey(e => e.IdEntrega)
                    .HasName("pk_Entrega");

                entity.ToTable("Entrega");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdDetalleNavigation)
                    .WithMany(p => p.Entregas)
                    .HasForeignKey(d => d.IdDetalle)
                    .HasConstraintName("fk_Detalle_Entrega");
            });

            modelBuilder.Entity<OrderPedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido)
                    .HasName("pk_OrderPedido");

                entity.ToTable("OrderPedido");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.OrderPedidos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("fk_Usuario_OrderPedido");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("pk_Persona");

                entity.ToTable("Persona");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ci)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionFoto)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("pk_Producto");

                entity.ToTable("Producto");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Peso).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.PrecioEntrega)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("Precio_Entrega");

                entity.Property(e => e.PrecioVenta)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("Precio_Venta");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("fk_Proveedor_Producto");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("pk_Proveedor");

                entity.ToTable("Proveedor");

                entity.Property(e => e.Nit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.IdSucursal)
                    .HasName("pk_Sucursal");

                entity.ToTable("Sucursal");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("pk_TipoUsuario");

                entity.ToTable("TipoUsuario");

                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("pk_Usuario");

                entity.ToTable("Usuario");

                entity.Property(e => e.ClaveUs)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordUs)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("fk_Persona_Usuario");

                entity.HasOne(d => d.IdSucursalNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdSucursal)
                    .HasConstraintName("fk_Sucursal_Usuario");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("fk_Tipo_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
