using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FM.Infrastructure.Migrations
{
    public partial class ChangetablenameFbprefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("18a45b0b-140b-48ce-9611-146f0d5912e1"),
                column: "ConcurrencyStamp",
                value: "1084b983-a81b-44aa-8ac8-1dab249e8a4a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d95b7821-fe71-4b83-bb40-7e27b547ef49"),
                column: "ConcurrencyStamp",
                value: "8749ecb7-9094-4faa-aa0f-cfbf219b52c4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("60b1b5df-96a3-42cf-8e39-e35f0955d9b1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Verified" },
                values: new object[] { "f5df12e1-b2ff-476a-8ab2-21d26ab6ce73", "AQAAAAEAACcQAAAAEAvxUA6BAEPk9pC9BWtTA0faBbKazUJX2z4yRcG4BbDRrt/9OyPAmLYftNoj03c35g==", new DateTime(2021, 8, 14, 9, 32, 4, 656, DateTimeKind.Utc).AddTicks(6769) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8fd1e13c-78ec-466c-bbd2-423fa8f5f2ec"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Verified" },
                values: new object[] { "a581e6e4-ad02-470b-8de1-03476cd619c5", "AQAAAAEAACcQAAAAEFUfKC5Oh8w4eMWWFrgXFp5Ew2KqliNWl2s/vLxqxIMJo+riM4rI9RJesF8y9t4/xg==", new DateTime(2021, 8, 14, 9, 32, 4, 672, DateTimeKind.Utc).AddTicks(6193) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
