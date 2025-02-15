﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetFamily.Infrastructure.DbContexts;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetFamily.Domain.SpeciesManagement.Entities.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("species_id")
                        .HasColumnType("uuid")
                        .HasColumnName("species_id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetFamily.Domain.SpeciesManagement.Entities.Breed.Name#BreedName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_breeds");

                    b.HasIndex("species_id")
                        .HasDatabaseName("ix_breeds_species_id");

                    b.ToTable("breeds", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.SpeciesManagement.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetFamily.Domain.SpeciesManagement.Species.Name#SpeciesName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_species");

                    b.ToTable("species", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.VolunteersManagement.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("birth_date");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("creation_date");

                    b.Property<double>("Height")
                        .HasColumnType("double precision")
                        .HasColumnName("height");

                    b.Property<bool>("IsCastrated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_castrated");

                    b.Property<bool>("IsVaccinated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_vaccinated");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<Guid?>("volunteer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("volunteer_id");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "PetFamily.Domain.VolunteersManagement.Entities.Pet.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("country");

                            b1.Property<int>("HouseNumber")
                                .HasColumnType("integer")
                                .HasColumnName("house_number");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasMaxLength(6)
                                .HasColumnType("character varying(6)")
                                .HasColumnName("postal_code");

                            b1.Property<string>("Region")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("region");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Color", "PetFamily.Domain.VolunteersManagement.Entities.Pet.Color#PetColor", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("color");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetFamily.Domain.VolunteersManagement.Entities.Pet.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("despription");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("HealthInfo", "PetFamily.Domain.VolunteersManagement.Entities.Pet.HealthInfo#PetHealthInfo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("health_info");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "PetFamily.Domain.VolunteersManagement.Entities.Pet.Name#PetName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("OrdinalNumber", "PetFamily.Domain.VolunteersManagement.Entities.Pet.OrdinalNumber#OrdinalNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("ordinal_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("TypeInfo", "PetFamily.Domain.VolunteersManagement.Entities.Pet.TypeInfo#PetType", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<Guid>("BreedId")
                                .HasColumnType("uuid")
                                .HasColumnName("breed_id");

                            b1.Property<Guid>("SpeciesId")
                                .HasColumnType("uuid")
                                .HasColumnName("species_id");
                        });

                    b.HasKey("Id")
                        .HasName("pk_pets");

                    b.HasIndex("volunteer_id")
                        .HasDatabaseName("ix_pets_volunteer_id");

                    b.ToTable("pets", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.VolunteersManagement.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("creation_date");

                    b.Property<double>("Experience")
                        .HasColumnType("double precision")
                        .HasColumnName("experience");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "PetFamily.Domain.VolunteersManagement.Volunteer.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Email", "PetFamily.Domain.VolunteersManagement.Volunteer.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "PetFamily.Domain.VolunteersManagement.Volunteer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("name");

                            b1.Property<string>("Patronymic")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("patronymic");

                            b1.Property<string>("Surname")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("surname");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("PhoneNumber", "PetFamily.Domain.VolunteersManagement.Volunteer.PhoneNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(12)
                                .HasColumnType("character varying(12)")
                                .HasColumnName("phone_number");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volonteers");

                    b.ToTable("volonteers", (string)null);
                });

            modelBuilder.Entity("PetFamily.Domain.SpeciesManagement.Entities.Breed", b =>
                {
                    b.HasOne("PetFamily.Domain.SpeciesManagement.Species", null)
                        .WithMany("Breeds")
                        .HasForeignKey("species_id")
                        .HasConstraintName("fk_breeds_species_species_id");
                });

            modelBuilder.Entity("PetFamily.Domain.VolunteersManagement.Entities.Pet", b =>
                {
                    b.HasOne("PetFamily.Domain.VolunteersManagement.Volunteer", null)
                        .WithMany("Pets")
                        .HasForeignKey("volunteer_id")
                        .HasConstraintName("fk_pets_volonteers_volunteer_id");

                    b.OwnsOne("PetFamily.Domain.Shared.VO.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(12)
                                .HasColumnType("character varying(12)")
                                .HasColumnName("phone_number");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");
                        });

                    b.OwnsOne("PetFamily.Domain.Shared.VO.PhotoList", "PetPhotos", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("pet_photos");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("PetFamily.Domain.Shared.VO.Photo", "Photos", b2 =>
                                {
                                    b2.Property<Guid>("PhotoListPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<bool>("IsMain")
                                        .HasColumnType("boolean");

                                    b2.Property<string>("PathToStorage")
                                        .IsRequired()
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)");

                                    b2.HasKey("PhotoListPetId", "__synthesizedOrdinal")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("PhotoListPetId")
                                        .HasConstraintName("fk_pets_pets_photo_list_pet_id");
                                });

                            b1.Navigation("Photos");
                        });

                    b.OwnsOne("PetFamily.Domain.VolunteersManagement.VO.RequisiteForHelpList", "RequisitesForHelp", b1 =>
                        {
                            b1.Property<Guid>("PetId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("PetId");

                            b1.ToTable("pets");

                            b1.ToJson("requisites_for_help");

                            b1.WithOwner()
                                .HasForeignKey("PetId")
                                .HasConstraintName("fk_pets_pets_id");

                            b1.OwnsMany("PetFamily.Domain.VolunteersManagement.VO.RequisiteForHelp", "Requisites", b2 =>
                                {
                                    b2.Property<Guid>("RequisiteForHelpListPetId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("RequisiteForHelpListPetId", "__synthesizedOrdinal")
                                        .HasName("pk_pets");

                                    b2.ToTable("pets");

                                    b2.WithOwner()
                                        .HasForeignKey("RequisiteForHelpListPetId")
                                        .HasConstraintName("fk_pets_pets_requisite_for_help_list_pet_id");
                                });

                            b1.Navigation("Requisites");
                        });

                    b.Navigation("PetPhotos")
                        .IsRequired();

                    b.Navigation("PhoneNumber")
                        .IsRequired();

                    b.Navigation("RequisitesForHelp")
                        .IsRequired();
                });

            modelBuilder.Entity("PetFamily.Domain.VolunteersManagement.Volunteer", b =>
                {
                    b.OwnsOne("PetFamily.Domain.Shared.VO.SocialNetworkList", "Networks", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volonteers");

                            b1.ToJson("networks");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volonteers_volonteers_id");

                            b1.OwnsMany("PetFamily.Domain.Shared.VO.SocialNetwork", "Networks", b2 =>
                                {
                                    b2.Property<Guid>("SocialNetworkListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Name")
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<string>("Url")
                                        .HasColumnType("text");

                                    b2.HasKey("SocialNetworkListVolunteerId", "__synthesizedOrdinal")
                                        .HasName("pk_volonteers");

                                    b2.ToTable("volonteers");

                                    b2.WithOwner()
                                        .HasForeignKey("SocialNetworkListVolunteerId")
                                        .HasConstraintName("fk_volonteers_volonteers_social_network_list_volunteer_id");
                                });

                            b1.Navigation("Networks");
                        });

                    b.OwnsOne("PetFamily.Domain.VolunteersManagement.VO.VolunteerRequisiteList", "Requisites", b1 =>
                        {
                            b1.Property<Guid>("VolunteerId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.HasKey("VolunteerId");

                            b1.ToTable("volonteers");

                            b1.ToJson("requisites");

                            b1.WithOwner()
                                .HasForeignKey("VolunteerId")
                                .HasConstraintName("fk_volonteers_volonteers_id");

                            b1.OwnsMany("PetFamily.Domain.VolunteersManagement.VO.VolunteerRequisite", "Requisites", b2 =>
                                {
                                    b2.Property<Guid>("VolunteerRequisiteListVolunteerId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("__synthesizedOrdinal")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<string>("Description")
                                        .HasMaxLength(2000)
                                        .HasColumnType("character varying(2000)");

                                    b2.Property<string>("Name")
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.HasKey("VolunteerRequisiteListVolunteerId", "__synthesizedOrdinal")
                                        .HasName("pk_volonteers");

                                    b2.ToTable("volonteers");

                                    b2.WithOwner()
                                        .HasForeignKey("VolunteerRequisiteListVolunteerId")
                                        .HasConstraintName("fk_volonteers_volonteers_volunteer_requisite_list_volunteer_id");
                                });

                            b1.Navigation("Requisites");
                        });

                    b.Navigation("Networks")
                        .IsRequired();

                    b.Navigation("Requisites")
                        .IsRequired();
                });

            modelBuilder.Entity("PetFamily.Domain.SpeciesManagement.Species", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("PetFamily.Domain.VolunteersManagement.Volunteer", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
