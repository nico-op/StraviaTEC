using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StraviaWebApi.Models;
namespace StraviaWebApi.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actividad> Actividads { get; set; }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<CuentasBancaria> CuentasBancarias { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Patrocinador> Patrocinadors { get; set; }

    public virtual DbSet<Reto> Retos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-45ERV0H\\SQLEXPRESS04;Database=StraviaTEC;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividad>(entity =>
        {
            entity.HasKey(e => e.ActividadId).HasName("PK__Activida__981483F0B7F35A05");

            entity.ToTable("Actividad");

            entity.Property(e => e.ActividadId)
                .ValueGeneratedNever()
                .HasColumnName("ActividadID");
            entity.Property(e => e.FechaHora).HasColumnType("date");
            entity.Property(e => e.NombreUsuario)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Ruta)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoActividad)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);


        });

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.NombreCarrera).HasName("PK__Carrera__6D03DD5DF878A922");

            entity.ToTable("Carrera");

            entity.Property(e => e.NombreCarrera)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaCarrera).HasColumnType("date");
            entity.Property(e => e.Modalidad)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Recorrido)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => new { e.NombreCategoria, e.NombreCarrera }).HasName("PK__Categori__64CF834BE907263B");

            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NombreCarrera)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DescripcionCategoria)
                .HasMaxLength(100)
                .IsUnicode(false);


        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.ComentarioId).HasName("PK__Comentar__F184495894AAEB25");

            entity.ToTable("Comentario");

            entity.Property(e => e.ComentarioId)
                .ValueGeneratedNever()
                .HasColumnName("ComentarioID");
            entity.Property(e => e.ActividadId).HasColumnName("ActividadID");
            entity.Property(e => e.Contenido)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaPublicacion).HasColumnType("date");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(15)
                .IsUnicode(false);

            
        });

        modelBuilder.Entity<CuentasBancaria>(entity =>
        {
            entity.HasKey(e => new { e.NombreCarrera, e.NumeroCuenta }).HasName("PK__CuentasB__D300485A9E9A19E7");

            entity.Property(e => e.NombreCarrera)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroCuenta)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NombreBanco)
                .HasMaxLength(50)
                .IsUnicode(false);


        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.GrupoId).HasName("PK__Grupo__556BF060CA8B80FC");

            entity.ToTable("Grupo");

            entity.Property(e => e.GrupoId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("GrupoID");
            entity.Property(e => e.Administrador)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Creacion).HasColumnType("date");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreGrupo)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Patrocinador>(entity =>
        {
            entity.HasKey(e => e.NombreComercial).HasName("PK__Patrocin__053E6903E55AF032");

            entity.ToTable("Patrocinador");

            entity.Property(e => e.NombreComercial)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Logo)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NombreLegal)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reto>(entity =>
        {
            entity.HasKey(e => e.NombreReto).HasName("PK__Reto__71D830CDD67A365E");

            entity.ToTable("Reto");

            entity.Property(e => e.NombreReto)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Altitud)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Fondo)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Privacidad)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoActividad)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);


        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.NombreUsuario).HasName("PK__Usuario__6B0F5AE15B77F399");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Contrasena, "UQ__Usuario__A96DE151607E09F5").IsUnique();

            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Apellido1)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Edad).HasComputedColumnSql("(datediff(year,[Fecha_nacimiento],getdate()))", false);
            entity.Property(e => e.FechaActual)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_actual");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("Fecha_nacimiento");
            entity.Property(e => e.Foto)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nacionalidad)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
