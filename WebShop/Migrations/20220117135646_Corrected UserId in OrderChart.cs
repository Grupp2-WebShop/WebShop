using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Migrations
{
    public partial class CorrectedUserIdinOrderChart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId1",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId1",
                table: "Order");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc412f11-026e-434f-a96a-2c1a3ba27d95");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "08fdc9aa-8f2d-412c-8d18-b098a06ab743", "6feb4202-a8b3-4dea-bead-ad540bb35fa7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6feb4202-a8b3-4dea-bead-ad540bb35fa7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08fdc9aa-8f2d-412c-8d18-b098a06ab743");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Order",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "35b03246-5eaa-44a9-8dae-15a8e75d06aa", "fcbbe5b3-f1ec-455a-87c7-14584d566b80", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a7e31b8-19a9-4bdb-82c5-03360906a1a2", "47dfcdff-dd01-4cc4-ab3a-cbf71443c963", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "City", "FirstName", "LastName", "Street", "ZipCode" },
                values: new object[] { "6ff3b475-5e19-407b-9108-fd5d3beff3e4", 0, "f0bf064f-7eb7-46b5-b3e6-7ab702b5442c", "ApplicationUser", "admin@admin.com", false, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEFVxjNBeWjGhzc2z1g+Nrrs2mAOX51SnhFwJFukoJZ8OVGT738sxVp6qqzPP8othhQ==", "070-123 45 67", false, "a3fb2929-5cfe-4780-8ef0-f3986c4a5c85", false, "admin@admin.com", "Göteborg", "Admin", "Adminsson", "Storgatan 3", "123 45" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "6ff3b475-5e19-407b-9108-fd5d3beff3e4", "35b03246-5eaa-44a9-8dae-15a8e75d06aa" });

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId",
                table: "Order");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a7e31b8-19a9-4bdb-82c5-03360906a1a2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "6ff3b475-5e19-407b-9108-fd5d3beff3e4", "35b03246-5eaa-44a9-8dae-15a8e75d06aa" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35b03246-5eaa-44a9-8dae-15a8e75d06aa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ff3b475-5e19-407b-9108-fd5d3beff3e4");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6feb4202-a8b3-4dea-bead-ad540bb35fa7", "cc1ac633-036c-43d7-a5b2-3616e87ca4e0", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc412f11-026e-434f-a96a-2c1a3ba27d95", "7ed6292c-8723-4de5-9822-25069fa50375", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "City", "FirstName", "LastName", "Street", "ZipCode" },
                values: new object[] { "08fdc9aa-8f2d-412c-8d18-b098a06ab743", 0, "060d9b8d-93b7-4932-93b0-a824f8dbc1b4", "ApplicationUser", "admin@admin.com", false, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEH/TaZpLPcCnNR/zsvUJUGWlUJhf9M2ITXdQqrqpm1jPWxRxZO1uX++H3SK7tm1v+Q==", "070-123 45 67", false, "dbc63f2e-ed8e-4f93-9a20-6754fda09d97", false, "admin@admin.com", "Göteborg", "Admin", "Adminsson", "Storgatan 3", "123 45" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "08fdc9aa-8f2d-412c-8d18-b098a06ab743", "6feb4202-a8b3-4dea-bead-ad540bb35fa7" });

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId1",
                table: "Order",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId1",
                table: "Order",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
