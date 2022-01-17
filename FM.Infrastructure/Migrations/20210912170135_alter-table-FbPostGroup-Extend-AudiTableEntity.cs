using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FM.Infrastructure.Migrations
{
    public partial class altertableFbPostGroupExtendAudiTableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "FbPostGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FbPostGroups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "FbPostGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "FbPostGroups",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FbPostGroups");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FbPostGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "FbPostGroups");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "FbPostGroups");
        }
    }
}
