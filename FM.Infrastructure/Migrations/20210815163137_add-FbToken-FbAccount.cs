using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FM.Infrastructure.Migrations
{
    public partial class addFbTokenFbAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostGroupActions_PostGroups_PostGroupId",
                table: "PostGroupActions");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGroupComments_PostGroups_PostGroupId",
                table: "PostGroupComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGroupPrivacy_PostGroups_PostGroupId",
                table: "PostGroupPrivacy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostGroups",
                table: "PostGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostGroupPrivacy",
                table: "PostGroupPrivacy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostGroupComments",
                table: "PostGroupComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostGroupActions",
                table: "PostGroupActions");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("18a45b0b-140b-48ce-9611-146f0d5912e1"), new Guid("60b1b5df-96a3-42cf-8e39-e35f0955d9b1") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d95b7821-fe71-4b83-bb40-7e27b547ef49"), new Guid("8fd1e13c-78ec-466c-bbd2-423fa8f5f2ec") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("18a45b0b-140b-48ce-9611-146f0d5912e1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d95b7821-fe71-4b83-bb40-7e27b547ef49"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("60b1b5df-96a3-42cf-8e39-e35f0955d9b1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8fd1e13c-78ec-466c-bbd2-423fa8f5f2ec"));

            migrationBuilder.RenameTable(
                name: "PostGroups",
                newName: "FbPostGroups");

            migrationBuilder.RenameTable(
                name: "PostGroupPrivacy",
                newName: "FbPostGroupPrivacy");

            migrationBuilder.RenameTable(
                name: "PostGroupComments",
                newName: "FbPostGroupComments");

            migrationBuilder.RenameTable(
                name: "PostGroupActions",
                newName: "FbPostGroupActions");

            migrationBuilder.RenameIndex(
                name: "IX_PostGroupPrivacy_PostGroupId",
                table: "FbPostGroupPrivacy",
                newName: "IX_FbPostGroupPrivacy_PostGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_PostGroupComments_PostGroupId",
                table: "FbPostGroupComments",
                newName: "IX_FbPostGroupComments_PostGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_PostGroupActions_PostGroupId",
                table: "FbPostGroupActions",
                newName: "IX_FbPostGroupActions_PostGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FbPostGroups",
                table: "FbPostGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FbPostGroupPrivacy",
                table: "FbPostGroupPrivacy",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FbPostGroupComments",
                table: "FbPostGroupComments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FbPostGroupActions",
                table: "FbPostGroupActions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FbAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FbUid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FbAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FbTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cookie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDead = table.Column<bool>(type: "bit", nullable: false),
                    IsUsing = table.Column<bool>(type: "bit", nullable: false),
                    LastCalledTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalCalled = table.Column<int>(type: "int", nullable: false),
                    TotalCalledInLastHour = table.Column<int>(type: "int", nullable: false),
                    TotalCalledInTimeFrame = table.Column<int>(type: "int", nullable: false),
                    MaxRequestInTimeFrame = table.Column<int>(type: "int", nullable: false),
                    TimeFrameMinute = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FbAccountId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FbTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FbTokens_FbAccounts_FbAccountId",
                        column: x => x.FbAccountId,
                        principalTable: "FbAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FbTokens_FbAccountId",
                table: "FbTokens",
                column: "FbAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_FbPostGroupActions_FbPostGroups_PostGroupId",
                table: "FbPostGroupActions",
                column: "PostGroupId",
                principalTable: "FbPostGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FbPostGroupComments_FbPostGroups_PostGroupId",
                table: "FbPostGroupComments",
                column: "PostGroupId",
                principalTable: "FbPostGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FbPostGroupPrivacy_FbPostGroups_PostGroupId",
                table: "FbPostGroupPrivacy",
                column: "PostGroupId",
                principalTable: "FbPostGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FbPostGroupActions_FbPostGroups_PostGroupId",
                table: "FbPostGroupActions");

            migrationBuilder.DropForeignKey(
                name: "FK_FbPostGroupComments_FbPostGroups_PostGroupId",
                table: "FbPostGroupComments");

            migrationBuilder.DropForeignKey(
                name: "FK_FbPostGroupPrivacy_FbPostGroups_PostGroupId",
                table: "FbPostGroupPrivacy");

            migrationBuilder.DropTable(
                name: "FbTokens");

            migrationBuilder.DropTable(
                name: "FbAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FbPostGroups",
                table: "FbPostGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FbPostGroupPrivacy",
                table: "FbPostGroupPrivacy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FbPostGroupComments",
                table: "FbPostGroupComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FbPostGroupActions",
                table: "FbPostGroupActions");

            migrationBuilder.RenameTable(
                name: "FbPostGroups",
                newName: "PostGroups");

            migrationBuilder.RenameTable(
                name: "FbPostGroupPrivacy",
                newName: "PostGroupPrivacy");

            migrationBuilder.RenameTable(
                name: "FbPostGroupComments",
                newName: "PostGroupComments");

            migrationBuilder.RenameTable(
                name: "FbPostGroupActions",
                newName: "PostGroupActions");

            migrationBuilder.RenameIndex(
                name: "IX_FbPostGroupPrivacy_PostGroupId",
                table: "PostGroupPrivacy",
                newName: "IX_PostGroupPrivacy_PostGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_FbPostGroupComments_PostGroupId",
                table: "PostGroupComments",
                newName: "IX_PostGroupComments_PostGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_FbPostGroupActions_PostGroupId",
                table: "PostGroupActions",
                newName: "IX_PostGroupActions_PostGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostGroups",
                table: "PostGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostGroupPrivacy",
                table: "PostGroupPrivacy",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostGroupComments",
                table: "PostGroupComments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostGroupActions",
                table: "PostGroupActions",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("18a45b0b-140b-48ce-9611-146f0d5912e1"), "1084b983-a81b-44aa-8ac8-1dab249e8a4a", "Admin", "ADMIN" },
                    { new Guid("d95b7821-fe71-4b83-bb40-7e27b547ef49"), "8749ecb7-9094-4faa-aa0f-cfbf219b52c4", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PasswordReset", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpires", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken", "Verified" },
                values: new object[,]
                {
                    { new Guid("60b1b5df-96a3-42cf-8e39-e35f0955d9b1"), 0, "f5df12e1-b2ff-476a-8ab2-21d26ab6ce73", "admin_default@gmail.com", false, "Admin", "Admin", false, null, "ADMIN_DEFAULT@GMAIL.COM", "ADMIN_DEFAULT@GMAIL.COM", "AQAAAAEAACcQAAAAEAvxUA6BAEPk9pC9BWtTA0faBbKazUJX2z4yRcG4BbDRrt/9OyPAmLYftNoj03c35g==", null, null, false, null, null, null, false, "admin_default@gmail.com", null, new DateTime(2021, 8, 14, 9, 32, 4, 656, DateTimeKind.Utc).AddTicks(6769) },
                    { new Guid("8fd1e13c-78ec-466c-bbd2-423fa8f5f2ec"), 0, "a581e6e4-ad02-470b-8de1-03476cd619c5", "user_default@gmail.com", false, "User", "User", false, null, "USER_DEFAULT@GMAIL.COM", "USER_DEFAULT@GMAIL.COM", "AQAAAAEAACcQAAAAEFUfKC5Oh8w4eMWWFrgXFp5Ew2KqliNWl2s/vLxqxIMJo+riM4rI9RJesF8y9t4/xg==", null, null, false, null, null, null, false, "user_default@gmail.com", null, new DateTime(2021, 8, 14, 9, 32, 4, 672, DateTimeKind.Utc).AddTicks(6193) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("18a45b0b-140b-48ce-9611-146f0d5912e1"), new Guid("60b1b5df-96a3-42cf-8e39-e35f0955d9b1") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d95b7821-fe71-4b83-bb40-7e27b547ef49"), new Guid("8fd1e13c-78ec-466c-bbd2-423fa8f5f2ec") });

            migrationBuilder.AddForeignKey(
                name: "FK_PostGroupActions_PostGroups_PostGroupId",
                table: "PostGroupActions",
                column: "PostGroupId",
                principalTable: "PostGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGroupComments_PostGroups_PostGroupId",
                table: "PostGroupComments",
                column: "PostGroupId",
                principalTable: "PostGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGroupPrivacy_PostGroups_PostGroupId",
                table: "PostGroupPrivacy",
                column: "PostGroupId",
                principalTable: "PostGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
