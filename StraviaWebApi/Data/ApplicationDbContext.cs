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

    public virtual DbSet<Categorium> Categoria { get; set; }

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
            entity.HasKey(e => e.ActividadId).HasName("PK__Activida__981483F0B19059E6");

            entity.ToTable("Actividad");

            entity.Property(e => e.ActividadId)
                .ValueGeneratedNever()
                .HasColumnName("ActividadID");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Ruta)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoActividad)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(15)
                .IsUnicode(false);


        });

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.NombreCarrera).HasName("PK__Carrera__6D03DD5D50591ADB");

            entity.ToTable("Carrera");

            entity.Property(e => e.NombreCarrera)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaCarrera).HasColumnType("date");
            entity.Property(e => e.Modalidad)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Recorrido)
                .HasMaxLength(20)
                .IsUnicode(false);

        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => new { e.NombreCategoria, e.NombreCarrera }).HasName("PK__Categori__64CF834BE677D3E9");

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
            entity.HasKey(e => e.ComentarioId).HasName("PK__Comentar__F1844958407C7C38");

            entity.ToTable("Comentario");

            entity.Property(e => e.ComentarioId)
                .ValueGeneratedNever()
                .HasColumnName("ComentarioID");
            entity.Property(e => e.ActividadId).HasColumnName("ActividadID");
            entity.Property(e => e.Contenido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaPublicacion).HasColumnType("date");
            entity.Property(e => e.Usuario)
                .HasMaxLength(15)
                .IsUnicode(false);


        });

        modelBuilder.Entity<CuentasBancaria>(entity =>
        {
            entity.HasKey(e => new { e.NombreCarrera, e.NumeroCuenta }).HasName("PK__CuentasB__D300485AEBD18B68");

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
            entity.HasKey(e => e.GrupoId).HasName("PK__Grupo__556BF060BAD88617");

            entity.ToTable("Grupo");

            entity.Property(e => e.GrupoId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("GrupoID");
            entity.Property(e => e.Administrador)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Creacion).HasColumnType("date");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreGrupo)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Patrocinador>(entity =>
        {
            entity.HasKey(e => e.NombreComercial).HasName("PK__Patrocin__053E6903E9205FA3");

            entity.ToTable("Patrocinador");

            entity.Property(e => e.NombreComercial)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Logo)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NombreLegal)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reto>(entity =>
        {
            entity.HasKey(e => e.NombreReto).HasName("PK__Reto__71D830CDE32B58D4");

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
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoActividad)
                .HasMaxLength(20)
                .IsUnicode(false);

        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Usuario1).HasName("PK__Usuario__E3237CF6B16CB3D6");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Contraseña, "UQ__Usuario__A961D9D27FF451BA").IsUnique();

            entity.Property(e => e.Usuario1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Usuario");
            entity.Property(e => e.Apellido1)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Edad).HasComputedColumnSql("(datediff(year,[Fecha_nacimiento],getdate()))", false);
            entity.Property(e => e.FechaActual)
                .HasComputedColumnSql("(getdate())", false)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_actual");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("Fecha_nacimiento");
            entity.Property(e => e.Foto)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
