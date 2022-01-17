using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FM.Infrastructure.Migrations
{
    public partial class AddtablePostGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    ObjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShareCount = table.Column<long>(type: "bigint", nullable: false),
                    StatusType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subscribed = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FbId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostGroupActions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostGroupId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGroupActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostGroupActions_PostGroups_PostGroupId",
                        column: x => x.PostGroupId,
                        principalTable: "PostGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostGroupComments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanRemove = table.Column<bool>(type: "bit", nullable: false),
                    LikeCount = table.Column<long>(type: "bigint", nullable: false),
                    UserLikes = table.Column<long>(type: "bigint", nullable: false),
                    FbId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostGroupId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGroupComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostGroupComments_PostGroups_PostGroupId",
                        column: x => x.PostGroupId,
                        principalTable: "PostGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostGroupPrivacy",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Allow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Friends = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostGroupId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGroupPrivacy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostGroupPrivacy_PostGroups_PostGroupId",
                        column: x => x.PostGroupId,
                        principalTable: "PostGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("18a45b0b-140b-48ce-9611-146f0d5912e1"),
                column: "ConcurrencyStamp",
                value: "41db7ddd-c701-489a-b77b-2c2009143d5e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d95b7821-fe71-4b83-bb40-7e27b547ef49"),
                column: "ConcurrencyStamp",
                value: "d7293cb0-5564-4892-bd84-86cb1846f9bb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("60b1b5df-96a3-42cf-8e39-e35f0955d9b1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Verified" },
                values: new object[] { "f308541f-13b8-4a06-ae6f-de268392e9ef", "AQAAAAEAACcQAAAAEDYgEgtsDRr0tuxqsl8EdnuaEojaH/pSqNYyf61vBjphNt/xWExDDEHfyQVWfaKfeg==", new DateTime(2021, 8, 14, 4, 41, 18, 508, DateTimeKind.Utc).AddTicks(1115) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8fd1e13c-78ec-466c-bbd2-423fa8f5f2ec"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Verified" },
                values: new object[] { "269d87a2-82d4-4352-a36d-2cf51cad8c88", "AQAAAAEAACcQAAAAEGEUX7TM4bCxOU2RwYij9c5i6Zb87PBVlE/dsFqInBNA6JH/qJXO8+OVkZHxEwFT+w==", new DateTime(2021, 8, 14, 4, 41, 18, 533, DateTimeKind.Utc).AddTicks(4016) });

            migrationBuilder.CreateIndex(
                name: "IX_PostGroupActions_PostGroupId",
                table: "PostGroupActions",
                column: "PostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PostGroupComments_PostGroupId",
                table: "PostGroupComments",
                column: "PostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PostGroupPrivacy_PostGroupId",
                table: "PostGroupPrivacy",
                column: "PostGroupId",
                unique: true,
                filter: "[PostGroupId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostGroupActions");

            migrationBuilder.DropTable(
                name: "PostGroupComments");

            migrationBuilder.DropTable(
                name: "PostGroupPrivacy");

            migrationBuilder.DropTable(
                name: "PostGroups");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("18a45b0b-140b-48ce-9611-146f0d5912e1"),
                column: "ConcurrencyStamp",
                value: "8dfd08ad-9bd9-43ed-9b65-153991ef8dbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d95b7821-fe71-4b83-bb40-7e27b547ef49"),
                column: "ConcurrencyStamp",
                value: "6c2c9d20-5d17-4d5b-bb74-07f17c72054f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("60b1b5df-96a3-42cf-8e39-e35f0955d9b1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Verified" },
                values: new object[] { "bef5a3cc-578e-4c79-8e99-4d96aa23e17d", "AQAAAAEAACcQAAAAEISpkIpFxxIqQQGeh1AUvuZVsmos5r2vfpkHLJoODRVC8EiWgIjohGrZ/L+4bpPdcA==", new DateTime(2021, 8, 12, 10, 52, 54, 768, DateTimeKind.Utc).AddTicks(2285) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8fd1e13c-78ec-466c-bbd2-423fa8f5f2ec"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Verified" },
                values: new object[] { "f3f930ce-79ee-4b31-9ea6-d5d8635d55c8", "AQAAAAEAACcQAAAAEPyy4rvb/fOHBgbv1BZ3oFQkJfYGzxN9TkeTGCCdbukIlXBXcp1Wd0SuohKBR1GZzg==", new DateTime(2021, 8, 12, 10, 52, 54, 789, DateTimeKind.Utc).AddTicks(6854) });
        }
    }
}
