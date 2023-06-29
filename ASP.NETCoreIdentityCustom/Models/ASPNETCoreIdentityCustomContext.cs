using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ASP.NETCoreIdentityCustom.Models
{
    public partial class ASPNETCoreIdentityCustomContext : DbContext
    {
        public ASPNETCoreIdentityCustomContext()
        {
        }

        public ASPNETCoreIdentityCustomContext(DbContextOptions<ASPNETCoreIdentityCustomContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Tarea> Tareas { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Proyecto> Proyectos { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DIDIER\\SQLEXPRESS; Initial Catalog=ASPNETCoreIdentityCustom; Integrated security=True; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasIndex(e => e.Cedula, "UQ__Clientes__415B7BE534146496")
                    .IsUnique();

                entity.HasIndex(e => e.Cedula, "UQ__Clientes__415B7BE5EDB85B16")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CedulaCliente)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cedula_cliente");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoRiesgo).HasColumnName("Tipo_Riesgo");

                entity.HasOne(d => d.CedulaClienteNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasPrincipalKey(p => p.Cedula)
                    .HasForeignKey(d => d.CedulaCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proyectos__cedul__123EB7A3");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__A3C02A107131DD47");

                entity.HasIndex(e => e.NombreCategoria, "UQ__Categori__788BF0FA55AC636F")
                    .IsUnique();

                entity.Property(e => e.ColorCategoria)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("colorCategoria");

                entity.Property(e => e.NombreCategoria)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombreCategoria");
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(e => e.IdTarea)
                    .HasName("PK__Tareas__EADE9098A8305785");

                entity.Property(e => e.Categoria)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("categoria");

                entity.Property(e => e.DescripcionTarea)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("descripcionTarea");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaFin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaInicio");

                entity.Property(e => e.NombreTarea)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombreTarea");

                entity.HasOne(d => d.CategoriaNavigation)
                    .WithMany(p => p.Tareas)
                    .HasPrincipalKey(p => p.NombreCategoria)
                    .HasForeignKey(d => d.Categoria)
                    .HasConstraintName("FK__Tareas__categori__72C60C4A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
