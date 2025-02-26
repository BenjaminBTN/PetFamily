﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "species",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_species", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "volunteers",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                experience = table.Column<double>(type: "double precision", nullable: false),
                creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                requisites = table.Column<string>(type: "jsonb", nullable: false),
                networks = table.Column<string>(type: "jsonb", nullable: false),
                is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                patronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                phone_number = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_volunteers", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "breeds",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                species_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_breeds", x => x.id);
                table.ForeignKey(
                    name: "fk_breeds_species_species_id",
                    column: x => x.species_id,
                    principalTable: "species",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "pets",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                weight = table.Column<double>(type: "double precision", nullable: false),
                height = table.Column<double>(type: "double precision", nullable: false),
                phone_number = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                is_castrated = table.Column<bool>(type: "boolean", nullable: false),
                is_vaccinated = table.Column<bool>(type: "boolean", nullable: false),
                birth_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                status = table.Column<string>(type: "text", nullable: false),
                creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                volunteer_id = table.Column<Guid>(type: "uuid", nullable: false),
                city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                house_number = table.Column<int>(type: "integer", nullable: false),
                postal_code = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                despription = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                health_info = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                ordinal_number = table.Column<int>(type: "integer", nullable: false),
                breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                species_id = table.Column<Guid>(type: "uuid", nullable: false),
                pet_photos = table.Column<string>(type: "jsonb", nullable: false),
                requisites_for_help = table.Column<string>(type: "jsonb", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_pets", x => x.id);
                table.ForeignKey(
                    name: "fk_pets_volunteers_volunteer_id",
                    column: x => x.volunteer_id,
                    principalTable: "volunteers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_breeds_species_id",
            table: "breeds",
            column: "species_id");

        migrationBuilder.CreateIndex(
            name: "ix_pets_volunteer_id",
            table: "pets",
            column: "volunteer_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "breeds");

        migrationBuilder.DropTable(
            name: "pets");

        migrationBuilder.DropTable(
            name: "species");

        migrationBuilder.DropTable(
            name: "volunteers");
    }
}
