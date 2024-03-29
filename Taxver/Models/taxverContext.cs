﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Taxver.Models
{
    public partial class taxverContext : DbContext
    {
        private string connectionString;
        public taxverContext(string _connectionString)
        {
            this.connectionString = _connectionString;
        }

        public taxverContext(DbContextOptions<taxverContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conductor> Conductor { get; set; }
        public virtual DbSet<Evaluacion> Evaluacion { get; set; }
        public virtual DbSet<FechasSeguro> FechasSeguro { get; set; }
        public virtual DbSet<Mantenimiento> Mantenimiento { get; set; }
        public virtual DbSet<ObjetosPerdidos> ObjetosPerdidos { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Posicionconductor> Posicionconductor { get; set; }
        public virtual DbSet<Seguro> Seguro { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }
        public virtual DbSet<Viaje> Viaje { get; set; }
        public virtual DbSet<Viajeposicion> Viajeposicion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conductor>(entity =>
            {
                entity.HasKey(e => e.IdConductor);

                entity.ToTable("conductor");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("fk_Conductor_Persona_idx");

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("fk_Conductor_Usuario_idx");

                entity.HasIndex(e => e.IdVehiculo)
                    .HasName("fk_Conductor_Vehiculo_idx");

                entity.Property(e => e.IdConductor)
                    .HasColumnName("idConductor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Foto).HasColumnType("varchar(200)");

                entity.Property(e => e.IdPersona)
                    .HasColumnName("idPersona")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("idUsuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdVehiculo)
                    .HasColumnName("idVehiculo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.Property(e => e.Tarifa).HasColumnType("int(11)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Conductor)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("fk_Conductor_Persona");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Conductor)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("fk_Conductor_Usuario");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.Conductor)
                    .HasForeignKey(d => d.IdVehiculo)
                    .HasConstraintName("fk_Conductor_Vehiculo");
            });

            modelBuilder.Entity<Evaluacion>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacion);

                entity.ToTable("evaluacion");

                entity.HasIndex(e => e.IdCliente)
                    .HasName("fk_Evaluacion_Usuario_idx");

                entity.HasIndex(e => e.IdViaje)
                    .HasName("fk_Evaluacion_Viaje_idx");

                entity.Property(e => e.IdEvaluacion)
                    .HasColumnName("idEvaluacion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Comentarios).HasColumnType("varchar(45)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(45)");

                entity.Property(e => e.IdCliente)
                    .HasColumnName("idCliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdViaje)
                    .HasColumnName("idViaje")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.Property(e => e.Valoracion).HasColumnType("int(11)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Evaluacion)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("fk_Evaluacion_Usuario");

                entity.HasOne(d => d.IdViajeNavigation)
                    .WithMany(p => p.Evaluacion)
                    .HasForeignKey(d => d.IdViaje)
                    .HasConstraintName("fk_Evaluacion_Viajeid");
            });

            modelBuilder.Entity<FechasSeguro>(entity =>
            {
                entity.HasKey(e => e.IdFechasSeguro);

                entity.ToTable("fechas_seguro");

                entity.HasIndex(e => e.IdSeguro)
                    .HasName("fk_FS_Seguro_idx");

                entity.HasIndex(e => e.IdVehiculo)
                    .HasName("fk_FS_Vehiculo_idx");

                entity.Property(e => e.IdFechasSeguro)
                    .HasColumnName("idFechas_Seguro")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FechaFinal)
                    .HasColumnName("Fecha_Final")
                    .HasColumnType("date");

                entity.Property(e => e.FechaInicio)
                    .HasColumnName("Fecha_Inicio")
                    .HasColumnType("date");

                entity.Property(e => e.IdSeguro)
                    .HasColumnName("idSeguro")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdVehiculo)
                    .HasColumnName("idVehiculo")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdSeguroNavigation)
                    .WithMany(p => p.FechasSeguro)
                    .HasForeignKey(d => d.IdSeguro)
                    .HasConstraintName("fk_FS_Seguro");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.FechasSeguro)
                    .HasForeignKey(d => d.IdVehiculo)
                    .HasConstraintName("fk_FS_Vehiculo");
            });

            modelBuilder.Entity<Mantenimiento>(entity =>
            {
                entity.HasKey(e => e.IdMantenimiento);

                entity.ToTable("mantenimiento");

                entity.HasIndex(e => e.IdVehiculo)
                    .HasName("fk_Mantenimiento_Vehiculo_idx");

                entity.Property(e => e.IdMantenimiento)
                    .HasColumnName("idMantenimiento")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(45)");

                entity.Property(e => e.Detalles).HasColumnType("varchar(45)");

                entity.Property(e => e.IdVehiculo)
                    .HasColumnName("idVehiculo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.Mantenimiento)
                    .HasForeignKey(d => d.IdVehiculo)
                    .HasConstraintName("fk_Mantenimiento_Vehiculo");
            });

            modelBuilder.Entity<ObjetosPerdidos>(entity =>
            {
                entity.HasKey(e => e.IdObjetosPerdidos);

                entity.ToTable("objetos_perdidos");

                entity.HasIndex(e => e.IdViaje)
                    .HasName("fk_OP_Viaje_idx");

                entity.Property(e => e.IdObjetosPerdidos)
                    .HasColumnName("idObjetos_Perdidos")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(45)");

                entity.Property(e => e.Detalles).HasColumnType("varchar(45)");

                entity.Property(e => e.IdViaje)
                    .HasColumnName("idViaje")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.HasOne(d => d.IdViajeNavigation)
                    .WithMany(p => p.ObjetosPerdidos)
                    .HasForeignKey(d => d.IdViaje)
                    .HasConstraintName("fk_OP_Viaje");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("persona");

                entity.Property(e => e.IdPersona)
                    .HasColumnName("idPersona")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApellidoMaterno)
                    .HasColumnName("Apellido_Materno")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ApellidoPaterno)
                    .HasColumnName("Apellido_Paterno")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Email).HasColumnType("varchar(45)");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("Fecha_Nacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.Nombre).HasColumnType("varchar(45)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.Property(e => e.Telefono).HasColumnType("varchar(25)");
            });

            modelBuilder.Entity<Posicionconductor>(entity =>
            {
                entity.HasKey(e => e.IdPosicionConductor);

                entity.ToTable("posicionconductor");

                entity.HasIndex(e => e.IdConductor)
                    .HasName("fk_Conductor_Posicion_idx");

                entity.Property(e => e.IdPosicionConductor)
                    .HasColumnName("idPosicionConductor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdConductor)
                    .HasColumnName("idConductor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Lat).HasColumnType("varchar(45)");

                entity.Property(e => e.Lng).HasColumnType("varchar(45)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.HasOne(d => d.IdConductorNavigation)
                    .WithMany(p => p.Posicionconductor)
                    .HasForeignKey(d => d.IdConductor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Conductor_Posicion");
            });

            modelBuilder.Entity<Seguro>(entity =>
            {
                entity.HasKey(e => e.IdSeguro);

                entity.ToTable("seguro");

                entity.Property(e => e.IdSeguro)
                    .HasColumnName("idSeguro")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(45)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(45)");

                entity.Property(e => e.Status).HasColumnType("int(11)");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario);

                entity.ToTable("tipo_usuario");

                entity.Property(e => e.IdTipoUsuario)
                    .HasColumnName("idTipo_Usuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(45)");

                entity.Property(e => e.NombreTipo)
                    .HasColumnName("Nombre_Tipo")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Status).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuarios);

                entity.ToTable("usuarios");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("fk_Usuario_Person_idx");

                entity.HasIndex(e => e.IdTipoUsuario)
                    .HasName("fk_Usuario_Tipo_idx");

                entity.Property(e => e.IdUsuarios)
                    .HasColumnName("idUsuarios")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(45)");

                entity.Property(e => e.IdPersona)
                    .HasColumnName("idPersona")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTipoUsuario)
                    .HasColumnName("idTipo_Usuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nombre).HasColumnType("varchar(45)");

                entity.Property(e => e.Password).HasColumnType("varchar(45)");

                entity.Property(e => e.PhoneId)
                    .HasColumnName("phoneID")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPersona)
                    .HasConstraintName("fk_Usuario_Person");

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("fk_Usuario_Tipo");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.IdVehiculo);

                entity.ToTable("vehiculo");

                entity.Property(e => e.IdVehiculo)
                    .HasColumnName("idVehiculo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(45)");

                entity.Property(e => e.Marca).HasColumnType("varchar(45)");

                entity.Property(e => e.Modelo).HasColumnType("varchar(45)");

                entity.Property(e => e.Numero).HasColumnType("int(11)");

                entity.Property(e => e.Placa).HasColumnType("varchar(45)");

                entity.Property(e => e.Status).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Viaje>(entity =>
            {
                entity.HasKey(e => e.IdViaje);

                entity.ToTable("viaje");

                entity.HasIndex(e => e.IdConductor)
                    .HasName("fk_Viaje_Conductor_idx");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("fk_Viaje_Persona_idx");

                entity.Property(e => e.IdViaje)
                    .HasColumnName("idViaje")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descripcion).HasColumnType("varchar(45)");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.IdConductor)
                    .HasColumnName("idConductor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdPersona)
                    .HasColumnName("idPersona")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.HasOne(d => d.IdConductorNavigation)
                    .WithMany(p => p.Viaje)
                    .HasForeignKey(d => d.IdConductor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Viaje_Conductor");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Viaje)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Viaje_Persona");
            });

            modelBuilder.Entity<Viajeposicion>(entity =>
            {
                entity.HasKey(e => e.IdViajePosicion);

                entity.ToTable("viajeposicion");

                entity.HasIndex(e => e.IdViaje)
                    .HasName("fk_viaje_posicionviaje_idx");

                entity.Property(e => e.IdViajePosicion)
                    .HasColumnName("idViajePosicion")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdTipo)
                    .HasColumnName("idTipo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdViaje)
                    .HasColumnName("idViaje")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Lat).HasColumnType("varchar(50)");

                entity.Property(e => e.Lng).HasColumnType("varchar(50)");

                entity.HasOne(d => d.IdViajeNavigation)
                    .WithMany(p => p.Viajeposicion)
                    .HasForeignKey(d => d.IdViaje)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_viaje_posicionviaje");
            });
        }
    }
}
