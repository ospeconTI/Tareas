// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OSPeConTI.Tareas.Infrastructure;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(TareasContext))]
    [Migration("20230301193823_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OSPeConTI.Tareas.Domain.Entities.Link", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaUpdate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("TareaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioAlta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioUpdate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TareaId");

                    b.ToTable("Link");
                });

            modelBuilder.Entity("OSPeConTI.Tareas.Domain.Entities.Sector", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UsuarioAlta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioUpdate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sector", "dbo");
                });

            modelBuilder.Entity("OSPeConTI.Tareas.Domain.Entities.Tarea", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<int>("Alerta")
                        .HasColumnType("int");

                    b.Property<DateTime>("Creacion")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("EjecutorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Instrucciones")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ReferenciaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TareaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioAlta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioUpdate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Vencimiento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VigenteDesde")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreadorId");

                    b.HasIndex("EjecutorId");

                    b.HasIndex("TareaId");

                    b.ToTable("Tarea", "dbo");
                });

            modelBuilder.Entity("OSPeConTI.Tareas.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaUpdate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Identificacion")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SectorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UsuarioAlta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioUpdate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SectorId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("OSPeConTI.Tareas.Domain.Entities.Link", b =>
                {
                    b.HasOne("OSPeConTI.Tareas.Domain.Entities.Tarea", null)
                        .WithMany("Adjuntos")
                        .HasForeignKey("TareaId");
                });

            modelBuilder.Entity("OSPeConTI.Tareas.Domain.Entities.Tarea", b =>
                {
                    b.HasOne("OSPeConTI.Tareas.Domain.Entities.Sector", "Creador")
                        .WithMany()
                        .HasForeignKey("CreadorId");

                    b.HasOne("OSPeConTI.Tareas.Domain.Entities.Sector", "Ejecutor")
                        .WithMany()
                        .HasForeignKey("EjecutorId");

                    b.HasOne("OSPeConTI.Tareas.Domain.Entities.Tarea", null)
                        .WithMany("Consecuencias")
                        .HasForeignKey("TareaId");

                    b.Navigation("Creador");

                    b.Navigation("Ejecutor");
                });

            modelBuilder.Entity("OSPeConTI.Tareas.Domain.Entities.Usuario", b =>
                {
                    b.HasOne("OSPeConTI.Tareas.Domain.Entities.Sector", null)
                        .WithMany("Usuarios")
                        .HasForeignKey("SectorId");
                });

            modelBuilder.Entity("OSPeConTI.Tareas.Domain.Entities.Sector", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("OSPeConTI.Tareas.Domain.Entities.Tarea", b =>
                {
                    b.Navigation("Adjuntos");

                    b.Navigation("Consecuencias");
                });
#pragma warning restore 612, 618
        }
    }
}
